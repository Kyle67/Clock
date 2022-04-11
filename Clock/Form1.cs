using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Windows.UI.ViewManagement.ApplicationViewTitleBar titleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            //titleBar.InactiveBackgroundColor = titleBar.BackgroundColor = Windows.UI.Colors.Black;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var timeNow = DateTime.Now;
            label1.Text = timeNow.ToString("hh:mm tt");
            var badNight = new DateTime(2000, 1, 1, 22, 0, 0);
            var badMorn = new DateTime(2000, 1, 1, 5, 0, 0);
            if (!((badMorn.TimeOfDay <= timeNow.TimeOfDay) && (timeNow.TimeOfDay < badNight.TimeOfDay)))
            {
                label1.ForeColor = Color.Red;
            }

            // IF THE MORNING TIME WAS TO BE BEFORE MIDNIGHT (I.E. THE SAME DAY, HENCE GREATER THAN BADNIGHT, THEN WOULD NEED A DIFFERENT IF STATEMENT
            //// convert everything to TimeSpan
            /*TimeSpan start = new TimeSpan(22, 0, 0);
            TimeSpan end = new TimeSpan(07, 0, 0);
            TimeSpan now = DateTime.Now.TimeOfDay;
            // see if start comes before end
            if (start < end)
                return start <= now && now <= end;
            // start is after end, so do the inverse comparison
            return !(end < now && now < start);*/
        }

        [DllImport("DwmApi")] //System.Runtime.InteropServices
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }
    }
}
