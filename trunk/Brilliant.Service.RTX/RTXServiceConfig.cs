using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Brilliant.Service.RTX
{
    public static class RTXServiceConfig
    {
        private static bool loaded = false;
        private static string serverIP;

        /// <summary>
        /// 服务IP
        /// </summary>
        public static string ServerIP
        {
            get { return loaded ? serverIP : "127.0.0.1"; }
        }
        private static short serverPort;

        /// <summary>
        /// 服务端口
        /// </summary>
        public static short ServerPort
        {
            get { return loaded ? serverPort : Convert.ToInt16(8006); }
        }

        static RTXServiceConfig()
        {
            string config = ConfigurationManager.AppSettings["rtx"];
            if (!String.IsNullOrEmpty(config))
            {
                string[] configs = config.Split(',');
                if (config.Length > 2)
                {
                    loaded = true;
                    serverIP = configs[0];
                    serverPort = Convert.ToInt16(configs[1]);
                }
            }
        }
    }
}
