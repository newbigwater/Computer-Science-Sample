using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Activities.Presentation;
using System.Activities.Statements;
using System.Activities.Core.Presentation;
using System.Activities.Presentation.View;
using MyEditorServiceApp;

namespace MyEditorServiceApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private MyEditorService expressionEditorService;
        public MainWindow()
        {
            InitializeComponent();
            new DesignerMetadata().Register();
            createDesigner();
        }

        public void createDesigner()
        {
            WorkflowDesigner designer = new WorkflowDesigner();
            Sequence root = new Sequence()
            {
                Activities = {
                new Assign(),
                new WriteLine()}
            };

            designer.Load(root);

            Grid.SetColumn(designer.View, 0);

            // Create ExpressionEditorService
            this.expressionEditorService = new MyEditorService();

            // Publish the instance of MyEditorService.  
            designer.Context.Services.Publish<IExpressionEditorService>(this.expressionEditorService);

            MyGrid.Children.Add(designer.View);
        }
    }
}
