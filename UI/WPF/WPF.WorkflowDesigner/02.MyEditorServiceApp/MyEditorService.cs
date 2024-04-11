using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Activities.Presentation.View;
using System.Activities.Presentation.Hosting;
using System.Activities.Presentation.Model;

namespace MyEditorServiceApp
{
    public class MyEditorService : IExpressionEditorService
    {
        public void CloseExpressionEditors()
        {

        }
        public IExpressionEditorInstance CreateExpressionEditor(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces, List<ModelItem> variables, string text)
        {
            MyExpressionEditorInstance instance = new MyExpressionEditorInstance();
            return instance;
        }
        public IExpressionEditorInstance CreateExpressionEditor(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces, List<ModelItem> variables, string text, System.Windows.Size initialSize)
        {
            MyExpressionEditorInstance instance = new MyExpressionEditorInstance();
            return instance;
        }
        public IExpressionEditorInstance CreateExpressionEditor(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces, List<ModelItem> variables, string text, Type expressionType)
        {
            MyExpressionEditorInstance instance = new MyExpressionEditorInstance();
            return instance;
        }
        public IExpressionEditorInstance CreateExpressionEditor(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces, List<ModelItem> variables, string text, Type expressionType, System.Windows.Size initialSize)
        {
            MyExpressionEditorInstance instance = new MyExpressionEditorInstance();
            return instance;
        }
        public void UpdateContext(AssemblyContextControlItem assemblies, ImportedNamespaceContextItem importedNamespaces)
        {

        }

    }
}
