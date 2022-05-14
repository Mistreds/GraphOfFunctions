using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GraphOfFunctions.ViewModel
{
   public class MainViewModel:ReactiveObject
    {
        private View.FuncGraph FuncGraph { get; set; }   

        public UserControl MainControl { get; set; }    

        public MainViewModel()
        {
            FuncGraph=new View.FuncGraph();
            FuncGraph.DataContext=this;
            MainControl=FuncGraph;
            CreacteGraphTest();
        }
      private void CreacteGraphTest()
        {
            double x = -10;
            double x2 = 10;
            for(double i =x;i<=x2;i++)
            {
                double y= sqrt(Math.Pow(i, 2)* Math.Cos(i)+2);
                Console.WriteLine(y);
                FuncGraph.chart.Series[0].Points.AddXY(i, y);
            }
            //double y = Math.Sqrt(Math.Pow(x,2)* Math.Cos(x)+2);
            
        }
        private double sqrt(double x)
        {
            if(x<0)
            {
                x=Math.Sqrt(-x);
                x=0-x;
            }
            else
            {
                x=Math.Sqrt(x);
            }
            return x;
        }
    }
}
