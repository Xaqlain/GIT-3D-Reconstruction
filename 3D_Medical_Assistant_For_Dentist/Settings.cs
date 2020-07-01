using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3D_Medical_Assistant_For_Dentist
{
	public partial class Settings : Form
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

		private ComboBox comboBox1;

		private Label label2;

		private RadioButton radioButton1;

		private RadioButton radioButton2;

		private TextBox textBox1;

		private Label label3;

		private Label label4;

		private CheckBox checkBox1;

		private Label label5;

		private ComboBox comboBox2;

		private ComboBox comboBox3;

		private Label label6;

		private Label label7;

		private Label label8;

		private TextBox textBox3;

		private Label label9;

		private TextBox textBox4;

		private Label label10;

		private Label label11;

		private Label label12;

		private TextBox textBox5;

		private Label label13;

		private TextBox textBox6;

		private Label label14;

		private TextBox textBox7;

		private Label label15;

		private Button button1;

		private TextBox textBox8;

		private Panel panel2;

		private RadioButton radioButton4;

		private RadioButton radioButton3;

		private TextBox textBox9;

		private TextBox textBox2;

		private Panel panel1;

		private CheckBox checkBox2;

		private TextBox textBox10;

		private RadioButton radioButton5;

		private Label label17;

		private RadioButton radioButton6;

		private ComboBox comboBox4;

		private Label label16;

		private string filePath = Directory.GetCurrentDirectory() + "/Config.txt";

		private string[] lines;

		private List<string> liness;

		public Settings()
		{
			
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.None;
			Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
			comboBox2.SelectedIndex = 2;
			comboBox3.SelectedIndex = 1;
			comboBox4.SelectedIndex = 0;
			base.AutoScaleMode = AutoScaleMode.Font;
			ToolTip toolTip = new ToolTip();
			toolTip.AutoPopDelay = 500000000;
			toolTip.InitialDelay = 100;
			toolTip.ReshowDelay = 50;
			toolTip.ShowAlways = true;
			toolTip.SetToolTip(label2, "VisualSfm parameter.\nPairwise matching is used to find coresponding features in a set of photos\nand therefor determine photos capturing the same side of an object.");
			toolTip.SetToolTip(radioButton1, "VisualSfm parameter.\nUse this setting for continually captured photos.\nDetermines how many photos ahead should be examined to contain the same features.\nIt is advisable to capture photos and process in this way, because it reduces error\nbecause of not looking for features in too distant photos, where they are not.\nNumber determines number of sequential look ahead photos for every photo being processed.\nSet this according to the density of your capture.\nOverally set this to a smaller number than you think to readuce an error.\nTipycally 5 - 8 work well.");
			toolTip.SetToolTip(radioButton2, "VisualSfm parameter.\nUse this setting for messed up photos.\nThis enables looking for corresponding features of a photo in all the others photo.");
			toolTip.SetToolTip(label5, "VisualSfm parameter.\nChoose the component to compute SIFT features.\nGLSL means GPU (OpenGL Shading Language), CUDA means CUDA of Nvidia cards.\nOverally, CPU is very very slow, GLSL and CUDA are very close to each other.\nGLSL worked fastest.");
			toolTip.SetToolTip(label6, "VisualSfm parameter.\nChoose the component to compute SIFT pairing (photos pairing).\nGLSL means GPU (OpenGL Shading Language), CUDA means CUDA of Nvidia cards.\nOverally, CPU is slow, GLSL and CUDA are very close to each other.\nCUDA worked fastest.");
			toolTip.SetToolTip(label8, "CMPMVS parameter.\nDetermines how much to downsize images before being processed by CMPMVS.\nWe suggest this parameter, and parameter Grow as the main quality and time changing parameters.");
			toolTip.SetToolTip(radioButton3, "CMPMVS parameter.\nDetermines how much to downsize images before being processed by CMPMVS.\nValues determine preferred resolution of longest side of image and appliation will internally determine best scale\nto meet this resolution as close as possible.\nBest quality/time ratio is met in 400 pixels wide longest side of image.");
			toolTip.SetToolTip(radioButton4, "CMPMVS parameter.\nDetermines how much to downsize images before being processed by CMPMVS.\nScale determines number of times of downscale.\nWidth and height of image will be downscaled (devided) by this factor.");
			toolTip.SetToolTip(label9, "CMPMVS parameter.\nMinimal apical angle for computing polygons.\nNobody really know what this means.\nTipycal values are from 1.0 to 4.0.\nSet to a smaller (2.0 or 1.0) value when you have the cameras next to each other\nand you want to reconstruct objects which are further from the cameras.");
			toolTip.SetToolTip(label11, "CMPMVS parameter.\n[grow]minNumOfConsistentCams is number of neighbouring target cameras used for planesweeping\nTypical vaules are 2 - 5.\nBest quality/time ratio met at 3 - 4.\nWe suggest this parameter, and parameter Scale as the main quality and time changing parameters.");
			toolTip.SetToolTip(label12, "CMPMVS parameter.\n[filter]minNumOfConsistentCams controls the minimal number of cameras associated with each tetrahedralization vertex.\nFor densely captured scenes it is very usefull to set this parameter to 3 in order to obtain better result faster.\nDetermines how many cameras have to see a point to become a polygon.\nMay be used to remove background (higher values) when capturing object in the middle.\nOverally raising value will delete badly covered points of the object of interest.\nIt it advised to leave this at 3 and remove unwanted polygons in MeshLab.");
			toolTip.SetToolTip(label14, "CMPMVS parameter.\nTo speedup the computation you can change the wsh to 2 (or 1).\nThis can cause lower quality of final reconstruction in some situations.");
			toolTip.SetToolTip(label13, "CMPMVS parameter.\nTo speedup the computation you can change the wsh to 2 (or 1).\nThis can cause lower quality of final reconstruction in some situations.\nLeave this at 4, lowering gives almost no time gain.");
			toolTip.SetToolTip(checkBox1, "Checking will allow you to see cmd with undergoing computations when reconstructing.\nNot much to see only tons of text in cmd.\nThis is the magic.");
			toolTip.SetToolTip(label17, "Defines where to store temp files.\nThese files are tipically very large!\n40GB for 800 photos overally.");
			toolTip.SetToolTip(checkBox2, "Determines whether to delete or leave created temp files.");
			panel2.Enabled = false;
			load();
			loadConfig();
		}
		private void load()
		{
			lines = File.ReadAllLines(filePath);
			liness = new List<string>();
			using (StreamReader streamReader = new StreamReader(filePath))
			{
				for (int i = 1; i <= lines.Length; i++)
				{
					liness.Add(streamReader.ReadLine());
				}
			}
			comboBox1.SelectedIndex = int.Parse(liness.ElementAt(0));
		}

		private void loadConfig()
		{
			if (comboBox1.SelectedIndex == 3)
			{
				panel2.Enabled = true;
			}
			else
			{
				panel2.Enabled = false;
			}
			if (int.Parse(liness.ElementAt(1)) == 0)
			{
				radioButton1.Checked = true;
				textBox1.Enabled = true;
			}
			else
			{
				radioButton2.Checked = true;
				textBox1.Enabled = false;
			}
			textBox1.Text = liness.ElementAt(2);
			if (int.Parse(liness.ElementAt(4)) == 0)
			{
				checkBox1.Checked = false;
			}
			else
			{
				checkBox1.Checked = true;
			}
			int num = 0;
			if (comboBox1.SelectedIndex == 0)
			{
				num = 6;
				textBox2.Text = liness.ElementAt(num + 9);
				textBox9.Text = liness.ElementAt(num + 10);
			}
			else if (comboBox1.SelectedIndex == 1)
			{
				num = 18;
				textBox2.Text = liness.ElementAt(num + 9);
				textBox9.Text = liness.ElementAt(num + 10);
			}
			else if (comboBox1.SelectedIndex == 2)
			{
				num = 30;
				textBox2.Text = liness.ElementAt(num + 9);
				textBox9.Text = liness.ElementAt(num + 10);
			}
			else
			{
				num = 42;
				textBox2.Text = liness.ElementAt(num + 9);
				textBox9.Text = liness.ElementAt(num + 10);
			}
			if (int.Parse(liness.ElementAt(num + 2)) == 0)
			{
				radioButton3.Checked = true;
				textBox2.Enabled = true;
				textBox9.Enabled = true;
				textBox8.Enabled = false;
			}
			else
			{
				radioButton4.Checked = true;
				textBox2.Enabled = false;
				textBox9.Enabled = false;
				textBox8.Enabled = true;
			}
			comboBox2.SelectedIndex = int.Parse(liness.ElementAt(num));
			comboBox3.SelectedIndex = int.Parse(liness.ElementAt(num + 1));
			textBox8.Text = liness.ElementAt(num + 3);
			textBox3.Text = liness.ElementAt(num + 4);
			textBox4.Text = liness.ElementAt(num + 5);
			textBox5.Text = liness.ElementAt(num + 6);
			textBox7.Text = liness.ElementAt(num + 7);
			textBox6.Text = liness.ElementAt(num + 8);
			if (int.Parse(liness.ElementAt(54)) == 0)
			{
				radioButton6.Checked = true;
				textBox10.Enabled = false;
			}
			else
			{
				radioButton5.Checked = true;
				textBox10.Enabled = true;
			}
			textBox10.Text = liness.ElementAt(55);
			if (int.Parse(liness.ElementAt(56)) == 0)
			{
				checkBox2.Checked = false;
			}
			else
			{
				checkBox2.Checked = true;
			}
		}

		private void presetChanged(object sender, EventArgs e)
		{
			if (comboBox1.SelectedIndex == 3)
			{
				panel2.Enabled = true;
			}
			else
			{
				panel2.Enabled = false;
			}
			loadConfig();
		}

		private void sequencePairwise(object sender, MouseEventArgs e)
		{
			textBox1.Enabled = true;
		}

		private void completePairwise(object sender, EventArgs e)
		{
			textBox1.Enabled = false;
		}

		private void autoScale(object sender, EventArgs e)
		{
			textBox2.Enabled = true;
			textBox9.Enabled = true;
			textBox8.Enabled = false;
		}

		private void setScale(object sender, EventArgs e)
		{
			textBox2.Enabled = false;
			textBox9.Enabled = false;
			textBox8.Enabled = true;
		}

		private bool fieldsCheck()
		{
			if (!int.TryParse(textBox7.Text, out int result) || !int.TryParse(textBox6.Text, out result) || !int.TryParse(textBox1.Text, out result) || !int.TryParse(textBox2.Text, out result) || !int.TryParse(textBox9.Text, out result) || !int.TryParse(textBox8.Text, out result) || !int.TryParse(textBox4.Text, out result) || !int.TryParse(textBox5.Text, out result))
			{
				MessageBox.Show("Invalid number entered.");
				return false;
			}
			return true;
		}

		private void saved(object sender, EventArgs e)
		{
			if (fieldsCheck())
			{
				using (StreamWriter streamWriter = new StreamWriter(filePath))
				{
					streamWriter.WriteLine(comboBox1.SelectedIndex);
					if (radioButton1.Checked)
					{
						streamWriter.WriteLine("0");
					}
					else
					{
						streamWriter.WriteLine("1");
					}
					streamWriter.WriteLine(textBox1.Text);
					streamWriter.WriteLine("Unhide Subprocesses");
					if (checkBox1.Checked)
					{
						streamWriter.WriteLine("1");
					}
					else
					{
						streamWriter.WriteLine("0");
					}
					if (comboBox1.SelectedIndex != 3)
					{
						for (int i = 6; i <= lines.Length; i++)
						{
							if (i == 55)
							{
								if (radioButton6.Checked)
								{
									streamWriter.WriteLine("0");
								}
								else
								{
									streamWriter.WriteLine("1");
								}
								streamWriter.WriteLine(textBox10.Text);
								if (checkBox2.Checked)
								{
									streamWriter.WriteLine("1");
								}
								else
								{
									streamWriter.WriteLine("0");
								}
								i += 2;
							}
							else
							{
								streamWriter.WriteLine(liness.ElementAt(i - 1));
							}
						}
					}
					else
					{
						for (int j = 6; j <= lines.Length; j++)
						{
							switch (j)
							{
								case 43:
									streamWriter.WriteLine(comboBox2.SelectedIndex);
									streamWriter.WriteLine(comboBox3.SelectedIndex);
									if (radioButton3.Checked)
									{
										streamWriter.WriteLine("0");
									}
									else
									{
										streamWriter.WriteLine("1");
									}
									streamWriter.WriteLine(textBox8.Text);
									streamWriter.WriteLine(textBox3.Text);
									streamWriter.WriteLine(textBox4.Text);
									streamWriter.WriteLine(textBox5.Text);
									streamWriter.WriteLine(textBox7.Text);
									streamWriter.WriteLine(textBox6.Text);
									streamWriter.WriteLine(textBox2.Text);
									streamWriter.WriteLine(textBox9.Text);
									j += 10;
									break;
								case 55:
									if (radioButton6.Checked)
									{
										streamWriter.WriteLine("0");
									}
									else
									{
										streamWriter.WriteLine("1");
									}
									streamWriter.WriteLine(textBox10.Text);
									if (checkBox2.Checked)
									{
										streamWriter.WriteLine("1");
									}
									else
									{
										streamWriter.WriteLine("0");
									}
									j += 2;
									break;
								default:
									streamWriter.WriteLine(liness.ElementAt(j - 1));
									break;
							}
						}
					}
				}
				Dispose();
			}
		}

		private void tempFolderSame(object sender, EventArgs e)
		{
			textBox10.Enabled = false;
		}

		private void tempSetFolder(object sender, EventArgs e)
		{
			textBox10.Enabled = true;
		}

		private void textBox9Copy(object sender, EventArgs e)
		{
			textBox9.Text = textBox2.Text;
		}

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
			this.WindowState = FormWindowState.Minimized;
		}

        private void button3_Click(object sender, EventArgs e)
        {
			Settings Settings = new Settings();
			this.Dispose();
		}

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
}


