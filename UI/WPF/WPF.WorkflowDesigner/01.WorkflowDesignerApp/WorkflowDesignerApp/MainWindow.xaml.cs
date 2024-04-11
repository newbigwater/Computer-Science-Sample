using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

// Workflow Designer 재호스팅
//  System.Activities 참조 추가
//  System.Activities.Presentation 참조 추가
//  System.Activities.Core.Presentation 참조 추가
using System.Activities;
using System.Activities.Core.Presentation;
using System.Activities.Presentation;
using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;
using System.ComponentModel;

// 유효성 검사 오류 표시
using System.Activities.Presentation.Validation;
using System.Diagnostics;

namespace WorkflowDesignerApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private WorkflowDesigner wd;
        public MainWindow()
        {
            InitializeComponent();
            RegisterMetadata();
            AddDesigner();
            AddToolBox();
            AddPropertyInspector();

            // IValidationErrorService 인터페이스를 편집 컨텍스트에 게시
            wd.Context.Services.Publish<IValidationErrorService>(new DebugValidationErrorService());
        }
        #region WorkflowDesigner
        private void AddDesigner()
        {
            // Create an instance of WorkflowDesigner class.
            this.wd = new WorkflowDesigner();

            // Place the designer canvas in the middle column of the grid.
            Grid.SetColumn(this.wd.View, 1);

            // Load a new Sequence as default.
            this.wd.Load(new Flowchart());

            // Add the designer canvas to the grid.
            grid1.Children.Add(this.wd.View);
        }

        private void RegisterMetadata()
        {
            var dm = new DesignerMetadata();
            dm.Register();


            AttributeTableBuilder builder = new AttributeTableBuilder();
            builder.AddCustomAttributes(typeof(MyDynamicActivity), new DescriptionAttribute("My Dynamic Activity Description"));
            builder.AddCustomAttributes(typeof(MyDynamicActivity), "InputParameter", new DescriptionAttribute("Input Parameter Description"));
            builder.AddCustomAttributes(typeof(MyDynamicActivity), "OutputParameter", new DescriptionAttribute("Output Parameter Description"));

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }

        #endregion

        #region Toolbox
        private ToolboxControl GetToolboxControl()
        {
            // Create the ToolBoxControl.
            var ctrl = new ToolboxControl();

            // Create a category.
            var category = new ToolboxCategory("category1");

            // Create Toolbox items.
            var tool1 =
                new ToolboxItemWrapper("System.Activities.Statements.Assign",
                typeof(Assign).Assembly.FullName, null, "Assign");

            var tool2 = new ToolboxItemWrapper("System.Activities.Statements.Sequence",
                typeof(Sequence).Assembly.FullName, null, "Sequence");

            var tool3 = new ToolboxItemWrapper(typeof(WriteLine));

            // Add the Toolbox items to the category.
            category.Add(tool1);
            category.Add(tool2);
            category.Add(tool3);

            // Add Custom Control
            var eisCategory = new ToolboxCategory("Custom");

            MyDynamicActivity myDynamicActivity = new MyDynamicActivity();
            eisCategory.Add(new ToolboxItemWrapper(myDynamicActivity.GetType(), myDynamicActivity.GetType().Name));

            // Add the category to the ToolBox control.
            ctrl.Categories.Add(eisCategory);
            ctrl.Categories.Add(category);
            return ctrl;
        }

        public class MyDynamicActivity : NativeActivity
        {
            public InArgument<string> InputParameter { get; set; }

            public OutArgument<string> OutputParameter { get; set; }

            protected override void Execute(NativeActivityContext context)
            {
                // Custom activity logic here
                string input = InputParameter.Get(context);
                Console.WriteLine($"Executing MyDynamicActivity with input: {input}");

                // Set output value
                OutputParameter.Set(context, "Output Value");
            }
        }

        private void AddToolBox()
        {
            ToolboxControl tc = GetToolboxControl();
            Grid.SetColumn(tc, 0);
            grid1.Children.Add(tc);
        }

        #endregion

        #region PropertyGrid
        private void AddPropertyInspector()
        {
            Grid.SetColumn(wd.PropertyInspectorView, 2);
            grid1.Children.Add(wd.PropertyInspectorView);
        }

        #endregion
    }

    #region IValidationErrorService 인터페이스 구현
    class DebugValidationErrorService : IValidationErrorService
    {
        public void ShowValidationErrors(IList<ValidationErrorInfo> errors)
        {
            errors.ToList().ForEach(vei => Debug.WriteLine($"Error: {vei.Message}"));
        }
    }

    #endregion
}
