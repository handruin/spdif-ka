using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace SPDIFKA.Lib {
    /// <summary>
    /// Stream for looping playback
    /// Credit: http://mark-dot-net.blogspot.sg/2009/10/looped-playback-in-net-with-naudio.html
    /// </summary>
    public class LoopStream : WaveStream {
        WaveStream sourceStream;

        /// <summary>
        /// Creates a new Loop stream
        /// </summary>
        /// <param name="sourceStream">The stream to read from. Note: the Read method of this stream should return 0 when it reaches the end
        /// or else we will not loop to the start again.</param>
        public LoopStream(WaveStream sourceStream) {
            this.sourceStream = sourceStream;
            this.EnableLooping = true;
        }

        /// <summary>
        /// Use this to turn looping on or off
        /// </summary>
        public bool EnableLooping { get; set; }

        /// <summary>
        /// Return source stream's wave format
        /// </summary>
        public override WaveFormat WaveFormat => this.sourceStream.WaveFormat;

        /// <summary>
        /// LoopStream simply returns
        /// </summary>
        public override long Length => this.sourceStream.Length;

        /// <summary>
        /// LoopStream simply passes on positioning to source stream
        /// </summary>
        public override long Position {
            get => this.sourceStream.Position;
            set => this.sourceStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count) {
            int totalBytesRead = 0;

            while (totalBytesRead < count) {
                int bytesRead = this.sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0) {
                    if (this.sourceStream.Position == 0 || !this.EnableLooping) {
                        // something wrong with the source stream
                        break;
                    }
                    // loop
                    this.sourceStream.Position = 0;
                }
                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }
    }
}
