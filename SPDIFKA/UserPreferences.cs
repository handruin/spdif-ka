using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPDIFKA
{
    class UserPreferences
    {

        public Boolean IsRunning { get; set; }
        public Boolean IsHidden { get; set; }

        public UserPreferences()
        {
            Load();
        }

        public void Save()
        {
            Properties.Settings.Default.IsHidden = this.IsHidden;
            Properties.Settings.Default.IsRunning = this.IsRunning;
            Properties.Settings.Default.Save();
        }

        public void Load() {

            this.IsHidden = Properties.Settings.Default.IsHidden;
            this.IsRunning = Properties.Settings.Default.IsRunning;

        }

    }
}
