using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APGSGA_GPP_App
{
    class LoginData
    {
        public Dictionary<string, string> dic_login = CreateValidation();


        public static Dictionary<string, string> CreateValidation()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            //Add the usernames and Passwords
            dic.Add("zhg", "c47a2624a4d01589f9696f3bb45cff8a");
            dic.Add("admin", "bed128365216c019988915ed3add75fb");
            dic.Add("hotline", "b0700b31ce0c29f134059ebda6516ad8");

            return dic;
        }
    }
}
