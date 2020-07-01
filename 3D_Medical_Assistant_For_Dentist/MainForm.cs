using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace _3D_Medical_Assistant_For_Dentist
{
	public partial class MainForm : Form
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
		
		private Info info = new Info();

		private Settings settingsWindow = new Settings();

		private HowTo howto = new HowTo();

		private Label label1;

		private Label label2;

		private Button button2;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem infoToolStripMenuItem;

		private ToolStripMenuItem settingsToolStripMenuItem;

		private ToolStripMenuItem howToToolStripMenuItem;

		private Panel Pn_DragInput;

		private Panel panel2;

		private Label label4;

		private Label label5;

		private Label label6;

		private Label label7;

		private ToolStripMenuItem open3DModelToolStripMenuItem;

		private Button button1;

		private ProgressBar progressBar1;

		private Label label8;

		private Label label9;

		private Label label10;

		private Label label11;

		private ListBox listBox1;

		private List<string> inputFolderPaths = new List<string>();

		private int counter;

		private string outputFolderPath = "";

		private int iniCounter;

		private List<string> liness;

		private bool showSubprocesses;

		private TextWriter loggingFile;

		private string tempFolderPath;

		private bool continueProcessing = true;

		private bool skipThisFolder;

		private ProcessStartInfo startInfo = new ProcessStartInfo();

		private Stopwatch watch = new Stopwatch();

		private int id;

		private ProcessStartInfo startInfo2 = new ProcessStartInfo();

		private ProcessStartInfo startInfo3 = new ProcessStartInfo();

		private List<string> CMPMVSinputFolders = new List<string>();

		private string[] lines;

		private string filePath = Directory.GetCurrentDirectory() + "/Config.txt";

		private int ofset;


		public MainForm()
		{
			
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.None;
			Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

			base.AutoScaleMode = AutoScaleMode.Font;
			label6.BackColor = Color.Transparent;
			ToolTip toolTip = new ToolTip();
			toolTip.AutoPopDelay = 500000000;
			toolTip.InitialDelay = 100;
			toolTip.ReshowDelay = 50;
			toolTip.ShowAlways = true;
			toolTip.SetToolTip(Pn_DragInput, "Drag and drop folder or folders containing captured photos.\nSub folders are ignored.\nFolder must contain jpg photos only.");
			toolTip.SetToolTip(panel2, "Drag and drop output folder.\nGenerated 3D models in .ply format will be placed here.\nThis folder will be used as a Temp folder as well if enabled in settings.");

		}

		private void helpWanted(object sender, EventArgs e)
		{
			if (!howto.IsHandleCreated)
			{
				howto = new HowTo();
			}
			howto.Show();
		}

		private void infoWindow(object sender, EventArgs e)
		{
			if (!info.IsHandleCreated)
			{
				info = new Info();
			}
			info.ShowDialog();
		}

		private void openSettings(object sender, EventArgs e)
		{
			if (!settingsWindow.IsHandleCreated)
			{
				settingsWindow = new Settings();
			}
			settingsWindow.ShowDialog();
		}
		private void Open3DmodelPly(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				if (Directory.Exists(outputFolderPath))
				{
					openFileDialog.InitialDirectory = outputFolderPath;
				}
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					ProcessStartInfo processStartInfo = new ProcessStartInfo();
					processStartInfo.WorkingDirectory = Directory.GetCurrentDirectory() + '/' + "MeshLab/";
					processStartInfo.FileName = Directory.GetCurrentDirectory() + '/' + "MeshLab/meshlab.exe";
					processStartInfo.Arguments = "\"" + openFileDialog.FileName + "\"";
					try
					{
						using (Process process = Process.Start(processStartInfo))
						{
							process.PriorityClass = ProcessPriorityClass.BelowNormal;
							process.WaitForExit();
						}
					}
					catch
					{
						MessageBox.Show("Cant Run Meshlab", "Open error");
					}
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception occurs in Open3DmodelPly() " +
					"\n " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Dragenter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.All;
		}

		private void Dragdrop(object sender, DragEventArgs e)
		{
			try
			{
				bool flag = true;
				int num = 0;
				bool flag2 = false;
				string[] array = (string[])e.Data.GetData(DataFormats.FileDrop, false);
				foreach (string text in array)
				{
					flag2 = false;
					if (File.Exists(text))
					{
						MessageBox.Show("Drop fodlers only\nDrop cancelled.", "Input error");
						flag = false;
						break;
					}
					if (!Directory.Exists(text))
					{
						MessageBox.Show("Drop folders only.\nDrop cancelled.", "Input error");
						flag = false;
						break;
					}
					if (Directory.GetFiles(text, "*.jp*g").Length == 0)
					{
						MessageBox.Show("Some of the dropped folders do not contain jpg images.\nDrop cancelled.", "Input error");
						flag = false;
						break;
					}
					if (flag)
					{
						for (int j = 0; j < inputFolderPaths.Count; j++)
						{
							if (inputFolderPaths.ElementAt(j) == text)
							{
								flag2 = true;
								break;
							}
						}
						if (!flag2)
						{
							inputFolderPaths.Add(text);
							num++;
						}
					}
				}
				if (flag)
				{
					label7.Text = "✓";
					if (num == 1)
					{
						counter++;
						label3.Text = counter.ToString();
						listBox1.Items.Clear();
						listBox1.Items.AddRange(inputFolderPaths.ToArray());
						label11.Text = "One input folder added.";
					}
					else
					{
						listBox1.Items.AddRange(inputFolderPaths.ToArray());
						counter += num;
						label3.Text = counter.ToString();
						label11.Text = num + " input folders added.";
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception occurs in Dragdrop() " +
			"\n " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void dragEnter2(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.All;
		}

		private void dragDrop2(object sender, DragEventArgs e)
		{
			try
			{
				bool flag = true;
				int num = 0;
				string[] array = (string[])e.Data.GetData(DataFormats.FileDrop, false);
				foreach (string path in array)
				{
					num++;
					if (Directory.Exists(path) && flag)
					{
						flag = false;
						outputFolderPath = path;
					}
					else if (num > 1)
					{
						MessageBox.Show("Drag only one folder.", "Input error");
					}
					else
					{
						MessageBox.Show("Drop folfer only\nDrop cancelled.", "Input error");
					}
				}
				if (!flag && num == 1)
				{
					listBox1.Items.Add(outputFolderPath);
					if (label5.Text == "✓")
					{
						label11.Text = "Output folder re-entered.";
					}
					else
					{
						label11.Text = "Output folder defined.";
					}
					label5.Text = "✓";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception occurs in Dragdrop2() " +
	"\n " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void process(object sender, EventArgs e)
		{
			try
			{
				if (label5.Text == "✓" && label7.Text == "✓")
				{
					progressBar1.Value = 10;
					progressBar1.Step = 90 / counter / 3;
					label6.Visible = false;
					button2.Enabled = false;
					button2.Visible = false;
					button1.Enabled = true;
					button1.Visible = true;
					iniCounter = counter;
					label8.Visible = true;
					label9.Visible = true;
					label10.Visible = true;
					label11.Visible = true;
					label11.Text = "Processing...";
					label10.Text = iniCounter.ToString();
					settingsToolStripMenuItem.Enabled = false;
					loadConfig();
					if (int.Parse(liness.ElementAt(4)) == 0)
					{
						showSubprocesses = true;
					}
					else
					{
						showSubprocesses = false;
					}
					if (int.Parse(liness.ElementAt(54)) == 0)
					{
						Directory.CreateDirectory(outputFolderPath + "/Temp");
						tempFolderPath = outputFolderPath + "/Temp/";
					}
					else
					{
						tempFolderPath = liness.ElementAt(55) + '/';
					}
					loggingFile = new StreamWriter(outputFolderPath + "/log.txt", true);
				}
				else
				{
					MessageBox.Show("Drop input and output folders first.", "Process error");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception occurs in process() " +
	"\n " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void visualSfmProcess_Exited(object sender, EventArgs e)
		{
			try
			{
				if (continueProcessing)
				{
					watch.Stop();
					loggingFile.WriteLine(new DirectoryInfo(inputFolderPaths.ElementAt(iniCounter - counter)).Name);
					loggingFile.WriteLine("VisualSfm: " + watch.Elapsed.Hours.ToString() + "h " + watch.Elapsed.Minutes.ToString() + "m " + watch.Elapsed.Seconds.ToString() + 's');
					watch.Restart();
					computeCMPMVS();
					if (!skipThisFolder)
					{
						progressBar1.Invoke((MethodInvoker)delegate
						{
							progressBar1.PerformStep();
						});
						Process process = new Process();
						process.StartInfo = startInfo2;
						process.EnableRaisingEvents = true;
						process.Exited += CMPMVSProcess1_Exited;
						watch.Start();
						process.Start();
						process.PriorityClass = ProcessPriorityClass.BelowNormal;
						id = process.Id;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception occurs in visualSfmProcess_Exited() " +
		"\n " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CMPMVSProcess1_Exited(object sender, EventArgs e)
		{
			try
			{
				if (continueProcessing && !skipThisFolder)
				{
					watch.Stop();
					loggingFile.WriteLine("CMPMVS1: " + watch.Elapsed.Hours.ToString() + "h " + watch.Elapsed.Minutes.ToString() + "m " + watch.Elapsed.Seconds.ToString() + 's');
					watch.Restart();
					progressBar1.Invoke((MethodInvoker)delegate
					{
						progressBar1.PerformStep();
					});
					Process process = new Process();
					process.StartInfo = startInfo3;
					process.EnableRaisingEvents = true;
					watch.Start();
					process.Start();
					process.PriorityClass = ProcessPriorityClass.BelowNormal;
					id = process.Id;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception occurs in CMPMVSProcess1_Exited() " +
		"\n " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void loadConfig()
		{
			try
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
				switch (int.Parse(liness.ElementAt(0)))
				{
					case 0:
						ofset = 6;
						break;
					case 1:
						ofset = 18;
						break;
					case 2:
						ofset = 30;
						break;
					case 3:
						ofset = 42;
						break;
					default:
						ofset = 6;
						break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception occurs in loadConfig() " +
"\n " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void stopProcess(object sender, EventArgs e)
		{
			try
			{
				DialogResult dialogResult = MessageBox.Show("Do you really want to cancel process?", "Confirmation", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					continueProcessing = false;
					Process processById = Process.GetProcessById(id);
					processById.Kill();
					button1.Enabled = false;
					progressBar1.Value = 100;
					label11.Text = "Cancelled!";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception occurs in stopProcess() " +
"\n " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void computeSfm()
		{
			try
			{
				int num = 115;
				int num2 = 124;
				if (File.Exists(Directory.GetCurrentDirectory() + '/' + "VisualSFM/nv.ini"))
				{
					string path = Directory.GetCurrentDirectory() + '/' + "VisualSFM/nv.ini";
					string path2 = Directory.GetCurrentDirectory() + '/' + "VisualSFM/nv.ini";
					string text = null;
					string text2 = null;
					using (StreamReader streamReader = new StreamReader(path))
					{
						for (int i = 1; i <= num; i++)
						{
							text = streamReader.ReadLine();
						}
					}
					if (text == null)
					{
						throw new InvalidDataException("VisualSfm error - nv.ini missing.");
					}
					text = "param_use_siftgpu " + liness.ElementAt(ofset);
					text2 = "param_use_siftmatchgpu " + liness.ElementAt(ofset + 1);
					string[] array = File.ReadAllLines(path2);
					using (StreamWriter streamWriter = new StreamWriter(path2))
					{
						for (int j = 1; j <= array.Length; j++)
						{
							if (j == num)
							{
								streamWriter.WriteLine(text);
							}
							else if (j == num2)
							{
								streamWriter.WriteLine(text2);
							}
							else
							{
								streamWriter.WriteLine(array[j - 1]);
							}
						}
					}
					string text3 = "sfm+pairs+cmp";
					string text4 = text3;
					text3 = text4;
					text3 = text3 + " \"" + inputFolderPaths.ElementAt(iniCounter - counter) + "\"";
					string text5 = text3;
					text3 = text5 + " \"" + tempFolderPath + new DirectoryInfo(inputFolderPaths.ElementAt(iniCounter - counter)).Name + "\"";
					CMPMVSinputFolders.Add(tempFolderPath + new DirectoryInfo(inputFolderPaths.ElementAt(iniCounter - counter)).Name + ".nvm.cmp");
					if (int.Parse(liness.ElementAt(1)) == 0)
					{
						text3 = text3 + " @" + liness.ElementAt(2);
					}
					startInfo = new ProcessStartInfo();
					startInfo.CreateNoWindow = showSubprocesses;
					startInfo.UseShellExecute = showSubprocesses;
					startInfo.WorkingDirectory = Directory.GetCurrentDirectory() + '/' + "VisualSFM/";
					startInfo.FileName = Directory.GetCurrentDirectory() + '/' + "VisualSFM/VisualSFM.exe";
					startInfo.WindowStyle = ProcessWindowStyle.Hidden;
					startInfo.Arguments = text3;
				}
				else
				{
					MessageBox.Show("VisualSfm error - nv.ini missing.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception occurs in computeSfm() " +
"\n " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void computeCMPMVS()
		{
			try
			{
				Image image = Image.FromFile(Directory.GetFiles(inputFolderPaths.ElementAt(iniCounter - counter), "*.jp*g")[0]);
				int width = image.Width;
				int height = image.Height;
				int num = 1;
				List<int> list = new List<int>();
				List<string> list2 = new List<string>();
				List<string> list3 = new List<string>();
				if (int.Parse(liness.ElementAt(0)) == 3 && int.Parse(liness.ElementAt(44)) == 1)
				{
					num = int.Parse(liness.ElementAt(45));
				}
				else
				{
					int num2 = int.Parse(liness.ElementAt(ofset + 9));
					int num3 = int.Parse(liness.ElementAt(ofset + 10));
					int num4;
					int num5;
					if (width > height)
					{
						num4 = width;
						num5 = num2;
					}
					else
					{
						num4 = height;
						num5 = num3;
					}
					for (int i = 0; i < 50; i++)
					{
						if (num4 < i * num5)
						{
							num = i;
							break;
						}
					}
					if (num * num5 - num4 > Math.Abs((num - 1) * num5 - num4))
					{
						num--;
					}
				}
				list2.Add("01_mvs_firstRun.ini");
				list2.Add("02_mvs_limitedScale.ini");
				list.Add(2);
				list.Add(5);
				list.Add(6);
				list.Add(7);
				list.Add(8);
				list3.Add("dirName=" + '"' + CMPMVSinputFolders.ElementAt(iniCounter - counter) + "/00/data/" + '"');
				DirectoryInfo directoryInfo = new DirectoryInfo(CMPMVSinputFolders.ElementAt(iniCounter - counter) + "/00/data/");
				if (!Directory.Exists(CMPMVSinputFolders.ElementAt(iniCounter - counter) + "/00/data/"))
				{
					skipThisFolder = true;
				}
				if (!skipThisFolder)
				{
					list3.Add("ncams=" + (directoryInfo.GetFiles().Length / 2).ToString());
					list3.Add("width=" + width.ToString());
					list3.Add("height=" + height.ToString());
					list3.Add("scale=" + num);
					for (int j = 0; j < list2.Count; j++)
					{
						string path = Directory.GetCurrentDirectory() + '/' + "CMPMVS/ini/" + list2.ElementAt(j);
						if (File.Exists(path))
						{
							string[] array = File.ReadAllLines(path);
							using (StreamWriter streamWriter = new StreamWriter(path))
							{
								for (int k = 1; k <= array.Length; k++)
								{
									for (int l = 0; l < list.Count; l++)
									{
										if (k == list.ElementAt(l))
										{
											streamWriter.WriteLine(list3.ElementAt(l));
											break;
										}
										if (l == list.Count - 1)
										{
											streamWriter.WriteLine(array[k - 1]);
										}
									}
								}
							}
						}
						else
						{
							MessageBox.Show("CMPMVS error - ini files error.", "Error");
						}
					}
					string path2 = Directory.GetCurrentDirectory() + '/' + "CMPMVS/ini/" + list2.ElementAt(0);
					string[] array2 = File.ReadAllLines(path2);
					using (StreamWriter streamWriter2 = new StreamWriter(path2))
					{
						for (int m = 1; m <= array2.Length; m++)
						{
							switch (m)
							{
								case 17:
									streamWriter2.WriteLine("minAngle=" + liness.ElementAt(ofset + 4));
									break;
								case 20:
									streamWriter2.WriteLine("minNumOfConsistentCams=" + liness.ElementAt(ofset + 5));
									break;
								case 23:
									streamWriter2.WriteLine("minNumOfConsistentCams=" + liness.ElementAt(ofset + 6));
									break;
								case 26:
									streamWriter2.WriteLine("wsh=" + liness.ElementAt(ofset + 7));
									break;
								case 29:
									streamWriter2.WriteLine("wsh=" + liness.ElementAt(ofset + 8));
									break;
								default:
									streamWriter2.WriteLine(array2[m - 1]);
									break;
							}
						}
					}
					startInfo2 = new ProcessStartInfo();
					startInfo2.CreateNoWindow = showSubprocesses;
					startInfo2.UseShellExecute = showSubprocesses;
					startInfo2.WorkingDirectory = Directory.GetCurrentDirectory() + '/' + "CMPMVS/";
					startInfo2.FileName = Directory.GetCurrentDirectory() + '/' + "CMPMVS/CMPMVS.exe";
					startInfo2.WindowStyle = ProcessWindowStyle.Hidden;
					startInfo2.Arguments = "./ini/" + list2.ElementAt(0);
					startInfo3 = new ProcessStartInfo();
					startInfo3.CreateNoWindow = showSubprocesses;
					startInfo3.UseShellExecute = showSubprocesses;
					startInfo3.WorkingDirectory = Directory.GetCurrentDirectory() + '/' + "CMPMVS/";
					startInfo3.FileName = Directory.GetCurrentDirectory() + '/' + "CMPMVS/CMPMVS.exe";
					startInfo3.WindowStyle = ProcessWindowStyle.Hidden;
					startInfo3.Arguments = "./ini/" + list2.ElementAt(1);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception occurs in computeCMPMVS() " +
"\n " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private static void DirectoryCopy(string source, string dest, bool copySubDirs = true)
		{
			var dir = new DirectoryInfo(source);

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
					$"Source directory does not exist or could not be found: {source}");
			}

			DirectoryInfo[] dirs = dir.GetDirectories();

			if (!Directory.Exists(dest))
			{
				Directory.CreateDirectory(dest);
			}

			FileInfo[] files = dir.GetFiles();

			foreach (FileInfo file in files)
			{
				string tempPath = Path.Combine(dest, file.Name);
				file.CopyTo(tempPath, false);
			}

			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string tempPath = Path.Combine(dest, subdir.Name);
					DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
				}
			}
		}

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
			this.WindowState = FormWindowState.Minimized;
		}

        private void button4_Click(object sender, EventArgs e)
        {
			MainForm HowTo1 = new MainForm();
			this.Dispose();
		}

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}