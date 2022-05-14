using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphOfFunctions.View
{
    /// <summary>
    /// Логика взаимодействия для FuncGraph.xaml
    /// </summary>
    public partial class FuncGraph : UserControl
    {
        public FuncGraph()
        {
            InitializeComponent();
            chart.ChartAreas.Add(new ChartArea("Default"));
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series("1");
            series.ChartArea = "Default";
            chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart.Series.Add(series);
            chart.ChartAreas[0].AxisX.ArrowStyle =
        System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Lines;
            chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart.Series[0].Color= System.Drawing.Color.Green;
           
        }
    }
}
