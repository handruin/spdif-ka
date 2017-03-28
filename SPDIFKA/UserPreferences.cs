using System.Collections.Specialized;

namespace SPDIFKA {
    public class UserPreferences {
        public const string DEFAULT_AUDIO_DEVICE = "Default playback device";

        public enum Sound { Silent, Inaudible }
        public bool IsRunning { get; set; }
        public bool IsHidden { get; set; }
        public bool IsStartWithWindows { get; set; }
        public bool IsMinimizeToNotificationArea { get; set; }
        public bool IsMinimizedOnClose { get; set; }
        public Sound SoundType { get; set; }
        public StringCollection TargetedDeviceNames { get; set; }

        public UserPreferences() {
            this.Load();
        }

        public void Save() {
            Properties.Settings.Default.IsHidden = this.IsHidden;
            Properties.Settings.Default.IsRunning = this.IsRunning;
            Properties.Settings.Default.IsStartWithWindows = this.IsStartWithWindows;
            Properties.Settings.Default.IsMinimizedNotification = this.IsMinimizeToNotificationArea;
            Properties.Settings.Default.IsMinimizedOnClose = this.IsMinimizedOnClose;
            Properties.Settings.Default.SoundType = this.SoundType.ToString();
            Properties.Settings.Default.TargetedDeviceNames = this.TargetedDeviceNames;
            Properties.Settings.Default.Save();
        }

        public void Load() {
            this.IsHidden = Properties.Settings.Default.IsHidden;
            this.IsRunning = Properties.Settings.Default.IsRunning;
            this.IsMinimizeToNotificationArea = Properties.Settings.Default.IsMinimizedNotification;
            this.IsMinimizedOnClose = Properties.Settings.Default.IsMinimizedOnClose;
            this.IsStartWithWindows = Properties.Settings.Default.IsStartWithWindows;
            if (Properties.Settings.Default.SoundType == "Silent") {
                this.SoundType = Sound.Silent;
            }

            if (Properties.Settings.Default.SoundType == "Inaudible") {
                this.SoundType = Sound.Inaudible;
            }
            this.TargetedDeviceNames = Properties.Settings.Default.TargetedDeviceNames ?? new StringCollection { DEFAULT_AUDIO_DEVICE };
        }
    }
}
