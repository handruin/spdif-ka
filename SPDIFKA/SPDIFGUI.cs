using System;
using System.Threading;
using System.Windows.Forms;

namespace SPDIFKA
{
    public partial class SPDIFKAGUI : Form
    {
        private static String name = "SPDIF-KA";
        private static String stoppedMessage = "stopped";
        private static String startMessage = "running";
        private static String toolStripStartText = "Start " + name;
        private static String toolStripStopText = "Stop " + name;

        private static String version = System.Reflection.Assembly.GetExecutingAssembly()
                                           .GetName()
                                           .Version
                                           .ToString();
        private static UserPreferences UserPerfs = new UserPreferences();

        /// <summary>
        /// General initialization.
        /// </summary>
        public SPDIFKAGUI()
        {
            InitializeComponent();

            this.MaximizeBox = false;

            this.spdifka.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.spdifka.BalloonTipText = name + " - " + stoppedMessage;
            this.spdifka.BalloonTipTitle = name;
            this.spdifka.Text = name + " - " + stoppedMessage;
            
            toolStripStart.Text = toolStripStartText;
            this.spdifka.ContextMenuStrip = RightClickMenuStrip;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            runningLabel.Text = stoppedMessage;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            this.loadState();
        }

        /// <summary>
        /// Load the settings and state of the application that were previously saved.
        /// </summary>
        private void loadState()
        {
            //Update the visual check boxes with saved state.
            IsMinimizedCheckBox.Checked = UserPerfs.IsHidden;
            IsRunningCheckBox.Checked = UserPerfs.IsRunning;
            
            if (UserPerfs.IsHidden)
            {
                this.minimize();
            }
            
            if (UserPerfs.IsRunning)
            {
                manageStartStop();
            }
        }

        /// <summary>
        /// General destructor
        /// </summary>
        ~SPDIFKAGUI()
        {
            this.Resize -= new System.EventHandler(this.Form1_Resize);
        }

        /// <summary>
        /// Start and Stop button for changing the audio state of this tool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, EventArgs e)
        {
            manageStartStop();
        }

        /// <summary>
        /// Resize event to manage the windows tool bar functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.minimize();
            }
        }

        /// <summary>
        /// Minimize the application into the task bar.
        /// </summary>
        private void minimize()
        {
            this.WindowState = FormWindowState.Minimized;
            spdifka.Visible = true;
            this.ShowInTaskbar = false;
            this.Hide();
        }

        /// <summary>
        /// Restore the window to normal user operation mode.
        /// </summary>
        private void restore()
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();           
        }

        /// <summary>
        /// Single mouse click event to manage the restoring of the tool window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                this.restore();
            }
        }

        /// <summary>
        /// Tool tip icon menu start and stop management.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripStart_Click(object sender, EventArgs e)
        {
            manageStartStop();
        }

        /// <summary>
        /// Tool tip icon menu program exit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripExit_Click(object sender, EventArgs e)
        {
            AudioControl.Instance.stop();  //Ensure audio stops before exiting.
            Application.Exit();
        }

        /// <summary>
        /// Tool tip icon menu About popup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copyright 2015 handruin.com - Version " + version, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Generic method to handle the starting and stopping of the SPDIF keep alive audio clip playback.
        /// </summary>
        private void manageStartStop()
        {
            if (!AudioControl.Instance.isRunning())
            {
                this.spdifka.Text = name + " - " + startMessage;
                toolStripStart.Text = toolStripStopText;
                runningLabel.Text = startMessage;
                this.spdifka.BalloonTipText = name + " - " + startMessage;
                startStopButton.Text = "stop";
                AudioControl.Instance.start();
                this.updateTrayIconWhenRunning(true);
            }
            else
            {
                this.spdifka.Text = name + " - " + stoppedMessage;
                startStopButton.Text = "start";
                toolStripStart.Text = toolStripStartText;
                runningLabel.Text = stoppedMessage;
                this.spdifka.BalloonTipText = name + " - " + stoppedMessage;
                AudioControl.Instance.stop();
                this.updateTrayIconWhenRunning(false);
            }
        }

        /// <summary>
        /// Update the visual icon in the tray to represent the application state.
        /// </summary>
        /// <param name="isRunning"></param>
        private void updateTrayIconWhenRunning(Boolean isRunning)
        {            
            if (isRunning)
            {
                spdifka.Icon = Properties.Resources.bar_chart_64_green;
            }
            else
            {
                spdifka.Icon = Properties.Resources.bar_chart_64_white;
            }
        }

        /// <summary>
        /// Handle event when IsMinimized CheckBox is selected/deselected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsMinimizedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UserPerfs.IsHidden = IsMinimizedCheckBox.Checked;
            UserPerfs.Save();
        }

        /// <summary>
        /// Handle event when IsRunning CheckBox is selected/deselected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsRunningCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UserPerfs.IsRunning = IsRunningCheckBox.Checked;
            UserPerfs.Save();
        }
    }
}