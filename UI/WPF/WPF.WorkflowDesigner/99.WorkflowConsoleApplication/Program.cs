using System;
using System.Activities;
using System.Activities.Statements;
using System.Linq;

namespace _99.WorkflowConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Activity workflow1 = new Workflow1();
            WorkflowInvoker.Invoke(workflow1);
        }
    }
}
