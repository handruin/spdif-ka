using System;
using System.Threading;
using System.Windows.Forms;

namespace SPDIFKA
{
    static class SPDIFKA
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, @"Global\" + "spdif-ka_mutex"))
            {
                try {
                    if (mutex.WaitOne(0, false)) // This is the only instance
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new SPDIFKAGUI());
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    mutex.Close();
                }
            }
        }
    }
}
