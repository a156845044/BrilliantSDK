/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是对话框工具类。
 * 
 * 作者：zwk       时间：2015-10-13
 * ========================================================================
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Brilliant.Utility
{
    /// <summary>
    /// 对话框工具类
    /// </summary>
    public static class DialogHelper
    {
        #region 基础方法
        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="msg">提示信息内容</param>
        /// <param name="url">地址</param>
        /// <param name="type">类型</param>
        /// <param name="page">页面对象</param>
        /// <remarks>作者:zwk 时间:2014-09-25</remarks>
        public static void ShowTip(string msg, string url, DialogType type, Page page)
        {
            string script = String.Format("showTipMsg('{0}','{1}','{2}');", msg, url, type == DialogType.Default ? "" : type.ToString());
            page.ClientScript.RegisterStartupScript(page.GetType(), "showTipMsg", script, true);
        }

        /// <summary>
        /// 带回传函数的消息提示
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式 Success Error</param>
        /// <param name="callback">JS回调函数</param>
        /// <param name="page">当前Page对象</param>
        ///<remarks>作者:zwk 时间:2014-09-25</remarks>
        public static void ShowTip(string msgcontant, string url, string msgcss, string callback, Page page)
        {
            ClientScriptManager csm = page.ClientScript;
            string script = String.Format("showTipMsg('{0}','{1}','{2}', {3});", msgcontant, url, msgcss, callback);
            csm.RegisterStartupScript(page.GetType(), "showTipMsg", script, true);
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="msg">提示信息内容</param>
        /// <param name="url">地址</param>
        /// <param name="type">类型</param>
        /// <param name="callback">回传执行函数</param>
        /// <param name="page">页面对象</param>
        /// <remarks>作者:dfq 时间:2014-12-15</remarks>
        public static void ShowTipManager(string msg, string url, DialogType type, string callback, Page page)
        {
            string script = String.Format("showTipMsg('{0}','{1}','{2}', {3});", msg, url, type == DialogType.Default ? "" : type.ToString(), callback);
            ScriptManager.RegisterStartupScript(page, page.GetType(), "showTipMsg", script, true);
        }

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="msg">提示信息内容</param>
        /// <param name="url">地址</param>
        /// <param name="type">类型</param>
        /// <param name="page">页面对象</param>
        /// <remarks>作者:zwk 时间:2014-09-25</remarks>
        public static void ShowTipManager(string msg, string url, DialogType type, Page page)
        {
            string script = String.Format("showTipMsg('{0}','{1}','{2}');", msg, url, type == DialogType.Default ? "" : type.ToString());
            ScriptManager.RegisterStartupScript(page, page.GetType(), "showTipMsg", script, true);
        }

        /// <summary>
        /// 显示弹窗信息
        /// </summary>
        /// <param name="msg">弹窗提示内容</param>
        /// <param name="url">跳转地址（可为空）</param>
        /// <param name="type">类型</param>
        /// <param name="page">页面对象</param>
        /// <remarks>作者:dfq 时间:2014-10-20</remarks>
        public static void ShowDialog(string msg, string url, DialogType type, Page page)
        {
            string script = String.Format("showDialogMsg('{0}','{1}','{2}','{3}');", "消息提示", msg, url, type == DialogType.Default ? "" : type.ToString());
            page.ClientScript.RegisterStartupScript(page.GetType(), "showDialogMsg", script, true);
        }

        /// <summary>
        ///显示弹窗信息 
        /// </summary>
        /// <param name="msg">弹窗提示内容</param>
        /// <param name="url">跳转地址（可为空）</param>
        /// <param name="type">类型</param>
        /// <param name="callback">回传执行函数</param>
        /// <param name="page">页面对象</param>
        ///  <remarks>作者:dfq 时间:2014-10-20</remarks>
        public static void ShowDialog(string msg, string url, DialogType type, string callback, Page page)
        {
            string script = String.Format("showDialogMsg('{0}','{1}','{2}','{3}',{4});", "消息提示", msg, url, type == DialogType.Default ? "" : type.ToString(), callback);
            page.ClientScript.RegisterStartupScript(page.GetType(), "showDialogMsg", script, true);
        }

        /// <summary>
        /// 显示弹窗信息(ScriptManager中用)
        /// </summary>
        /// <param name="msg">弹窗提示内容</param>
        /// <param name="url">跳转地址（可为空）</param>
        /// <param name="type">类型</param>
        /// <param name="page">页面对象</param>
        /// <remarks>作者:dfq 时间:2014-10-20</remarks>
        public static void ShowDialogManager(string msg, string url, DialogType type, Page page)
        {
            string script = String.Format("showDialogMsg('{0}','{1}','{2}','{3}');", "消息提示", msg, url, type == DialogType.Default ? "" : type.ToString());
            ScriptManager.RegisterStartupScript(page, page.GetType(), "showDialogMsg", script, true);
        }

        /// <summary>
        /// 显示弹窗信息(ScriptManager中用)
        /// </summary>
        /// <param name="msg">弹窗提示内容</param>
        /// <param name="url">跳转地址（可为空）</param>
        /// <param name="type">类型</param>
        /// <param name="callback">回传执行函数</param>
        /// <param name="page">页面对象</param>
        /// <remarks>作者:dfq 时间:2014-12-15</remarks>
        public static void ShowDialogManager(string msg, string url, DialogType type, string callback, Page page)
        {
            string script = String.Format("showDialogMsg('{0}','{1}','{2}','{3}',{4});", "消息提示", msg, url, type == DialogType.Default ? "" : type.ToString(), callback);
            ScriptManager.RegisterStartupScript(page, page.GetType(), "showDialogMsg", script, true);
        }
        #endregion

        #region ScriptManager提示框
        /// <summary>
        /// 消息提示_成功（SM）
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        public static void ShowTipSuccessMsgManager(string msgcontant, string url, Page page)
        {
            ShowTipManager(msgcontant, url, DialogType.Success, page);
        }

        /// <summary>
        /// 消息提示_失败（SM）
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        public static void ShowTipErrorMsgManager(string msgcontant, string url, Page page)
        {
            ShowTipManager(msgcontant, url, DialogType.Error, page);
        }

        /// <summary>
        /// 消息提示_警告（SM）
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        public static void ShowTipWarningMsgManager(string msgcontant, string url, Page page)
        {
            ShowTipManager(msgcontant, url, DialogType.Default, page);
        }

        /// <summary>
        /// 消息提示_成功（SM）
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        /// <param name="callback">回传函数名称</param>
        ///  <remarks>作者：dfq 时间：2015-11-17</remarks>
        public static void ShowTipSuccessMsgManager(string msgcontant, string url, string callback, Page page)
        {
            ShowTipManager(msgcontant, url, DialogType.Success, callback, page);
        }

        /// <summary>
        /// 消息提示_失败（SM）
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>'
        /// <param name="callback">回传函数名称</param>
        ///  <remarks>作者：dfq 时间：2015-11-17</remarks>
        public static void ShowTipErrorMsgManager(string msgcontant, string url, string callback, Page page)
        {
            ShowTipManager(msgcontant, url, DialogType.Error, callback, page);
        }

        /// <summary>
        /// 消息提示_警告（SM）
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        /// <param name="callback">回传函数名称</param>
        ///  <remarks>作者：dfq 时间：2015-11-17</remarks>
        public static void ShowTipWarningMsgManager(string msgcontant, string url, string callback, Page page)
        {
            ShowTipManager(msgcontant, url, DialogType.Default, callback, page);
        }
        #endregion

        #region 普通提示框
        /// <summary>
        /// 消息提示_成功
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        public static void ShowTipSuccessMsg(string msgcontant, string url, Page page)
        {
            ShowTip(msgcontant, url, DialogType.Success, page);
        }

        /// <summary>
        /// 消息提示_失败
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        public static void ShowTipErrorMsg(string msgcontant, string url, Page page)
        {
            ShowTip(msgcontant, url, DialogType.Error, page);
        }

        /// <summary>
        /// 消息提示_警告
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        public static void ShowTipWarningMsg(string msgcontant, string url, Page page)
        {
            ShowTip(msgcontant, url, DialogType.Default, page);
        }

        /// <summary>
        /// 消息提示_成功
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        /// <param name="callback">回传函数名称</param>
        /// <remarks>作者：dfq 时间：2015-11-17</remarks>
        public static void ShowTipSuccessMsg(string msgcontant, string url, string callback, Page page)
        {
            ShowTip(msgcontant, url, DialogType.Success.ToString(), callback, page);
        }

        /// <summary>
        /// 消息提示_失败
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        /// <param name="callback">回传函数名称</param>
        /// <remarks>作者：dfq 时间：2015-11-17</remarks>
        public static void ShowTipErrorMsg(string msgcontant, string url, string callback, Page page)
        {
            ShowTip(msgcontant, url, DialogType.Error.ToString(), callback, page);
        }

        /// <summary>
        /// 消息提示_警告
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        /// <param name="callback">回传函数名称</param>
        /// <remarks>作者：dfq 时间：2015-11-17</remarks>
        public static void ShowTipWarningMsg(string msgcontant, string url, string callback, Page page)
        {
            ShowTip(msgcontant, url, DialogType.Default.ToString(), callback, page);
        }

        #endregion

        #region ScriptManager对话框
        /// <summary>
        /// 弹窗提示_成功
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        public static void ShowDialogSuccessMsgManager(string msgcontant, string url, Page page)
        {
            ShowDialogManager(msgcontant, url, DialogType.Success, page);
        }

        /// <summary>
        /// 弹窗提示_失败
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        public static void ShowDialogErrorMsgManager(string msgcontant, string url, Page page)
        {
            ShowDialogManager(msgcontant, url, DialogType.Error, page);
        }

        /// <summary>
        /// 弹窗提示_警告
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        public static void ShowDialogWarningMsgManager(string msgcontant, string url, Page page)
        {
            ShowDialogManager(msgcontant, url, DialogType.Default, page);
        }

        /// <summary>
        /// 弹窗提示_成功
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        /// <param name="callback">回传函数名称</param>
        ///  <remarks>作者：dfq 时间：2015-11-17</remarks>
        public static void ShowDialogSuccessMsgManager(string msgcontant, string url, string callback, Page page)
        {
            ShowDialogManager(msgcontant, url, DialogType.Success, callback, page);
        }

        /// <summary>
        /// 弹窗提示_失败
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        /// <param name="callback">回传函数名称</param>
        ///  <remarks>作者：dfq 时间：2015-11-17</remarks>
        public static void ShowDialogErrorMsgManager(string msgcontant, string url, string callback, Page page)
        {
            ShowDialogManager(msgcontant, url, DialogType.Error, callback, page);
        }

        /// <summary>
        /// 弹窗提示_警告
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        /// <param name="callback">回传函数名称</param>
        ///  <remarks>作者：dfq 时间：2015-11-17</remarks>
        public static void ShowDialogWarningMsgManager(string msgcontant, string url, string callback, Page page)
        {
            ShowDialogManager(msgcontant, url, DialogType.Default, callback, page);
        }
        #endregion

        #region 普通对话框
        /// <summary>
        /// 弹窗提示_成功
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        public static void ShowDialogSuccessMsg(string msgcontant, string url, Page page)
        {
            ShowDialog(msgcontant, url, DialogType.Success, page);
        }

        /// <summary>
        /// 弹窗提示_失败
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        public static void ShowDialogErrorMsg(string msgcontant, string url, Page page)
        {
            ShowDialog(msgcontant, url, DialogType.Error, page);
        }

        /// <summary>
        /// 弹窗提示_警告
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        public static void ShowDialogWarningMsg(string msgcontant, string url, Page page)
        {
            ShowDialog(msgcontant, url, DialogType.Default, page);
        }

        /// <summary>
        /// 弹窗提示_带回传函数
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址 可为空</param>
        /// <param name="type">Error Success 或者为空</param>
        /// <param name="callback">回调函数名称 不用带括号</param>
        /// <param name="page">当前Page对象</param>
        public static void ShowDialogMsg(string msgcontant, string url, DialogType type, string callback, Page page)
        {
            ShowDialog(msgcontant, url, type, callback, page);
        }

        /// <summary>
        /// 弹窗提示_成功
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        /// <param name="callback">回传函数名称</param>
        /// <remarks>作者：dfq 时间：2015-11-17</remarks>
        public static void ShowDialogSuccessMsg(string msgcontant, string url, string callback, Page page)
        {
            ShowDialog(msgcontant, url, DialogType.Success, callback, page);
        }

        /// <summary>
        /// 弹窗提示_失败
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        /// <param name="callback">回传函数名称</param>
        /// <remarks>作者：dfq 时间：2015-11-17</remarks>
        public static void ShowDialogErrorMsg(string msgcontant, string url, string callback, Page page)
        {
            ShowDialog(msgcontant, url, DialogType.Error, callback, page);
        }

        /// <summary>
        /// 弹窗提示_警告
        /// </summary>
        /// <param name="msgcontant">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="page">当前Page对象</param>
        /// <param name="callback">回传函数名称</param>
        /// <remarks>作者：dfq 时间：2015-11-17</remarks>
        public static void ShowDialogWarningMsg(string msgcontant, string url, string callback, Page page)
        {
            ShowDialog(msgcontant, url, DialogType.Default, callback, page);
        }
        #endregion

        /// <summary>
        /// 回传
        /// </summary>
        /// <param name="callback">回传函数名称</param>
        /// <param name="page">页面对象</param>
        /// <remarks>作者:dfq 时间:2014-12-15</remarks>
        public static void GetcallBack(string callback, Page page)
        {
            string script = String.Format("callitback({0});", callback);
            page.ClientScript.RegisterStartupScript(page.GetType(), "callitback", script, true);
        }

        /// <summary>
        /// / 回传(ScriptManager中用)
        /// </summary>
        /// <param name="callback">回传函数名称</param>
        /// <param name="page">页面对象</param>
        /// <remarks>作者:dfq 时间:2014-12-15</remarks>
        public static void GetcallBackManager(string callback, Page page)
        {
            string script = String.Format("callitback({0});", callback);
            ScriptManager.RegisterStartupScript(page, page.GetType(), "callitback", script, true);
        }
    }

    /// <summary>
    /// 对话框类型
    /// </summary>
    public enum DialogType
    {
        /// <summary>
        /// 默认警告类型
        /// </summary>
        Default,
        /// <summary>
        /// 错误类型
        /// </summary>
        Error,
        /// <summary>
        /// 成功类型
        /// </summary>
        Success
    }
}
