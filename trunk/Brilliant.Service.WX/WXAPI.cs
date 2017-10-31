using Brilliant.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brilliant.Service.WX
{
    public class WXAPI
    {
        private static string corpId = "wxe76f5d365f6f792b";
        private static string secret = "_dGVcAgxRKm3B-PsPiCgfUQAEeNSGTd-GmUQ1TGPUE3c8GnqPkadL8j7GZR5m9vs";
        private static int rootDeptId = 1;

        public static string AccessToken
        {
            get
            {
                string url = String.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", corpId, secret);
                AccessToken at = HttpHelper.HttpGet<AccessToken>(url);
                if (at == null) throw (new Exception("获取AccessToken失败!"));
                return at.access_token;
            }
        }

        public static List<DeptInfo> GetDepts()
        {
            string url = String.Format("https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}&id={1}", AccessToken, rootDeptId);
            DeptData deptData = HttpHelper.HttpGet<DeptData>(url);
            return deptData.department;
        }

        public static List<UserInfo> GetDeptUsers(int deptId)
        {
            string url = String.Format("https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}&status={3}", AccessToken, deptId, 1, 0);
            DeptUserData deptUserData = HttpHelper.HttpGet<DeptUserData>(url);
            return deptUserData.userlist;
        }

        public static bool SendText(string msg)
        {
            string url = String.Format("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}", AccessToken);
            return true;
        }
    }
}
