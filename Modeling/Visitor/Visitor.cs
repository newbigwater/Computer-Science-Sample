using System;

namespace Visitor
{
    public interface IVisitor
    {
        void VisitConcreteElementA(ConcreteElementA elementA);
        void VisitConcreteElementB(ConcreteElementB elementB);
    }

    public class ConcreteVisitor1 : IVisitor
    {
        public void VisitConcreteElementA(ConcreteElementA elementA)
        {
            Console.WriteLine("ConcreteVisitor1 visiting ConcreteElementA");
        }

        public void VisitConcreteElementB(ConcreteElementB elementB)
        {
            Console.WriteLine("ConcreteVisitor1 visiting ConcreteElementB");
        }
    }

    public class ConcreteVisitor2 : IVisitor
    {
        public void VisitConcreteElementA(ConcreteElementA elementA)
        {
            Console.WriteLine("ConcreteVisitor2 visiting ConcreteElementA");
        }

        public void VisitConcreteElementB(ConcreteElementB elementB)
        {
            Console.WriteLine("ConcreteVisitor2 visiting ConcreteElementB");
        }
    }
}
