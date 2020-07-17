using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lopea.VJ.Core
{
    public interface IAudioHandler : IDisposable
    {
        void GetFFTData(float[] data);
        void Initialize(FFTSize size);
        void Stop();
        void Start();
    }
}

