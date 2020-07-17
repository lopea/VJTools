using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//TODO: Add VisualizerBus that stores active nodes and having more ease of use when using each active node.
public class VisualizerCore : MonoBehaviour
{
    private static VisualizerCore _instance;

    private List<VisualizerNode> LoadedNodes = new List<VisualizerNode>();


    
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


    //TODO: Specify where to add the node (Bus A, B, C or D)
    public void Add(VisualizerNode newNode)
    {
        //Check if current core already contains node 
        if (LoadedNodes.Contains(newNode))
        {
            Debug.LogWarning("VisualizerCore: You are trying to add a node that is already loaded.");
            return;
        }
        
        //add Node to the list
        LoadedNodes.Add(newNode);

        //activate node
        //TODO: Have a newNode.Activate() for more flexability
        newNode.gameObject.SetActive(true);
    }
    

    //TODO: Make so that removing is only based on the Bus that is needed.
    public void Remove(VisualizerNode node)
    {
        //Remove node from list
        LoadedNodes.Remove(node);
        
        //destroy node as it is not needed.
    }
}
