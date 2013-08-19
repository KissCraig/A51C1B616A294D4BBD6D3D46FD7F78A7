using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using KCPlayer.Plugin.LiuXing.Controls;
using KCPlayer.Plugin.LiuXing.LiuXing;
using KCPlayer.Plugin.LiuXing.Properties;

namespace KCPlayer.Plugin.LiuXing
{
    public class MainInterFace
    {
        #region FixedField

        public static Control Owner { get; set; }
        public Control OwnerParent { get; set; }

        public Image CustomDrawing { get; set; }

        public Guid Guid { get; set; }

        public static OuterMethod OuterInvoke { get; set; }

        #endregion

        #region FixedMethod

        /// <summary>
        ///     被添加到父容器时触发
        /// </summary>
        /// <param name="control"></param>
        public MainInterFace(Control control)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            object[] attrs = assembly.GetCustomAttributes(typeof (GuidAttribute), false);
            Guid = new Guid(((GuidAttribute) attrs[0]).Value);
            OwnerParent = control;
            OuterInvoke = new OuterMethod(this);
        }

        /// <summary>
        ///     每次激活时触发
        /// </summary>
        /// <param name="owner">panel容器</param>
        public void Main(Control owner)
        {
            Owner = owner;
            Init();
        }

        #endregion

        #region 当绘制方式为Custome的时候即可使用

        public void MouseMove(MouseEventArgs arg)
        {
        }

        public void MouseLeave()
        {
        }

        public void MouseEnter()
        {
        }

        public void MouseDown(MouseEventArgs arg)
        {
        }

        public void MouseUp(MouseEventArgs arg)
        {
        }

        public void MouseDoubleClick(MouseEventArgs arg)
        {
        }

        #endregion

        /// <summary>
        ///     当磁铁添加进入后触发事件
        /// </summary>
        public void Shown()
        {
        }


        /// <summary>
        ///     每次激活时触发
        /// </summary>
        public void Init()
        {
            // 加载字体
            if (PublicStatic.SegoeFont == null)
            {
                PublicStatic.SegoeFont = @"微软雅黑";
                //PublicStatic.SegoeFont = new FontsHelper().GetFont("Segoe WP Light.TTF");
            }
            // 设置全局
            Owner.Size = Owner.Parent.Size = new Size(1020, 563+30);
            Owner.BackgroundImageLayout = Owner.Parent.BackgroundImageLayout = ImageLayout.Stretch;
            PublicStatic.LiuXingPal = new EPanel
                {
                    Dock = DockStyle.Fill,
                };
            Owner.Controls.Add(PublicStatic.LiuXingPal);
            // 开始加载
            try
            {
                LiuXingStart.LoadLiuXing();
            }
            catch
            {
                
            }
        }

        /// <summary>
        ///     自创建父容器控件
        /// </summary>
        /// <returns></returns>
        public Control GetPanel()
        {
            return null;
        }
    }
}