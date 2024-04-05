using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GettingStartedHost
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        string SayHello(string name);
    }
}
