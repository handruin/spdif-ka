using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPDIFKA
{
    class UserPreferences
    {
        public enum Sound { Silent, Inaudible}
        public Boolean IsRunning { get; set; }
        public Boolean IsHidden { get; set; }
        public Boolean IsMinimizeToNotificationArea { get; set; }
        public Boolean IsMinimizedOnClose { get; set; }
        public Sound SoundType { get; set; }

        public UserPreferences()
        {
            Load();
        }

        public void Save()
        {
            Properties.Settings.Default.IsHidden = this.IsHidden;
            Properties.Settings.Default.IsRunning = this.IsRunning;
            Properties.Settings.Default.IsMinimizedNotification = this.IsMinimizeToNotificationArea;
            Properties.Settings.Default.IsMinimizedOnClose = this.IsMinimizedOnClose;
            Properties.Settings.Default.SoundType = this.SoundType.ToString();
            Properties.Settings.Default.Save();
        }

        public void Load() {

            this.IsHidden = Properties.Settings.Default.IsHidden;
            this.IsRunning = Properties.Settings.Default.IsRunning;
            this.IsMinimizeToNotificationArea = Properties.Settings.Default.IsMinimizedNotification;
            this.IsMinimizedOnClose = Properties.Settings.Default.IsMinimizedOnClose;
            if (Properties.Settings.Default.SoundType == "Silent")
            {
                this.SoundType = Sound.Silent;
            }

            if (Properties.Settings.Default.SoundType == "Inaudible")
            {
                this.SoundType = Sound.Inaudible;
            }
        }

    }
}
