using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using Brilliant.Data.Common;

namespace Brilliant.ProjectStudio
{
    public class Export
    {
        private object endRange = @"\endofdoc";
        private object missing = Missing.Value;
        private Word.Application word;
        private Word.Document doc;

        public Export()
        {
            this.word = new Word.ApplicationClass();
            this.doc = word.Documents.Add(ref missing, ref missing, ref missing, ref missing);
            this.doc.SpellingChecked = false;
            this.doc.ShowSpellingErrors = false;
        }

        public void AddBasicInfo(string dbName)
        {
            this.SetPage(false);
            this.AddHeader("江阴市普瑞利安信息科技有限公司");
            this.AddFooter();
            this.AddParagraph("数据库名：" + dbName, "华文中宋", 1, 18f, 1f, 5f, WdParagraphAlignment.wdAlignParagraphCenter);
        }

        public void ExportSchemaInfo(int i, SchemaTable dbTable)
        {
            //this.AddParagraph("", "宋体", 0, 10.5f, 0f, 0f, WdParagraphAlignment.wdAlignParagraphLeft);
            string title = String.Format("{0}.表名：{1} {2}", i, dbTable.TableName, String.IsNullOrEmpty(dbTable.TableDesc) ? String.Empty : String.Format("（{0}）", dbTable.TableDesc));
            this.AddTitle(title);
            this.AddTable(dbTable.ColumnList);
        }

        public void ShowWord()
        {
            word.Visible = true;
        }

        /// <summary>
        /// 设置页面（横向、纵向、页边距）
        /// </summary>
        private void SetPage(bool isVertical)
        {
            word.ActiveDocument.PageSetup.Orientation = isVertical ? WdOrientation.wdOrientPortrait : Word.WdOrientation.wdOrientLandscape;
        }

        /// <summary>
        /// 设置页眉
        /// </summary>
        private void AddHeader(string headerText)
        {
            word.ActiveWindow.View.Type = WdViewType.wdOutlineView;
            word.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
            word.ActiveWindow.ActivePane.Selection.InsertAfter(headerText);
            word.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            word.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument;
        }

        /// <summary>
        /// 设置页脚和页码
        /// </summary>
        private void AddFooter()
        {
            WdHeaderFooterIndex wdFooterIndex = WdHeaderFooterIndex.wdHeaderFooterPrimary;
            object align = WdPageNumberAlignment.wdAlignPageNumberCenter;
            object firstPage = true;
            word.Selection.Sections[1].Footers[wdFooterIndex].PageNumbers.Add(ref align, ref firstPage);
            word.Selection.Sections[1].Footers[wdFooterIndex].PageNumbers.NumberStyle = WdPageNumberStyle.wdPageNumberStyleNumberInDash;
        }

        /// <summary>
        /// 添加段落
        /// </summary>
        private void AddParagraph(string titleText, string fontName, int bold, float size, float spaceBefore, float spaceAfter, WdParagraphAlignment align)
        {
            Paragraph paragraph = doc.Content.Paragraphs.Add(ref missing);
            paragraph.Range.Text = titleText;
            object style = WdBuiltinStyle.wdStyleBodyText;
            paragraph.Range.Text = titleText;
            paragraph.Range.Font.Bold = bold;
            paragraph.Range.Font.Name = fontName;
            paragraph.Range.Font.Size = size;
            paragraph.Range.ParagraphFormat.Alignment = align;
            paragraph.Format.SpaceBefore = spaceBefore;
            paragraph.Format.SpaceAfter = spaceAfter;
            paragraph.Range.InsertParagraphAfter();
        }

        /// <summary>
        /// 添加标题
        /// </summary>
        /// <param name="titleText">标题内容</param>
        private void AddTitle(string titleText)
        {
            //"宋体", 1, 12f, 1.5f, 1.5f, WdParagraphAlignment.wdAlignParagraphLeft
            // 宋体 3号 居左 加粗 段前:12 段后:3 
            Paragraph paragraph = doc.Content.Paragraphs.Add(ref missing);
            paragraph.Range.Text = titleText;
            object style = WdBuiltinStyle.wdStyleTitle;
            paragraph.set_Style(ref style);
            paragraph.Range.Font.Bold = 1;
            paragraph.Range.Font.Name = "宋体";
            paragraph.Range.Font.Size = 16f;
            paragraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            paragraph.Format.SpaceBefore = 12f;
            paragraph.Format.SpaceAfter = 3f;
            paragraph.Range.InsertParagraphAfter();
        }

        /// <summary>
        /// 添加表格
        /// </summary>
        private void AddTable(IList<SchemaColumn> fieldList)
        {
            Range range = doc.Bookmarks[endRange].Range;
            Table table = doc.Tables.Add(range, fieldList.Count + 1, 11, ref missing, ref missing);
            table.Range.Font.Name = "宋体";
            table.Borders.Enable = 1;
            table.Rows.Height = 15f;
            table.AutoFitBehavior(WdAutoFitBehavior.wdAutoFitContent);

            table.Rows.First.Range.Font.Bold = 1;
            table.Rows.First.Range.Font.Size = 10.5f;
            table.Rows.First.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            table.Rows.First.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            table.Rows.First.Shading.Texture = WdTextureIndex.wdTexture25Percent;
            table.Cell(1, 1).Range.Text = "序号";
            table.Cell(1, 2).Range.Text = "列名";
            table.Cell(1, 3).Range.Text = "数据类型";
            table.Cell(1, 4).Range.Text = "长度";
            table.Cell(1, 5).Range.Text = "自增";
            table.Cell(1, 6).Range.Text = "主键";
            table.Cell(1, 7).Range.Text = "外键";
            table.Cell(1, 8).Range.Text = "外键表";
            table.Cell(1, 9).Range.Text = "允许空";
            table.Cell(1, 10).Range.Text = "默认值";
            table.Cell(1, 11).Range.Text = "说明";

            int i = 2;
            int count = 0;
            foreach (SchemaColumn field in fieldList)
            {
                count++;
                table.Rows[i].Range.Font.Bold = 0;
                table.Rows[i].Range.Font.Size = 10.5f;
                table.Rows[i].Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                table.Cell(i, 1).Range.Text = count.ToString();
                table.Cell(i, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                table.Cell(i, 2).Range.Text = field.ColumnName;
                table.Cell(i, 3).Range.Text = field.ColumnType;
                table.Cell(i, 4).Range.Text = field.ColumnLength == -1 ? "MAX" : field.ColumnLength.ToString();
                table.Cell(i, 4).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                table.Cell(i, 5).Range.Text = field.IsIdentity ? "√" : String.Empty;
                table.Cell(i, 5).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                table.Cell(i, 6).Range.Text = field.IsPK ? "√" : String.Empty;
                table.Cell(i, 6).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                table.Cell(i, 7).Range.Text = field.IsFK ? "√" : String.Empty;
                table.Cell(i, 7).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                table.Cell(i, 8).Range.Text = field.FkTableName;
                table.Cell(i, 9).Range.Text = field.IsNull ? "√" : String.Empty;
                table.Cell(i, 9).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                table.Cell(i, 10).Range.Text = field.ColumnDefaultValue;
                table.Cell(i, 10).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                table.Cell(i, 11).Range.Text = field.ColumnDesc;
                i++;
            }
            table.AutoFitBehavior(WdAutoFitBehavior.wdAutoFitWindow);
        }
    }
}
