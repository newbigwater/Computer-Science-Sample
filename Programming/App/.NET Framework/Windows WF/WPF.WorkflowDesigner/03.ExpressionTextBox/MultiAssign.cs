using System;
using System.Activities;
using System.ComponentModel;

namespace ExpressionTextBox
{
    // Sets To1 = Value1 and To2 = Value2
    [Designer(typeof(MultiAssignDesigner))]
    public class MultiAssign : CodeActivity
    {
        public OutArgument<String> To1 { get; set; }
        public OutArgument<String> To2 { get; set; }
        public InArgument<String> Value1 { get; set; }
        public InArgument<String> Value2 { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            throw new NotImplementedException();
        }
    }
}
