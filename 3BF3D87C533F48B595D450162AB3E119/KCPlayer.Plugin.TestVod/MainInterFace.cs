using System;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using KCPlayer.Plugin.TestVod.Controls;
using KCPlayer.Plugin.TestVod.Properties;

namespace KCPlayer.Plugin.TestVod
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
        ///     外部调用
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        public void CallMeAction(string name, string url)
        {
            PublicStatic.VodInput.Text = url;
            TestVodAction.AnSearchStart();
        }


        /// <summary>
        ///     每次激活时触发
        /// </summary>
        public void Init()
        {
            if (PublicStatic.SegoeFont == null)
            {
                PublicStatic.SegoeFont = "微软雅黑";
            }

            Owner.Size = Owner.Parent.Size = new Size(1020, 563 + 30);//+18
            Owner.BackgroundImageLayout = Owner.Parent.BackgroundImageLayout = ImageLayout.Stretch;
            PublicStatic.VodPal = new EPanel
                {
                    Dock = DockStyle.Fill,
                };
            Owner.Controls.Add(PublicStatic.VodPal);
           
            try
            {
                TestVodStart.LoadVodStartPal();
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