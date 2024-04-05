using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingStartedHost
{
    public class MyService : IMyService
    {
        public string SayHello(string name)
        {
            return $"Hello, {name}!";
        }
    }
}
