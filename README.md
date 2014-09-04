spdif-ka
========

SPDIF Keep Alive utility

This is a windows-based .net GUI application that is used for a very specific task of keeping the SPDIF connection active.  This is designed to resolve a delay issue when there are no sounds being transmitted over the link which causes some sound cards and receivers/DACs to stop the link.  When a sound is eventually played, there is roughly a 500ms delay in reconnecting the link thereby causing some audio sounds to be missed.

This utility plays a mono-channel WAV file with no sound which tricks the sounds card and mixer in windows into opening the audio connection to the receiver/DAC via the SPDIF link.
