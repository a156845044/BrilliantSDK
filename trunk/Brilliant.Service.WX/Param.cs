using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brilliant.Service.WX
{
    public class AccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }

    public class Data
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }

    }

    public class DeptData : Data
    {
        public List<DeptInfo> department { get; set; }
    }

    public class DeptUserData : Data
    {
        public List<UserInfo> userlist { get; set; }
    }

    public class DeptInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string parentid { get; set; }
        public string order { get; set; }
    }

    public class UserInfo
    {
        public string userid { get; set; }

        public string name { get; set; }

        public int[] department { get; set; }
    }
}
