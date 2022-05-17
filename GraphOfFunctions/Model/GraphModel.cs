using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;

namespace GraphOfFunctions.Model
{
    public class GraphModel:ReactiveObject
    {
        private Chart FuncChart;

        //Первая минимальная точка Y, и соотвественно ее координата на X
        [Reactive]
        private double Minimum { get; set; }    
        private double min_x;
        //Вторая минимальная точка Y, и соотвественно ее координата на X
        [Reactive]
        private double Minimum2 { get; set; }
        private double min_x_2;
        //Первая максимальная точка Y, и соотвественно ее координата на X
        [Reactive]
        private double Maximum { get; set; }
        private double max_x;
        //Вторая максимальная точка Y, и соотвественно ее координата на X
        [Reactive]
        private double Maximum2 { get; set; } 
        private double max_x_2;
        [Reactive]
        public string MinPoint { get; set; }
        [Reactive]
        public string MaxPoint { get; set; }
        private void UpdateMinPoint()
        {
            if (Minimum==0 && Minimum2==0)
                MinPoint="";
            else
            {
                MinPoint= $"Точки локального минимума ";
                if (Minimum!=0)
                    MinPoint+=$"X:{Math.Round(min_x, 2)} Y:{Math.Round(Minimum, 2)}; ";
                if (Minimum2!=0)
                    MinPoint+=$"X:{Math.Round(min_x_2, 2)} Y:{Math.Round(Minimum2, 2)};";
            }
        }
        private void UpdateMaxPoint()
        {
            if (Maximum==0 && Maximum2==0)
                MaxPoint="";
            else
            {
                MaxPoint= $"Точки локального максимума ";
                if (Maximum!=0)
                    MaxPoint+=$"X:{Math.Round(max_x, 2)} Y:{Math.Round(Maximum, 2)}; ";
                if(Maximum2!=0)
                    MaxPoint+=$"X:{Math.Round(max_x_2, 2)} Y:{Math.Round(Maximum2, 2)};";
            }
                
           
                
        }

        public GraphModel(Chart FuncChart)
        {
            this .FuncChart = FuncChart;
            this.WhenAnyValue(p => p.Minimum).Subscribe(_ => UpdateMinPoint());
            this.WhenAnyValue(p => p.Minimum2).Subscribe(_ => UpdateMinPoint());
            this.WhenAnyValue(p => p.Maximum2).Subscribe(_ => UpdateMaxPoint());
            this.WhenAnyValue(p => p.Maximum).Subscribe(_ => UpdateMaxPoint());
        }
        private double sqrt(double x)
        {
            if (x<0)
            {
                x=Math.Sqrt(-x);
                x=0-x;
            }
            else
                x=Math.Sqrt(x);
            return x;
        }
        private void SetInterval(double start_x, double end_x)
        {
            FuncChart.ChartAreas[0].AxisX.Minimum = start_x-1;
            FuncChart.ChartAreas[0].AxisX.Maximum = end_x+1;
            if (start_x<0)
                start_x=0-start_x;
            if (end_x<0)
                end_x=0-end_x;
            var interval = ((start_x+end_x)/2)/10;
            Console.WriteLine(interval);
            FuncChart.ChartAreas[0].AxisX.Interval=interval;
            FuncChart.ChartAreas[0].AxisY.Interval=interval;
        }
        public void CreateFunctGraph(double start_x, double end_x)
        {
            if(start_x>end_x)
            {
                MessageBox.Show("Стартовая точка интервала не может быть меньше конечной","Ошибка");
                return;
            }
            if(start_x==end_x)
            {
                MessageBox.Show("Стартовая точка интервала не может быть равна конечной", "Ошибка");
                return;
            }
            SetInterval(start_x,end_x);

            ClearGraph();
            for (double x = start_x; x<=end_x; x+=0.1)
            {
                double y = sqrt(Math.Pow(x, 2)* Math.Cos(x)+2);
                FuncChart.Series[0].Points.AddXY(x, y);
                if(y<Minimum && x<0)
                {
                    Minimum=y;
                    min_x=x;
                    
                }
                if (y<Minimum2 && x>0)
                {
                    Minimum2=y;
                    min_x_2=x;

                }
                if (y>Maximum && x<0)
                {
                   
                    Maximum=y;
                    max_x=x;
                }
                if (y>Maximum2 && x>0)
                {
                    
                    Maximum2=y;
                    max_x_2=x;
                }
            }
            if (Math.Round(Minimum,4)<=Math.Round(Minimum2, 4))
                AddPointToChart(min_x, Minimum, 1);
            else
                Minimum=0;
            if(Math.Round(Minimum2, 4)<=Math.Round(Minimum, 4))
                AddPointToChart(min_x_2, Minimum2, 1);
            else
                Minimum2=0;
            if (Math.Round(Maximum, 4)>=Math.Round(Maximum2, 4))
                AddPointToChart(max_x, Maximum, 2);
            else
               Maximum=0;
            if (Math.Round(Maximum2, 4)>=Math.Round(Maximum, 4))
                AddPointToChart(max_x_2, Maximum2, 2);
            else
                Maximum2=0;
        }
        private void AddPointToChart(double x, double y, int chart)
        {
            DataPoint point = new DataPoint(x, y);
            point.Label=$"X:{Math.Round(x, 2)} Y:{Math.Round(y, 2)}";
            FuncChart.Series[chart].Points.Add(point);
        }
        public void ClearGraph()
        {
            FuncChart.Series[0].Points.Clear();
            FuncChart.Series[1].Points.Clear();
            FuncChart.Series[2].Points.Clear();
            Minimum=0;
            Minimum2 = 0;
            Maximum=0;
            Maximum2 = 0;
        }
    }
}
