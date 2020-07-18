using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lopea.VJ.Core;

public class NodeTest : MonoBehaviour
{
    [SerializeField]
    VisualizerScene testScene;

    void Start()
    {
        VisualizerCore.Instance.SetSceneToBus(0,testScene,true);
    }
}
