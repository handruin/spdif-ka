using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPDIFKA
{
    public partial class SPDIFKAGUI : Form
    {
        private static String name = "SPDIF-KA";
        private static String stoppedMessage = "stopped";
        private static String startMessage = "running...";
        private static String toolStripStartText = "Start " + name;
        private static String toolStripStopText = "Stop " + name;
        private static String version = System.Reflection.Assembly.GetExecutingAssembly()
                                           .GetName()
                                           .Version
                                           .ToString();


        /// <summary>
        /// General initialization.
        /// </summary>
        public SPDIFKAGUI()
        {
            InitializeComponent();
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = name + " - " + stoppedMessage;
            this.notifyIcon.BalloonTipTitle = name;
            this.notifyIcon.Text = name;
            toolStripStart.Text = toolStripStartText;
            this.notifyIcon.ContextMenuStrip = contextMenuStrip1;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            runningLabel.Text = stoppedMessage;
            FormBorderStyle = FormBorderStyle.FixedSingle;

        }

        /// <summary>
        /// General destructor
        /// </summary>
        public ~SPDIFKAGUI()
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
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(2500);
                this.ShowInTaskbar = false;
            }
        }

        /// <summary>
        /// Double mouse click event to manage the restoring of the tool window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon.Visible = false;
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
            MessageBox.Show("Copyright 2014 handruin.com - Version " + version, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Generic method to handle the starting and stopping of the SPDIF keep alive audio clip playback.
        /// </summary>
        private void manageStartStop()
        {
            if (!AudioControl.Instance.isRunning())
            {
                toolStripStart.Text = toolStripStopText;
                runningLabel.Text = startMessage;
                this.notifyIcon.BalloonTipText = name + " - " + startMessage;
                startStopButton.Text = "stop";
                AudioControl.Instance.start();
            }
            else
            {
                startStopButton.Text = "start";
                toolStripStart.Text = toolStripStartText;
                runningLabel.Text = stoppedMessage;
                this.notifyIcon.BalloonTipText = name + " - " + stoppedMessage;
                AudioControl.Instance.stop();
            }
        }



    }
}
