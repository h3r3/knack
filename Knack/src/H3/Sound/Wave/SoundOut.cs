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
		int samplesPerSecond = 44100;
		int channels = 2;
		int bitsPerSample = 16;
		int bufferBlockBytes = 3200;
		int bufferBytes = 3200 * 8;  // This must be a multiple of bufferBlockBytes !
		int sleepTime = 5;
        WaveOut waveOut;
		FileStream fs;
	    BinaryWriter w;
		bool outputToFile = false;
		//int completedPos = 0;
		short[] blockData;
		float[] floatLeftChannel;
		float[] floatRightChannel;
		bool disposed = false;
		
		public enum StreamingType { Thread, Timer }
		
		public SoundOut()
		{
			samplesPerSecond = Settings.Instance.GetInt(
				"/Settings/Output/Sound/General/SamplesPerSecond");
			bitsPerSample = Settings.Instance.GetInt(
				"/Settings/Output/Sound/General/BitsPerSample");
			channels = Settings.Instance.GetInt(
				"/Settings/Output/Sound/General/Channels");
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
            waveOut.Play();
			
			blockData = new short[bufferBlockBytes];
			floatLeftChannel = new float[bufferBlockBytes/2];
			floatRightChannel = new float[bufferBlockBytes/2];
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
			//soundBuffer = new SecondaryBuffer(soundBufferDescription,soundDevice);
			//bufferBytes = soundBuffer.Caps.BufferBytes;
			/*
			if (streamingType == StreamingType.Timer) {
				soundTimer = new System.Timers.Timer(sleepTime);
				soundTimer.Enabled = false;
				soundTimer.Elapsed += new System.Timers.ElapsedEventHandler(SoundTimerElapsed);
			}
			
			blockData.Initialize();
			for (int i = 0; i<bufferBytes/bufferBlockBytes; i++)  {
				//soundBuffer.Write(blockData.Length*i, blockData, LockFlag.EntireBuffer);  
			}
             * */
			
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
		public virtual void Render(float[] leftChannel,float[] rightChannel)
		{ }
		
		private void Render(short[] interleavedChannel) 
        {
			int channelSize = interleavedChannel.Length/2;
				
			float wave;
			
			for (int i=0; i<channelSize; i++) {
				floatLeftChannel[i] = 0.0f;
			}
			for (int i=0; i<channelSize; i++) {
				floatRightChannel[i] = 0.0f;
			}
				
			this.Render(floatLeftChannel,floatRightChannel);
			
			for (int i=0; i<channelSize; i++) {
				wave = floatLeftChannel[i];
				if (wave>1.0f) wave = 1.0f; else if (wave<-1.0f) wave = -1.0f;
				interleavedChannel[i*2] = (short) Math.Round(wave * 32767.0f);
				wave = floatRightChannel[i];
				if (wave>1.0f) wave = 1.0f; else if (wave<-1.0f) wave = -1.0f;
				interleavedChannel[i*2+1] = (short) Math.Round(wave * 32767.0f);
			}
			if (outputToFile) {
				if (w!=null)
				for (int i=0; i<interleavedChannel.Length; i++) {
					w.Write(interleavedChannel[i]);
				}
			}
		}

        int sample;
        float Frequency = 1000;
        float Amplitude = 0.25f;

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;
            for (int n = 0; n < sampleCount; n++)
            {
                buffer[n + offset] = (float)(Amplitude * Math.Sin((2 * Math.PI * sample * Frequency) / sampleRate));
                sample++;
                if (sample >= sampleRate) sample = 0;
            }
            return sampleCount;
        }
		
	}
   
}
