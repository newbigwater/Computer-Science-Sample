using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace soapServer
{
    /// <summary>
    /// Example의 요약 설명입니다.
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "SoapServer")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // ASP.NET AJAX를 사용하여 스크립트에서 이 웹 서비스를 호출하려면 다음 줄의 주석 처리를 제거합니다. 
    // [System.Web.Script.Services.ScriptService]
    public class SimpleSoapServer : System.Web.Services.WebService
    {
        // 접속자 address를 넣기 위한 list
        private static List<String> list = new List<string>();

        // 접속 context, request, response를 받기 위한 맴버 함수
        private HttpContext context;

        public SimpleSoapServer()
        {
            this.context = HttpContext.Current;
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int Count()
        {
            return list.Count;
        }

        [WebMethod]
        public List<String> GetCurrentors(string name)
        {
            var address = $@"{this.context.Request.UserHostAddress} ({name})";
            if (!list.Where(x => x.Equals(address)).Any())
                list.Add(address);
            return list;
        }

        [WebMethod]
        public int Add(int a, int b)
        {
            return a + b;
        }

        [WebMethod]
        public int Sub(int a, int b)
        {
            return a - b;
        }

        [WebMethod]
        public int Div(int a, int b)
        {
            return a / b;
        }

        [WebMethod]
        public int Mul(int a, int b)
        {
            return a * b;
        }
    }
}
