using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using KCPlayer.Plugin.WatchTV.Controls;
using KCPlayer.Plugin.WatchTV.LoadWatchTV;

namespace KCPlayer.Plugin.WatchTV
{
    public class MainInterFace
    {
        #region FixedField

        public static Control Owner { get; set; }
        public Control OwnerParent { get; set; }
        private Image _customDrawing = null;

        public Image CustomDrawing
        {
            get { return _customDrawing; }
            set { _customDrawing = value; }
        }

        private Guid _guid = new Guid();

        public Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        public static OuterMethod OuterInvoke { get; set; }

        #endregion

        #region FixedMethod

        /// <summary>
        /// 被添加到父容器时触发
        /// </summary>
        /// <param name="control"></param>
        public MainInterFace(Control control)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            object[] attrs = assembly.GetCustomAttributes(typeof (System.Runtime.InteropServices.GuidAttribute), false);
            Guid = new Guid(((GuidAttribute) attrs[0]).Value);
            OwnerParent = control;
            OuterInvoke = new OuterMethod(this);
        }

        /// <summary>
        /// 每次激活时触发
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

        #region 激活触发

        ///
        ///  <summary>
        /// 当磁铁添加进入后触发事件
        /// </summary>
        public void Shown()
        {
        }

        /// <summary>
        /// 每次激活时触发
        /// </summary>
        public void Init()
        {
            PublicStatic.SegoeFont = new FontsHelper().GetFont("Segoe UI Light.TTF");
            if (PublicStatic.SegoeFont == null)
            {
                MessageBox.Show(@"没有正确加载应用字体");
                return;
            }
            Owner.Parent.Size = new Size(865, 545);
            Owner.Parent.BackgroundImage = Properties.Resources.Groove_1920x1080;
            Owner.Parent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            PublicStatic.LiuXingPal = new ELabel
                {
                    Dock = DockStyle.Fill,
                };
            Owner.Controls.Add(PublicStatic.LiuXingPal);
            try
            {
                WatchTvStart.LoadLiuXing();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// 自创建父容器控件
        /// </summary>
        /// <returns></returns>
        public Control GetPanel()
        {
            return null;
        }

        #endregion
    }
}