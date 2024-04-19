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
using System.Activities.Presentation.Toolbox;
using System.Activities.Statements;

// 유효성 검사 오류 표시
using System.Activities.Presentation.Validation;
using System.Diagnostics;

using System.Xaml;
using ExpressionTextBox;
using System.Activities.Presentation.View;

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
            wd = new WorkflowDesigner();

            // Place the designer canvas in the middle column of the grid.
            Grid.SetColumn(wd.View, 1);

            // Load a new Flowchart as default.
            var builder = new ActivityBuilder
            {
                Implementation = new Flowchart(),
                Name = "ActivityBuilder"
            };
            // Add Argument - Name에 Space 포함시 주의 발생
            builder.Properties.Add(new DynamicActivityProperty { Name = "BuilderName", Type = typeof(InArgument<string>) });
            wd.Load(builder);

            // Add the designer canvas to the grid.
            grid1.Children.Add(wd.View);

            // 인수 탭 추가
            //var designerView = wd.Context.Services.GetService<DesignerView>();
            //designerView.WorkflowShellBarItemVisibility =
            //    ShellBarItemVisibility.Variables |
            //    ShellBarItemVisibility.Arguments |  // 인수 탭
            //    ShellBarItemVisibility.Imports |
            //    ShellBarItemVisibility.Zoom |
            //    ShellBarItemVisibility.MiniMap;
        }

        private void RegisterMetadata()
        {
            var dm = new DesignerMetadata();
            dm.Register();
        }

        #endregion

        #region Toolbox
        private ToolboxControl GetToolboxControl()
        {
            // Create the ToolBoxControl.
            var ctrl = new ToolboxControl();

            // Create a category.
            var eisCategory = new ToolboxCategory("Custom");
            var controlCategory = new ToolboxCategory("ControlCategory");
            var flowCategory = new ToolboxCategory("FlowCategory");
            var runtimeCategory = new ToolboxCategory("Runtime");
            var primitivesCategory = new ToolboxCategory("Primitives");
            var transation = new ToolboxCategory("Transaction");
            var collection = new ToolboxCategory("Collection");
            var errorHandling = new ToolboxCategory("Error Handling");

            // Create Toolbox items.
            MultiAssign multiAssign = new MultiAssign();
            MyDynamicActivity myDynamicActivity = new MyDynamicActivity();
              // ToolboxItemWrapper 생성 4가지 방법
            var iDoWhile = new ToolboxItemWrapper(typeof(DoWhile));
            var iForEach = new ToolboxItemWrapper(typeof(ForEach<>), "ForEach");
            var iIf = new ToolboxItemWrapper(typeof(If), "icon-bitmap.png", "If");
            var iParallel = new ToolboxItemWrapper("System.Activities.Statements.Parallel", typeof(Parallel).Assembly.FullName, null, "Parallel");

            var iParallelForEach = new ToolboxItemWrapper(typeof(ParallelForEach<>));
            var iPick = new ToolboxItemWrapper(typeof(Pick));
            var iPickBranch = new ToolboxItemWrapper(typeof(PickBranch));
            var iSequence = new ToolboxItemWrapper(typeof(Sequence));
            var iSwitch = new ToolboxItemWrapper(typeof(Switch<>));
            var iWhile = new ToolboxItemWrapper(typeof(While));

            // Add the Toolbox items to the category.
            eisCategory.Add(new ToolboxItemWrapper(multiAssign.GetType(), multiAssign.GetType().Name));
            eisCategory.Add(new ToolboxItemWrapper(myDynamicActivity.GetType(), myDynamicActivity.GetType().Name));
            controlCategory.Add(iDoWhile);
            controlCategory.Add(iForEach);
            controlCategory.Add(iIf);
            controlCategory.Add(iParallel);
            controlCategory.Add(iParallelForEach);
            controlCategory.Add(iPick);
            controlCategory.Add(iPickBranch);
            controlCategory.Add(iSequence);
            controlCategory.Add(iSwitch);
            controlCategory.Add(iWhile);
            flowCategory.Add(new ToolboxItemWrapper(typeof(Flowchart)));
            flowCategory.Add(new ToolboxItemWrapper(typeof(FlowDecision)));
            flowCategory.Add(new ToolboxItemWrapper(typeof(FlowSwitch<>)));
            runtimeCategory.Add(new ToolboxItemWrapper(typeof(Persist)));
            runtimeCategory.Add(new ToolboxItemWrapper(typeof(TerminateWorkflow)));
            primitivesCategory.Add(new ToolboxItemWrapper(typeof(Assign)));
            primitivesCategory.Add(new ToolboxItemWrapper(typeof(Delay)));
            primitivesCategory.Add(new ToolboxItemWrapper(typeof(InvokeMethod)));
            primitivesCategory.Add(new ToolboxItemWrapper(typeof(WriteLine)));
            transation.Add(new ToolboxItemWrapper(typeof(CancellationScope)));
            transation.Add(new ToolboxItemWrapper(typeof(CompensableActivity)));
            transation.Add(new ToolboxItemWrapper(typeof(Compensate)));
            transation.Add(new ToolboxItemWrapper(typeof(Confirm)));
            transation.Add(new ToolboxItemWrapper(typeof(TransactionScope)));
            collection.Add(new ToolboxItemWrapper(typeof(AddToCollection<>)));
            collection.Add(new ToolboxItemWrapper(typeof(ClearCollection<>)));
            collection.Add(new ToolboxItemWrapper(typeof(ExistsInCollection<>)));
            collection.Add(new ToolboxItemWrapper(typeof(RemoveFromCollection<>)));
            errorHandling.Add(new ToolboxItemWrapper(typeof(Rethrow)));
            errorHandling.Add(new ToolboxItemWrapper(typeof(Throw)));
            errorHandling.Add(new ToolboxItemWrapper(typeof(TryCatch)));

            // Add the category to the ToolBox control.
            ctrl.Categories.Add(eisCategory);
            ctrl.Categories.Add(controlCategory);
            ctrl.Categories.Add(flowCategory);
            ctrl.Categories.Add(runtimeCategory);
            ctrl.Categories.Add(primitivesCategory);
            ctrl.Categories.Add(transation);
            ctrl.Categories.Add(collection);
            ctrl.Categories.Add(errorHandling);

            return ctrl;
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

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "XAML File (*.xaml)|*.xaml|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                wd.Save(saveFileDialog.FileName);
            }
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            var OpenFileDialog = new Microsoft.Win32.OpenFileDialog();
            OpenFileDialog.Filter = "XAML File (*.xaml)|*.xaml|All files (*.*)|*.*";
            if (OpenFileDialog.ShowDialog() == true)
            {
                ActivityBuilder activityBuilder = new ActivityBuilder();
                Activity workflow;

                using (var stream = System.IO.File.OpenRead(OpenFileDialog.FileName))
                {
                    if(XamlServices.Load(stream) is Activity act)
                    {
                        activityBuilder.Implementation = act;
                    }
                }

                workflow = activityBuilder.Implementation;
                if (workflow != null)
                {
                    WorkflowApplication workflowApplication = null;
                    try
                    {
                        workflowApplication = new WorkflowApplication(workflow);
                        workflowApplication.Run();
                        MessageBox.Show("Workflow completed.");
                    }
                    finally
                    {
                        if (workflowApplication != null)
                        {
                            workflowApplication.Abort();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Failed to load workflow.");
                }

                // XAML 파일에서 워크플로 로드
                //Activity workflow;
                //using (var stream = System.IO.File.OpenRead(OpenFileDialog.FileName))
                //{
                //    workflow = XamlServices.Load(stream) as Activity;
                //}

                // 워크플로 실행
                //if (workflow != null)
                //{
                //    WorkflowApplication workflowApplication = null;
                //    try
                //    {
                //        workflowApplication = new WorkflowApplication(workflow);
                //        workflowApplication.Run();
                //        MessageBox.Show("Workflow completed.");
                //    }
                //    finally
                //    {
                //        if (workflowApplication != null)
                //        {
                //            workflowApplication.Abort();
                //        }
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Failed to load workflow.");
                //}
            }
        }
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
