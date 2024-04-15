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
            this.wd = new WorkflowDesigner();

            // Place the designer canvas in the middle column of the grid.
            Grid.SetColumn(this.wd.View, 1);

            // Load a new Flowchart as default.
            this.wd.Load(new Flowchart());

            // 인수 탭 추가
            var designerView = wd.Context.Services.GetService<DesignerView>();
            designerView.WorkflowShellBarItemVisibility =
                ShellBarItemVisibility.Variables |
                ShellBarItemVisibility.Arguments |  // 인수 탭
                ShellBarItemVisibility.Imports |
                ShellBarItemVisibility.Zoom |
                ShellBarItemVisibility.MiniMap;

            // Add the designer canvas to the grid.
            grid1.Children.Add(this.wd.View);
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

            MultiAssign multiAssign = new MultiAssign();
            eisCategory.Add(new ToolboxItemWrapper(multiAssign.GetType(), multiAssign.GetType().Name));

            MyDynamicActivity myDynamicActivity = new MyDynamicActivity();
            eisCategory.Add(new ToolboxItemWrapper(myDynamicActivity.GetType(), myDynamicActivity.GetType().Name));

            // Add the category to the ToolBox control.
            ctrl.Categories.Add(eisCategory);
            ctrl.Categories.Add(category);
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
                // XAML 파일에서 워크플로 로드
                Activity workflow;
                using (var stream = System.IO.File.OpenRead(OpenFileDialog.FileName))
                {
                    workflow = XamlServices.Load(stream) as Activity;
                }

                // 워크플로 실행
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
