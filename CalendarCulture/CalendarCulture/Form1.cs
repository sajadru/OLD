using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalendarCulture
{
    public partial class FrmCalendar : Form
    {
        private CalendarStruct _calendarStruct;
        public FrmCalendar()
        {
            InitializeComponent();   
        }

        private void FrmCalendar_Load(object sender, EventArgs e)
        {
            RbPersian.Checked = true;
            _calendarStruct = new CalendarStruct(this, "fa-IR");
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            _calendarStruct.AddMonth(1);
        }

        private void BtnPreview_Click(object sender, EventArgs e)
        {
            _calendarStruct.AddMonth(-1);
        }

        private void BtnNextYear_Click(object sender, EventArgs e)
        {
            _calendarStruct.AddYear(1);
        }

        private void BtnPreviewYear_Click(object sender, EventArgs e)
        {
            _calendarStruct.AddYear(-1);
        }

        private void Lbl_Click(object sender, EventArgs e)
        {
            foreach (var c in tableLayoutPanel1.Controls)
            {
                if (c.GetType()==typeof(Label))
                {
                    ((Label)c).BackColor = Color.FromArgb(224, 224, 224);

                }
                ((Label)sender).BackColor=Color.CadetBlue;
            }
        }

        private void RbPersian_CheckedChanged(object sender, EventArgs e)
        {
            _calendarStruct = new CalendarStruct(this, "fa-IR");
        }

        private void RbEnglish_CheckedChanged(object sender, EventArgs e)
        {
            _calendarStruct = new CalendarStruct(this, "en-US");
        }
    }
}
