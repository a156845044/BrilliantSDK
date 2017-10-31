/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是GridView操作工具类。
 * 
 * 作者：zwk       时间：2013-11-12
 * ========================================================================
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Brilliant.Utility
{
    /// <summary>
    /// GridView操作工具类
    /// </summary>
    public static class GridViewHelper
    {
        /// <summary>
        /// 为GridView添加光棒效果
        /// </summary>
        /// <param name="e">GridView事件参数</param>
        /// <param name="zebraLineColor">光棒效果背景色 如：#CCC #EFEFEF Red等</param>
        public static void ZebraLineEffect(GridViewRowEventArgs e, string zebraLineColor)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", String.Format("currentColor=this.style.backgroundColor;this.style.backgroundColor='{0}';", zebraLineColor));
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentColor;");
            }
        }

        /// <summary>
        /// 合并GridView指定列中相同内容的行
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="colNum">指定列</param>
        public static void GroupRows(GridView gridView, int colNum)
        {
            int i = 0, rowSpanNum = 1;
            while (i < gridView.Rows.Count - 1)
            {
                GridViewRow gvr = gridView.Rows[i];
                for (++i; i < gridView.Rows.Count; i++)
                {
                    GridViewRow gvrNext = gridView.Rows[i];
                    if (gvr.Cells[colNum].Text == gvrNext.Cells[colNum].Text && !String.IsNullOrEmpty(gvr.Cells[colNum].Text))
                    {
                        gvrNext.Cells[colNum].Visible = false;
                        rowSpanNum++;
                    }
                    else
                    {
                        gvr.Cells[colNum].RowSpan = rowSpanNum;
                        rowSpanNum = 1;
                        break;
                    }
                    if (i == gridView.Rows.Count - 1)
                    {
                        gvr.Cells[colNum].RowSpan = rowSpanNum;
                    }
                }
            }
        }

        /// <summary>
        /// 根据条件合并GridView指定列中相同内容的行
        /// </summary>
        /// <param name="gridView">GridView</param>
        /// <param name="colNum">指定列</param>
        /// <param name="conditionCol">条件列</param>
        public static void GroupRows(GridView gridView, int colNum, int conditionCol)
        {
            int i = 0, rowSpanNum = 1;
            while (i < gridView.Rows.Count - 1)
            {
                GridViewRow gvr = gridView.Rows[i];
                for (++i; i < gridView.Rows.Count; i++)
                {
                    GridViewRow gvrNext = gridView.Rows[i];
                    if (gvr.Cells[colNum].Text + gvr.Cells[conditionCol].Text == gvrNext.Cells[colNum].Text + gvrNext.Cells[conditionCol].Text)
                    {
                        gvrNext.Cells[colNum].Visible = false;
                        rowSpanNum++;
                    }
                    else
                    {
                        gvr.Cells[colNum].RowSpan = rowSpanNum;
                        rowSpanNum = 1;
                        break;
                    }
                    if (i == gridView.Rows.Count - 1)
                    {
                        gvr.Cells[colNum].RowSpan = rowSpanNum;
                    }
                }
            }
        }
    }
}
