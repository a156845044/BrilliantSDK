using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliant.Web.UI
{
    public class Dialog
    {
        private const string DEFAULT_TITLE = "提示";
        private JsonState _jsonState = new JsonState();

        public string Cls
        {
            get { return (string)_jsonState["cls"]; }
            set { _jsonState["cls"] = value; }
        }

        public string ID
        {
            get { return (string)_jsonState["id"]; }
            set { _jsonState["id"] = value; }
        }

        //public string[] Buttons

        public bool? IsDrag
        {
            get { return (bool?)_jsonState["isDrag"]; }
            set { _jsonState["isDrag"] = value; }
        }

        public int? Width
        {
            get { return (int?)_jsonState["width"]; }
            set { _jsonState["width"] = value; }
        }

        public int? Height
        {
            get { return (int?)_jsonState["height"]; }
            set { _jsonState["height"] = value; }
        }

        public string Content
        {
            get { return (string)_jsonState["content"]; }
            set { _jsonState["content"] = value; }
        }

        public string TargetID
        {
            get { return (string)_jsonState["targetId"]; }
            set { _jsonState["targetId"] = value; }
        }

        public string Url
        {
            get { return (string)_jsonState["url"]; }
            set { _jsonState["url"] = value; }
        }

        public bool? Load
        {
            get { return (bool?)_jsonState["load"]; }
            set { _jsonState["load"] = value; }
        }

        public DialogType Type
        {
            get { return _jsonState["type"] == null ? DialogType.None : (DialogType)_jsonState["type"]; }
            set { _jsonState["type"] = value; }
        }

        public int? Left
        {
            get { return (int?)_jsonState["left"]; }
            set { _jsonState["left"] = value; }
        }

        public int? Top
        {
            get { return (int?)_jsonState["top"]; }
            set { _jsonState["top"] = value; }
        }

        public bool? Modal
        {
            get { return (bool?)_jsonState["modal"]; }
            set { _jsonState["modal"] = value; }
        }

        public string Name
        {
            get { return (string)_jsonState["name"]; }
            set { _jsonState["name"] = value; }
        }

        public bool? IsResize
        {
            get { return (bool?)_jsonState["isResize"]; }
            set { _jsonState["isResize"] = value; }
        }

        public bool? AllowClose
        {
            get { return (bool?)_jsonState["allowClose"]; }
            set { _jsonState["allowClose"] = value; }
        }

        //public object opener;

        public string TimeParmName
        {
            get { return (string)_jsonState["timeParmName"]; }
            set { _jsonState["timeParmName"] = value; }
        }

        public bool? CloseWhenEnter
        {
            get { return (bool?)_jsonState["closeWhenEnter"]; }
            set { _jsonState["closeWhenEnter"] = value; }
        }

        public bool? IsHidden
        {
            get { return (bool?)_jsonState["isHidden"]; }
            set { _jsonState["isHidden"] = value; }
        }

        public bool? Show
        {
            get { return (bool?)_jsonState["show"]; }
            set { _jsonState["show"] = value; }
        }

        public string Title
        {
            get { return _jsonState["title"] == null ? DEFAULT_TITLE : (string)_jsonState["title"]; }
            set { _jsonState["title"] = value; }
        }

        public bool? ShowMax
        {
            get { return (bool?)_jsonState["showMax"]; }
            set { _jsonState["showMax"] = value; }
        }

        public bool? ShowToggle
        {
            get { return (bool?)_jsonState["showToggle"]; }
            set { _jsonState["showToggle"] = value; }
        }

        public bool? ShowMin
        {
            get { return (bool?)_jsonState["showMin"]; }
            set { _jsonState["showMin"] = value; }
        }

        public bool? Slide
        {
            get { return (bool?)_jsonState["slide"]; }
            set { _jsonState["slide"] = value; }
        }

        public string FixedType
        {
            get { return (string)_jsonState["fixedType"]; }
            set { _jsonState["fixedType"] = value; }
        }

        public string ShowType
        {
            get { return (string)_jsonState["showType"]; }
            set { _jsonState["showType"] = value; }
        }

        public string ContentCls
        {
            get { return (string)_jsonState["contentCls"]; }
            set { _jsonState["contentCls"] = value; }
        }

        //public object urlParms;

        public int? LayoutMode
        {
            get { return (int?)_jsonState["layoutMode"]; }
            set { _jsonState["layoutMode"] = value; }
        }

        public void Open()
        {
            if (!String.IsNullOrEmpty(TargetID))
            {
                _jsonState.AddProperty("target", String.Format("$(\"#{0}\")", TargetID));
            }
            string script = String.Format("$.ligerDialog.open({0});", _jsonState.Serialize());
            ScriptManager.Instance.AddExtraScript(script);
        }

        public static void Alert(string content, string title, DialogType type)
        {
            string script = String.Format("$.ligerDialog.alert('{0}', '{1}', '{2}');", content, title, type.ToString().ToLower());
            ScriptManager.Instance.AddExtraScript(script);
        }

        public static void Alert(string content, DialogType type)
        {
            Alert(content, DEFAULT_TITLE, type);
        }

        public static void Success(string content)
        {
            Alert(content, DialogType.Success);
        }

        public static void Warn(string content)
        {
            Alert(content, DialogType.Warn);
        }

        public static void Question(string content)
        {
            Alert(content, DialogType.Question);
        }

        public static void Error(string content)
        {
            Alert(content, DialogType.Error);
        }

        public static void Confirm(string content)
        {
            Alert(content, DialogType.Confirm);
        }

        public static void Warning(string content)
        {
            Alert(content, DialogType.Warning);
        }

        public static void Prompt(string content)
        {
            Alert(content, DialogType.Prompt);
        }

        public static void Waitting(string content)
        {
            Alert(content, DialogType.Waitting);
        }
    }

    public enum DialogType
    {
        None,
        Confirm,
        Error,
        Prompt,
        Prompt2,
        Prompt3,
        Prompt4,
        Question,
        Success,
        Waitting,
        Waitting2,
        Warn,
        Warning
    }
}
