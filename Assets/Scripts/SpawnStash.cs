using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using System;

public class SpawnStash : MonoBehaviour
{
    /// <summary>
    /// Placed on an infinite object to replace itself when it's grabbed
    /// </summary>
    //OVRGrabbable oVRGrabbable;

    [SerializeField] private string itemTag;
    private static int spawnLimit=4;
    private static List <GameObject> allSpawned = new List<GameObject>();
    public GameObject itemToBeSpawned;
    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == itemTag )
        {
            if(allSpawned.Count >= spawnLimit)
            {
                Destroy(allSpawned[0]);
                allSpawned.RemoveAt(0);
            }
            Instantiate(itemToBeSpawned, transform.localPosition, transform.localRotation);
            allSpawned.Add(other.gameObject);
            

        }
            
    }

    public void decrementAllSpawned()
    {
        for (int i=0; i< allSpawned.Count; i++)
        {
            if (allSpawned[i].GetComponent<EmptyCupBehaviour>().isInMixer)
            {
                Destroy(allSpawned[i]);
                allSpawned.RemoveAt(i);
            }
                
        }
    }
}
