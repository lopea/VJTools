using System;
using System.Collections;
using System.Collections.Generic;
using CSCore;
using CSCore.Codecs.AIFF;
using CSCore.CoreAudioAPI;
using CSCore.DSP;
using CSCore.SoundIn;
using CSCore.Streams;
using CSCore.Streams.Effects;
using Lopea.VJ.Core;
using UnityEngine;


namespace Lopea.VJ.Core
{
    public class CSCoreHandler : IAudioHandler
    {
        private WasapiCapture _soundIn;
        private IWaveSource _source;
        private PitchShifter _pitch;
        private FFTSize size;
        private FftProvider _fft;

        public CSCoreHandler()
        {
        
        }

        public void GetFFTData(float[] data)
        {
            _fft.GetFftData(data);
        }

        public void Dispose()
        {
            _soundIn?.Dispose();
            _source?.Dispose();
            _pitch?.Dispose();
        }

        //most of this code is stolen from the example in the CSCore github so idk what it does 40% of the time
        public void Initialize(FFTSize _size = FFTSize._4096)
        {
            size = _size;
            _soundIn = new WasapiLoopbackCapture();

            _soundIn.Initialize();
            var soundInSource = new SoundInSource(_soundIn);

            var source = soundInSource.ToSampleSource();

            _fft = new FftProvider(source.WaveFormat.Channels, (FftSize)size);

            var n = new SingleBlockNotificationStream(source);

            n.SingleBlockRead += (s, a) => _fft.Add(a.Left, a.Right);

            _source = n.ToWaveSource(16);
            byte[] buffer = new byte[_source.WaveFormat.BytesPerSecond];
            soundInSource.DataAvailable += (s, aEvent) =>
            {
                int read;
                while ((read = _source.Read(buffer, 0, buffer.Length)) > 0) ;
            };
            _soundIn.Start();
        }


        public void Stop()
        {
            _soundIn.Stop();
        }
        public void Start()
        {
            _soundIn.Start();
        }
    }
}

