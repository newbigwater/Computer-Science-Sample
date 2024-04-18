using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeepAlive
{
    public class StringClass : IDisposable
    {
        private byte[] bytes = new byte[10];

        public byte[] Bytes
        {
            get => bytes;
            private set => bytes = value;
        }

        public string Value
        {
            get => Encoding.ASCII.GetString(Bytes);
            set => Bytes = Encoding.ASCII.GetBytes(value);
        }
        

        public void Dispose()
        {
            this.bytes = null;
        }
    }

    public class KeepAliveTestClass
    {
        private string funcName = "";
        private StringClass strClass = new StringClass();

        public StringClass Value
        {
            get => strClass;
            set => strClass = value;
        }

        public KeepAliveTestClass(string funcName = "")
        {
            this.funcName = funcName;
            strClass.Value = "0123456789";
        }

        ~KeepAliveTestClass()
        {
            strClass.Dispose();
            Console.WriteLine($"[{this.funcName}] GC");
        }

        public void MyMethod_Before()
        {
            Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] Start");

            KeepAliveTestClass obj = new KeepAliveTestClass(System.Reflection.MethodBase.GetCurrentMethod().Name);
            StringClass obj2 = obj.Value;// 이 코드 이후로는 obj 개체를 참조하지 않음!

            Task.Run(() =>
            {
                try
                {
                    Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] Task.Run Start");

                    // ... 만약, 바로 이 시점에 GC가 호출된다면?
                    Thread.Sleep(1000);

                    // obj still alive here? Possibly not.
                    TestMethod(obj2);
                    Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] Task.Run End");
                }
                catch (Exception exp)
                {
                    Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] {exp.Message}");
                }
            });

            GC.Collect();
            Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] end");
        }

        public void MyMethod_After()
        {
            Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] Start");

            KeepAliveTestClass obj = new KeepAliveTestClass(System.Reflection.MethodBase.GetCurrentMethod().Name);
            StringClass obj2 = obj.Value;// 이 코드 이후로는 obj 개체를 참조하지 않음!
            GC.KeepAlive(obj2);

            Task.Run(() =>
            {
                try
                {
                    Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] Task.Run Start");

                    // ... 만약, 바로 이 시점에 GC가 호출된다면?
                    Thread.Sleep(1000);

                    // obj still alive here? Possibly not.
                    TestMethod(obj2);
                    Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] Task.Run End");
                }
                catch (Exception exp)
                {
                    Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] {exp.Message}");
                }
            });

            GC.Collect();
            Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] end");
        }

        void TestMethod(StringClass obj2, [CallerMemberName] string funcName = "")
        {
            Console.WriteLine($"[{funcName}] Value : {obj2.Value}");
        }
    }
}
