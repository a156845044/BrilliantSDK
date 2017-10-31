using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTXSAPILib;

namespace Brilliant.Service.RTX
{
    /// <summary>
    /// 腾讯通服务接口(服务端)
    /// </summary>
    public class RTXService
    {
        private static readonly RTXService service = new RTXService();

        /// <summary>
        /// 服务实例
        /// </summary>
        public static RTXService Service
        {
            get { return RTXService.service; }
        }

        private RTXSAPIRootObj rootObj;
        private RTXDept rtxDept;
        private RTXUser rtxUser;

        /// <summary>
        /// 
        /// </summary>
        public RTXDept RtxDept
        {
            get { return rtxDept; }
        }

        /// <summary>
        /// 
        /// </summary>
        public RTXUser RtxUser
        {
            get { return rtxUser; }
        }

        /// <summary>
        /// 服务IP
        /// </summary>
        public string ServerIP
        {
            get { return rootObj.ServerIP; }
            set { rootObj.ServerIP = value; }
        }

        /// <summary>
        /// 服务端口
        /// </summary>
        public short ServerPort
        {
            get { return rootObj.ServerPort; }
            set { rootObj.ServerPort = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private RTXService()
        {
            this.rootObj = new RTXSAPIRootObj();
            this.rootObj.ServerIP = RTXServiceConfig.ServerIP;
            this.rootObj.ServerPort = RTXServiceConfig.ServerPort;
            rtxDept = new RTXDept(rootObj);
            rtxUser = new RTXUser(rootObj);
        }

        /// <summary>
        /// 发送消息弹窗
        /// </summary>
        /// <param name="receivers">接收人账号(多个用,隔开)</param>
        /// <param name="title">消息标题</param>
        /// <param name="msg">消息内容</param>
        /// <remarks>作者:zwk 时间:2015-09-06</remarks>
        public static void SendNotify(string receivers, string title, string msg)
        {
            service.rootObj.SendNotify(receivers, title, 0, msg);
        }

        /// <summary>
        /// 发送消息弹窗
        /// </summary>
        /// <param name="receivers">接收人账号(多个用,隔开)</param>
        /// <param name="msg">消息内容</param>
        /// <remarks>作者:zwk 时间:2015-09-06</remarks>
        public static void SendNotify(string receivers, string msg)
        {
            SendNotify(receivers, "系统提示", msg);
        }

        /// <summary>
        /// 发送即时消息
        /// </summary>
        /// <param name="sender">发送方</param>
        /// <param name="senderPwd">发送方密码</param>
        /// <param name="receivers">接收方(多个用,隔开)</param>
        /// <param name="msg">消息内容</param>
        /// <remarks>作者:zwk 时间:2015-09-06</remarks>
        public static void SendIM(string sender, string senderPwd, string receivers, string msg)
        {
            Guid guid = Guid.NewGuid();
            service.rootObj.SendIM(sender, senderPwd, receivers, msg, guid.ToString("B"));
        }
    }
}
