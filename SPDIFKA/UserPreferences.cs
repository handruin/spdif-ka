using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPDIFKA {
    class UserPreferences {
        public const string DEFAULT_AUDIO_DEVICE = "Default playback device";

        public enum Sound { Silent, Inaudible }
        public bool IsRunning { get; set; }
        public bool IsHidden { get; set; }
        public bool IsStartWithWindows { get; set; }
        public Sound SoundType { get; set; }
        public StringCollection EnabledDeviceNames { get; set; }

        public UserPreferences() {
            this.Load();
        }

        public void Save() {
            Properties.Settings.Default.IsHidden = this.IsHidden;
            Properties.Settings.Default.IsRunning = this.IsRunning;
            Properties.Settings.Default.IsStartWithWindows = this.IsStartWithWindows;
            Properties.Settings.Default.SoundType = this.SoundType.ToString();
            Properties.Settings.Default.EnabledDeviceNames = this.EnabledDeviceNames;
            Properties.Settings.Default.Save();
        }

        public void Load() {
            this.IsHidden = Properties.Settings.Default.IsHidden;
            this.IsRunning = Properties.Settings.Default.IsRunning;
            this.IsStartWithWindows = Properties.Settings.Default.IsStartWithWindows;
            if (Properties.Settings.Default.SoundType == "Silent") {
                this.SoundType = Sound.Silent;
            }

            if (Properties.Settings.Default.SoundType == "Inaudible") {
                this.SoundType = Sound.Inaudible;
            }
            this.EnabledDeviceNames = Properties.Settings.Default.EnabledDeviceNames ?? new StringCollection { DEFAULT_AUDIO_DEVICE };
        }
    }
}
