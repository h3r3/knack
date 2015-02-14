/*
 *  Copyright 2004, 2005, 2006, 2007, 2008 Riccardo Gerosa.
 *
 *  This file is part of Knack.
 *
 *  Knack is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  Knack is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with Knack.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
#if PLATFORM_WIN32  
    using NAudio.Wave;
#endif

using H3.Utils;

namespace H3.Sound.Wave
{
	/// <summary>
	/// A NAudio sound player
	/// </summary>
	public class SoundOut : WaveProvider32, IDisposable
	{
		const int samplesPerSecond = 44100;
		const int channels = 2;
		int bitsPerSample = 16;
		int bufferBlockBytes = 3200;
		int bufferBytes = 3200 * 8;  // This must be a multiple of bufferBlockBytes !
		int sleepTime = 5;
        WaveOut waveOut;
		FileStream fs;
	    BinaryWriter w;
		bool outputToFile = false;
		float[] floatLeftChannel;
		float[] floatRightChannel;
		bool disposed = false;
		
		public enum StreamingType { Thread, Timer }
		
		public SoundOut() : base(samplesPerSecond, channels)
		{
			//samplesPerSecond = Settings.Instance.GetInt(
			//	"/Settings/Output/Sound/General/SamplesPerSecond");
			bitsPerSample = Settings.Instance.GetInt(
				"/Settings/Output/Sound/General/BitsPerSample");
			//channels = Settings.Instance.GetInt(
			//	"/Settings/Output/Sound/General/Channels");
			bufferBlockBytes = Utils.Settings.Instance.GetInt(
			    "/Settings/Output/Sound/DirectSound/BufferSamples")
				*(bitsPerSample/8)*channels;
			bufferBytes = Settings.Instance.GetInt(
				"/Settings/Output/Sound/DirectSound/Buffers")
				*bufferBlockBytes;
			sleepTime = Settings.Instance.GetInt(
				"/Settings/Output/Sound/DirectSound/ThreadSleepTime");

            waveOut = new WaveOut();
            waveOut.Init(this);
			
			//floatLeftChannel = new float[bufferBlockBytes/2];
			//floatRightChannel = new float[bufferBlockBytes/2];
			/*
			soundWaveFormat.SamplesPerSecond = samplesPerSecond;
			soundWaveFormat.Channels = (short) channels;
			soundWaveFormat.BitsPerSample = (short) bitsPerSample;
			soundWaveFormat.BlockAlign = (short)(soundWaveFormat.Channels * (soundWaveFormat.BitsPerSample / 8));
			soundWaveFormat.AverageBytesPerSecond = soundWaveFormat.BlockAlign * soundWaveFormat.SamplesPerSecond;
			soundWaveFormat.FormatTag = WaveFormatTag.Pcm;
			System.Console.WriteLine(soundWaveFormat.ToString());
			soundBufferDescription = new BufferDescription();
			soundBufferDescription.GlobalFocus = true;
			soundBufferDescription.LocateInSoftware = true;
			soundBufferDescription.BufferBytes = bufferBytes;
			soundBufferDescription.CanGetCurrentPosition = true;
			soundBufferDescription.ControlVolume = true;  
			soundBufferDescription.Format = soundWaveFormat;
			*/
		}
		
		~SoundOut()
		{
			Dispose(false);
		}
		
		public void Dispose()
   		{
     	 	Dispose(true);
     		GC.SuppressFinalize(this);
  		}
		
		protected virtual void Dispose(bool disposing)
  	 	{
			Console.WriteLine("SoundOut disposed");
      		if(!this.disposed)
      		{
         		if(disposing)
         		{
            		// Dispose managed resources.
					Stop();
                    waveOut.Dispose();
                    waveOut = null;
         		}
         		// Release unmanaged resources. 
      		}
      		disposed = true;         
   		}
		
		public void Start() 
		{
            System.Console.WriteLine("SoundOut started");
            waveOut.Play();
			if (outputToFile) {
				fs = new FileStream("knack.raw", FileMode.CreateNew);
		        w = new BinaryWriter(fs);
			}
		}
		
		public void Stop() 
		{
            System.Console.WriteLine("SoundOut stopped");
			if (waveOut != null) {
                waveOut.Stop();
			}
			if (outputToFile) {
				w.Close();
	        	fs.Close();
			}
		}
		
		/// <summary>
		/// Override this method to output a sound
		/// </summary>
		public virtual void Render(float[] leftChannel, float[] rightChannel)
		{ }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            int channelSampleCount = sampleCount / 2;
            if (floatLeftChannel == null || floatRightChannel == null
                || channelSampleCount != floatLeftChannel.Length || channelSampleCount != floatRightChannel.Length)
            {
                floatLeftChannel = new float[channelSampleCount];
                floatRightChannel = new float[channelSampleCount];
            }
            Render(floatLeftChannel, floatRightChannel);
            int pos = offset;
            for (int i = 0; i < channelSampleCount; i++)
            {
                buffer[pos++] = floatLeftChannel[i];
                buffer[pos++] = floatRightChannel[i];
            }
            if (outputToFile)
            {
                if (w != null)
                    for (int i = 0; i < sampleCount; i++)
                    {
                        w.Write(buffer[offset + i]);
                    }
            }
            return sampleCount;
        }
		
	}
   
}
