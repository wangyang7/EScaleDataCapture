using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace EScaleDataCapture.Control
{
    public partial class CheckableGroupBox : GroupBox
    {
        private bool _checked;
        private const int CheckBoxSize = 13;

        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                foreach (System.Windows.Forms.Control control in this.Controls)
                {
                    control.Enabled = _checked;
                }
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // 绘制标准 GroupBox
            base.OnPaint(e);

            // 1. 绘制GroupBox边框（不绘制标题）
            //GroupBoxRenderer.DrawGroupBox(e.Graphics, ClientRectangle,
            //    Enabled ? GroupBoxState.Normal : GroupBoxState.Disabled);

            // 绘制 CheckBox
            Rectangle checkBoxRect = new Rectangle(8, 0, CheckBoxSize, CheckBoxSize);
            ControlPaint.DrawCheckBox(e.Graphics, checkBoxRect, //ButtonState.Inactive);
                _checked ? ButtonState.Checked : ButtonState.Normal);
            

            // 调整文本位置
            //Rectangle textRect = new Rectangle(
            //    checkBoxRect.Right + 15,
            //    checkBoxRect.Top,
            //    ClientSize.Width - checkBoxRect.Right - 10,
            //    checkBoxRect.Height);

            //TextRenderer.DrawText(e.Graphics, Text, Font, textRect, ForeColor);            
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Rectangle checkBoxRect = new Rectangle(8, 0, CheckBoxSize, CheckBoxSize);
            if (checkBoxRect.Contains(e.Location))
            {
                Checked = !Checked;
            }
            base.OnMouseDown(e);
        }

        public CheckableGroupBox()
        {
            InitializeComponent();
        }
    }
}
