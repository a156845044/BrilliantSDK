/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是提示框工具类。
 * 
 * 作者：zwk       时间：2013-11-12
 * ========================================================================
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Brilliant.Utility
{
    /// <summary>
    /// 提示框工具类
    /// </summary>
    public static class MsgBoxHelper
    {
        /// <summary>
        /// 在UpdatePanel中弹出提示框
        /// </summary>
        /// <param name="str">提示框显示文本</param>
        /// <param name="page">当前页面对象</param>
        public static void ShowUpdatePanelMsgBox(string str, Page page)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "", String.Format("alert('{0}');", str), true);
        }

        /// <summary>
        /// 在UpdatePanel中弹出提示框，并将当前页面导向指定页面
        /// </summary>
        /// <param name="str">提示框显示文本</param>
        /// <param name="url">跳转路径</param>
        /// <param name="page">当前页面对象</param>
        public static void ShowUpdatePanelMsgBoxAndRedirect(string str, string url, Page page)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "", String.Format("alert('{0}');location.href='{1}';", str, url), true);
        }

        /// <summary>
        /// 在UpdatePanel中弹出提示框，并将当前页面导向指定页面(适用于跳出框架页)
        /// </summary>
        /// <param name="str">提示框显示文本</param>
        /// <param name="url">跳转路径</param>
        /// <param name="page">当前页面对象</param>
        public static void ShowUpdatePanelMsgBoxAndRedirectFrame(string str, string url, Page page)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "", String.Format("alert('{0}');top.location.href='{1}';", str, url), true);
        }

        /// <summary>
        /// 弹出提示框
        /// </summary>
        /// <param name="str">提示框显示文本</param>
        /// <param name="page">当前页面对象</param>
        public static void ShowMsgBox(string str, Page page)
        {
            String csname = "PopupScript";
            Type cstype = page.GetType();
            ClientScriptManager csm = page.ClientScript;
            if (!csm.IsStartupScriptRegistered(cstype, csname))
            {
                String cstext = String.Format("<script language=javascript>alert('{0}');</script>", str);
                csm.RegisterStartupScript(cstype, csname, cstext, false);
            }
        }

        /// <summary>
        /// 弹出提示框，并将当前页面导向指定页面
        /// </summary>
        /// <param name="str">提示框显示文本</param>
        /// <param name="url">跳转路径</param>
        /// <param name="page">当前页面对象</param>
        public static void ShowMsgAndRedirect(string str, string url, Page page)
        {
            String csname = "PopupAndRedirectScript";
            Type cstype = page.GetType();
            ClientScriptManager csm = page.ClientScript;
            if (!csm.IsStartupScriptRegistered(cstype, csname))
            {
                String cstext = String.Format("<script language=javascript>alert('{0}');location.href='{1}';</script>", str, url);
                csm.RegisterStartupScript(cstype, csname, cstext, false);
            }
        }

        /// <summary>
        /// 弹出提示框，并将当前页面导向指定页面(适用于跳出框架页)
        /// </summary>
        /// <param name="str">提示框显示文本</param>
        /// <param name="url">跳转路径</param>
        /// <param name="page">当前页面对象</param>
        public static void ShowMsgAndRedirectFrame(string str, string url, Page page)
        {
            String csname = "PopupAndRedirectScript";
            Type cstype = page.GetType();
            ClientScriptManager csm = page.ClientScript;
            if (!csm.IsStartupScriptRegistered(cstype, csname))
            {
                String cstext = String.Format("<script language=javascript>alert('{0}');top.location.href='{1}';</script>", str, url);
                csm.RegisterStartupScript(cstype, csname, cstext, false);
            }
        }
    }
}
