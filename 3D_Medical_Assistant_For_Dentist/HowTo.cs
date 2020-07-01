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

namespace _3D_Medical_Assistant_For_Dentist
{
	public partial class HowTo : Form
	{
		[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

		private static extern IntPtr CreateRoundRectRgn
		(
			int nLeftRect,     
			int nTopRect,      
			int nRightRect,    
			int nBottomRect,   
			int nWidthEllipse, 
			int nHeightEllipse 
		);

		private TabPage tabPage4;

		private Label label19;

		private Label label18;

		private Label label17;

		private Label label16;

		private Label label15;

		private TabPage tabPage2;

		private Label label14;

		private Label label9;

		private Label label8;

		private TabPage tabPage1;

		private Label label13;

		private Label label12;

		private Label label11;

		private Label label10;

		private Label label7;

		private Label label6;

		private Label label5;

		private Label label4;

		private Label label3;

		private Label label2;

		private Label label1;

		private TabControl HowToTab;

		private Label label20;

		private Label label21;

		public HowTo()
		{
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.None;
			Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
		}

        private void HowTo_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
			HowTo HowTo1 = new HowTo();
			this.Dispose();
			
        }

        private void button2_Click(object sender, EventArgs e)
        {

			this.WindowState = FormWindowState.Minimized;

		}
    }
}
