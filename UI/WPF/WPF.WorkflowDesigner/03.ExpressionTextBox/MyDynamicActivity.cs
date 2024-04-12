using System;
using System.Activities;

namespace ExpressionTextBox
{
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
}
