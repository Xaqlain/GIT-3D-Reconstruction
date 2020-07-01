using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3D_Medical_Assistant_For_Dentist
{
	public partial class Info : Form
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

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private PictureBox pictureBox1;

		private PictureBox pictureBox2;

		private PictureBox pictureBox3;

		private Label label6;

		private Label label7;

		private Label label8;

		private Label label10;

		private Label label9;

		private Label label11;

		private Label label12;
		public Info()
		{
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.None;
			Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
		}

		private void Facebook(object sender, EventArgs e)
		{
			Process.Start("https://facebook.com/TAYYABDOST786");
		}

		private void Hovering1(object sender, MouseEventArgs e)
		{
			Cursor = Cursors.Hand;
		}

		private void notHovering1(object sender, EventArgs e)
		{
			Cursor = Cursors.Default;
		}

		private void GitHub(object sender, EventArgs e)
		{
			Process.Start("https://github.com/TAYYAB-BUKC");
		}

		private void hover2(object sender, MouseEventArgs e)
		{
			Cursor = Cursors.Hand;
		}

		private void notHovering2(object sender, EventArgs e)
		{
			Cursor = Cursors.Default;
		}

		private void WebPage(object sender, EventArgs e)
		{
			Process.Start("http://aldostcompany.com");
		}

		private void hovering3(object sender, MouseEventArgs e)
		{
			Cursor = Cursors.Hand;
		}

		private void notHovering3(object sender, EventArgs e)
		{
			Cursor = Cursors.Default;
		}

        private void Info_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
			this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
			Info HowTo1 = new Info();
			this.Dispose();
		}
    }
}
