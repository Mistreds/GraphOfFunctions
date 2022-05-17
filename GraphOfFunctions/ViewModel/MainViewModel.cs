using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GraphOfFunctions.ViewModel
{
   public class MainViewModel:ReactiveObject
    {
        private View.FuncGraph FuncGraph { get; set; }   

        public UserControl MainControl { get; set; }
        [Reactive]
        public Model.GraphModel GraphModel { get; set; }
        [Reactive]
        public int start_x { get; set; }
        [Reactive]
        public int end_x { get; set; }    

        public MainViewModel()
        {
            FuncGraph=new View.FuncGraph();
            FuncGraph.DataContext=this;
            MainControl=FuncGraph;
            GraphModel=new Model.GraphModel(FuncGraph.chart);
            start_x=-10;
            end_x=10;
            
        }
        public ReactiveCommand<Unit, Unit> CreateGraph => ReactiveCommand.Create(() => { GraphModel.CreateFunctGraph(start_x, end_x); });
        public ReactiveCommand<Unit, Unit> ClearGraph => ReactiveCommand.Create(GraphModel.ClearGraph);
    }
}
