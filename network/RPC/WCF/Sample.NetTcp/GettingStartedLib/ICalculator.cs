using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GettingStartedLib
{
    [ServiceContract]
    public interface ICalculator
    {
        [OperationContract]
        CalculateResult Add(double n1, double n2);
        [OperationContract]
        CalculateResult Subtract(double n1, double n2);
        [OperationContract]
        CalculateResult Multiply(double n1, double n2);
        [OperationContract]
        CalculateResult Divide(double n1, double n2);
    }

    [DataContract] // 형식에 데이터 계약이 있음을 선언
    public class CalculateResult
    {
        private double _resultVal = 0.0;

        [DataMember]
        public double ResultVal
        {
            get { return _resultVal; }
            set { _resultVal = value; }
        }
    }

}
