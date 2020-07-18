using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lopea.VJ.Core
{
    public class VisualizerBus : MonoBehaviour
    {


        private VisualizerScene _currentScene = null;

        public bool IsEmpty { get => _currentScene == null; }

        public bool IsSceneActive { get => _currentScene.gameObject.activeSelf; }


        void ShutdownCurrentScene()
        {
            //destroy scene
            Destroy(_currentScene?.gameObject);

            //empty scene reference
            _currentScene = null;
        }


        public void SetSceneTo(VisualizerScene scene, bool startActive = false)
        {
            ShutdownCurrentScene();

            if (scene != null)
            {
                var instance = Instantiate(scene.gameObject, Vector3.zero, Quaternion.identity);
                instance.transform.parent = transform;
                _currentScene = instance.GetComponent<VisualizerScene>();
                instance.SetActive(startActive);
            }
        }

        public void SetSceneActiveState(bool state)
        {
            _currentScene?.gameObject.SetActive(state);
        }
        void OnDestroy()
        {
            ShutdownCurrentScene();
        }

    }
}