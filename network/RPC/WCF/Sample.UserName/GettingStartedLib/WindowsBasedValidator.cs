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
            if (userName == "THiRA" && password == "UTECH")
            {
                return;
            }

            throw new FaultException("THiRA account can be authenticated ONLY.");
        }
    }
}
