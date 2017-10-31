using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace Brilliant.ProjectStudio
{
    /// <summary>
    /// 资源管理
    /// </summary>
    public static class ResManager
    {
        private static readonly ImageList sysImageList = new ImageList();

        /// <summary>
        /// 系统图标枚举器
        /// </summary>
        public static ImageList SysImageList
        {
            get { return ResManager.sysImageList; }
        }

        /// <summary>
        /// 构造器
        /// </summary>
        static ResManager()
        {
            sysImageList.ColorDepth = ColorDepth.Depth32Bit;
            sysImageList.ImageSize = new Size(16, 16);
            sysImageList.TransparentColor = Color.Transparent;
            Type type = Type.GetType("Brilliant.ProjectStudio.ResImageList", false);
            PropertyInfo[] prop = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Static);
            foreach (PropertyInfo p in prop)
            {
                if (p.Name == "Culture" || p.Name == "ResourceManager")
                {
                    continue;
                }
                sysImageList.Images.Add(p.Name, (Bitmap)p.GetValue(null, null));
            }
        }

        /// <summary>
        /// 获取图标资源
        /// </summary>
        /// <returns></returns>
        public static ImageList GetFileIcons()
        {
            ImageList fileImages = new ImageList();
            fileImages.ColorDepth = ColorDepth.Depth32Bit;
            fileImages.ImageSize = new Size(16, 16);
            fileImages.TransparentColor = Color.Transparent;
            Type type = Type.GetType("Brilliant.ProjectStudio.ResFileIcon", false);
            PropertyInfo[] prop = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Static);
            foreach (PropertyInfo p in prop)
            {
                if (p.Name == "Culture" || p.Name == "ResourceManager")
                {
                    continue;
                }
                fileImages.Images.Add(p.Name, (Bitmap)p.GetValue(null, null));
            }
            return fileImages;
        }
    }
}
