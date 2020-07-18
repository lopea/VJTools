using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//TODO: Add VisualizerBus that stores active nodes and having more ease of use when using each active node.
public class VisualizerCore : MonoBehaviour
{
    private static VisualizerCore _instance;



    [SerializeField]
    private uint _numberOfBusses = 8;

    private List<VisualizerBus> _busses;

    public static VisualizerCore Instance 
    {
        get 
        {
            //This only happens when the VisualizerCore singleton is called for the first time.
            if (_instance == null)
            {
                var Cores = FindObjectsOfType<VisualizerCore>();
                
                //too many cores
                if(Cores.Length > 1)
                {
                    Debug.LogError("VisualizerCore: Too Many VisualizerCores you idiot! Go fix them!");
                }
                //No Visualizer Core when needed 
                else if(Cores.Length == 0)
                {
                    Debug.LogError("VisualizerCore: " +
                    "If you don't properly set up you shit that YOU created," +
                     " then idk what to tell you future me.");
                }

                //send first instance of VisualizerCore that was found.
                _instance = Cores[0];
            }
            return _instance;
        }
    }

    
    private void Awake()
    {
        _busses = new List<VisualizerBus>((int)_numberOfBusses);
        for(uint i = 0; i < _numberOfBusses; i ++)
        {
            //add gameObject representing bus to the scene
            var busGameObject = new GameObject("Bus: " + i);

            //add bus component to the list 
            var newBus = busGameObject.AddComponent<VisualizerBus>();
            
            //parent to the Core gameObject
            busGameObject.transform.parent = transform;

            //add new bus to the list.
            _busses.Add(newBus);
        }
    }

    /// <summary>
    /// Sets a Visualizer Scene to a Visualizer bus
    /// you can specify if the scene should be active by the time the scene gets added to the bus
    /// </summary>
    /// <param name="busNumber"></param>
    /// <param name="scene"></param>
    /// <param name="startActive"></param>
    public void SetSceneToBus(uint busNumber, VisualizerScene scene, bool startActive = false)
    {
        if(busNumber >= _numberOfBusses)
            throw new System.Exception("Bus number given does not exist.");

        _busses[(int)busNumber].SetSceneTo(scene, startActive);
    }

    public void SetBusStatus(uint busNumber, bool state)
    {
        if(busNumber >= _numberOfBusses)
            throw new System.Exception("Bus number given does not exist.");

        _busses[(int)busNumber].SetSceneActiveState(state);
    }
}
