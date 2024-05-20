using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMathod
{
    public abstract class AbstractClass
    {
        public void TemplateMethod()
        {
            PrimitiveOperation1();
            PrimitiveOperation2();
        }

        public abstract void PrimitiveOperation1();
        public abstract void PrimitiveOperation2();
    }

    public class ConcreteClass : AbstractClass
    {
        public override void PrimitiveOperation1()
        {
            Console.WriteLine("PrimitiveOperation1 Call !!");
        }

        public override void PrimitiveOperation2()
        {
            Console.WriteLine("PrimitiveOperation2 Call !!");
        }
    }
}
