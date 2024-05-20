using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GettingStartedLib
{
    [ServiceContract(
        CallbackContract = typeof(IServiceReply),
        SessionMode = SessionMode.Required
        )]
    public interface ICalculator
    {
        [OperationContract]
        double Add(double n1, double n2);

        [OperationContract]
        double Subtract(double n1, double n2);

        [OperationContract]
        double Multiply(double n1, double n2);

        [OperationContract]
        double Divide(double n1, double n2);

        [OperationContract(IsOneWay = true)]
        void Add1(double n1, double n2);
    }

    public interface IServiceReply
    {
        [OperationContract(IsOneWay = true)]
        void Reply(double result);
    }
}
