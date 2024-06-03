using System;
using System.Linq;

namespace Interpreter
{
    public abstract class AbstractExpression
    {
        public abstract void Interpret(Context context);
    }

    public class TerminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            Console.WriteLine("TerminalExpression: Interpreting context.");
            // 간단한 예로, 입력 문자열에 포함된 'a'의 개수를 출력으로 설정
            context.Output = context.Input.Count(c => c == 'a');
        }
    }

    public class NonterminalExpression : AbstractExpression
    {
        private AbstractExpression _expression1;
        private AbstractExpression _expression2;

        public NonterminalExpression(AbstractExpression expression1, AbstractExpression expression2)
        {
            _expression1 = expression1;
            _expression2 = expression2;
        }

        public override void Interpret(Context context)
        {
            Console.WriteLine("NonterminalExpression: Interpreting context.");
            _expression1.Interpret(context);
            _expression2.Interpret(context);
            // 예를 들어, 두 개의 표현식의 출력을 더함
            context.Output *= 2;
        }
    }
}
