using System;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            Interpreter.Context InterpreterContext = new Interpreter.Context("aabc");

            System.Collections.Generic.List<AbstractExpression> expressions = new System.Collections.Generic.List<AbstractExpression>
            {
                new TerminalExpression(),
                new NonterminalExpression(new TerminalExpression(), new TerminalExpression())
            };

            foreach (var expression in expressions)
            {
                expression.Interpret(InterpreterContext);
                Console.WriteLine($"Output: {InterpreterContext.Output}");
            }


            Console.ReadLine();
        }
    }
}
