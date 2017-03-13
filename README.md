### SPDIF Keep Alive utility (spdif-ka)
Windows-based .net GUI application used for keeping the [S/PDIF](http://en.wikipedia.org/?title=S/PDIF) connection alive when no sound is playing.

Some sound cards will stop the S/PDIF link when no sound is actively being played.  When a sound is eventually played, there is roughly a 500ms delay in reconnecting the link thereby causing some audio sounds to be missed or a perceived cutoff of sound.  By running spdif-ka, this problem no longer ocurres.   

### How to workaround the issue
This utility plays a mono-channel WAV file which contains no sound.  This tricks the sounds card and/or Windows mixer into opening the audio connection to the receiver/DAC via the S/PDIF link.  With this tool running, you will no longer have a delay.

### Known issues
- If you use utilities like foobar200 with a WASAPI plugin, you may encounter some strange behaviour with this tool.  

### Tested platforms
- At this time, spdif-ka has been tested under Windows 8.1 64-bit, Windows 10 64-bit using the .net framework 4.5.

### Ways to help
- **Fix bugs, add features.** Fix an **[open issue](https://github.com/handruin/spdif-ka/issues?state=open)** on this repo. This spdif-ka utility is an Open Source Project.  Please contribute by recommend enhancements, writing code, testing, fixing bugs, etc.

### Features overview
- Runs in the windows system tray and displays a green icon when the keep-alive is running.
- Support for saving configuration settings so that when you exit the utility the same settings will be applied when spdif-ka is restarted.
- New support for audio selection type.  The inaudible audio option is the default option to be used when needing to keep an audio channel open.  The silent audio option can be used in special cases and is currently experimental for some use-cases.

### GUI Screen captures
- Main screen of the utility

![ScreenShot](/screen-captures/spdif-ka_sc1.jpg)
- Settings tab

![ScreenShot](/screen-captures/spdif-ka_sc2.jpg)
