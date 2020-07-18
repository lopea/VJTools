using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Lopea.VJ.Core
{

    public enum AudioInputSource
    {
        CSCore,
        Unity
    }

    public enum FFTSize
    {
        _64 = 64,
        _128 = 128,
        _256 = 256,
        _512 = 512,
        _1024 = 1024,
        _2048 = 2048,
        _4096 = 4096,
        _8192 = 8192
    }

    public class AudioCore : MonoBehaviour
    {
        public AudioInputSource source;

        /// <summary>
        /// The quality of FFT data that is being sent
        /// </summary>
        public FFTSize size = FFTSize._2048;

        /// <summary>
        /// Handles all audio for the audio manager to receive 
        /// </summary>
        private IAudioHandler _handler = null;

        //check if the AudioCore is initialized
        private bool _intialized = false;

        private static AudioCore _instance;
        /// <summary>
        /// Global reference to the main AudioCore. There should only be one active in the entire scene (group).
        /// </summary>
        public static AudioCore Instance
        {
            get
            {
                //get AudioCore from scene
                if (_instance == null)
                {
                    var managers = GameObject.FindObjectsOfType<AudioCore>();

                    //too many managers
                    if (managers.Length > 1)
                        Debug.LogWarning("Hey idiot, you have 2 or more AudioCores active. Go fix them you twat.");

                    //no managers found 
                    if (managers.Length == 0)
                    {
                        Debug.LogError("You should probably add an AudioCore if you're planning to use it.");
                        return null;
                    }

                    //set main to first instance
                    _instance = managers[0];
                }

                return _instance;
            }

        }



        void InitializeHandler()
        {
            switch (source)
            {
                case AudioInputSource.CSCore:
                    _handler = new CSCoreHandler();
                    break;
                case AudioInputSource.Unity:
                    print("Lmao not yet.");
                    //FIXME: make this shit work lol
                    break;
            }

            //setup the audio handler
            _handler.Initialize(size);

            
            print("Audio Handler Initialized.");
            print("Using type: " + source.ToString() + " with FFT size of: " + (int)size);
        }



        void Awake()
        {
            InitializeHandler();
        }


        public void GetFFTData(float[] data)
        {
            _handler.GetFFTData(data);
        }



        public void OnDestroy()
        {
            _handler?.Dispose();
            _handler = null;
            if (_instance == this)
                _instance = null;
        }
    }
}
