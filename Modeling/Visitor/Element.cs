using System;

namespace Visitor
{
    public abstract class Element
    {
        public abstract void Accept(IVisitor visitor);
    }

    public class ConcreteElementA : Element
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteElementA(this);
        }

        public void OperationA()
        {
            Console.WriteLine("Operation A");
        }
    }

    public class ConcreteElementB : Element
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteElementB(this);
        }

        public void OperationB()
        {
            Console.WriteLine("Operation B");
        }
    }
}
