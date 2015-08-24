using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPDIFKA
{
    class AudioControl : IDisposable
    {
        private Boolean isSoundStarted = false;
        private SoundPlayer sound;
        private static AudioControl instance;

        private AudioControl() { }

        /// <summary>
        /// Singleton management of the audio controls.
        /// </summary>
        public static AudioControl Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new AudioControl();
                }
                return instance;
            }
        }

        ~AudioControl()
        {
            //Dispose(false);
        }

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //private void Dispose(bool p)
        //{
        //    this.sound.Dispose();
        //}

        /// <summary>
        /// Start the audio playback which will keep the SPDIF link alive.
        /// </summary>
        public void start()
        {
            sound = new SoundPlayer(Properties.Resources.silence);
            sound.LoadCompleted += new AsyncCompletedEventHandler(wavPlayer_LoadCompleted);
            sound.LoadAsync();
        }
        
        /// <summary>
        /// Stop the audio playback which will stop the SPDIF link.
        /// </summary>
        public void stop()
        {
            //Make sure we don't try to call Stop on a null object.
            if(this.sound != null)
            {
                this.sound.Stop();
                sound.Dispose();
                sound = null;
            }
            isSoundStarted = false;
        }

        /// <summary>
        /// Check to see the current state of the audio playback is either started or stopped.
        /// </summary>
        /// <returns></returns>
        public Boolean isRunning()
        {
            return isSoundStarted;
        }


        /// <summary>
        /// Async WAV file event loading and begin playback.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wavPlayer_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            isSoundStarted = true;
            ((System.Media.SoundPlayer)sender).PlayLooping();
        }
    }
}
