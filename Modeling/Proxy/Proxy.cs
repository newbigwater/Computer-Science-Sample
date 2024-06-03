using System;

namespace Proxy
{
    public class Proxy : ISubject
    {
        private RealSubject _realSubject;

        public void Request()
        {
            if (_realSubject == null)
            {
                _realSubject = new RealSubject();
            }

            Console.WriteLine("Proxy: Logging access to RealSubject.");
            _realSubject.Request();
        }
    }
}
