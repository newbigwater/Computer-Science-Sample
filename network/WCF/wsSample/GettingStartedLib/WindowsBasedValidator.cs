using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace GettingStartedLib
{
    public class WindowsBasedValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {

            // 예외가 발생하면 인증이 실패된 것임.%^
            // DB 에서 사용자 계정을 조회
            // 일단, "test", "test" 계정만 유효한 것으로 가정
            if (userName == "test" && password == "test")
            {
                return;
            }

            throw new FaultException("Test account can be authenticated ONLY.");
        }
    }
}
