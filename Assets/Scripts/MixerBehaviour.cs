using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerBehaviour : MonoBehaviour
{

    private bool isEmpty = true;//whether or not there  is an empty cup in the mixer
    private float mixingTime = 5f;//the time it takes to mix


    public GameObject mixingParticles;   //particles that spawn to indicate mixing
    public GameObject brainSlush; //the finished mixing product

    public SpawnStash spawnStash;
    // Start is called before the first frame update
    void Start()
    {
        isEmpty = true;
    }

    public void StartMixing()
    {
        if (!isEmpty)
        {

            spawnStash.decrementAllSpawned();
            
            lastSpawnedParticles =Instantiate(mixingParticles,transform.position,Quaternion.identity);
            Invoke("FinishMixing", mixingTime);
            isEmpty = true;
        }
    }

    private GameObject lastSpawnedSlush;
    private GameObject lastSpawnedParticles;
    private void FinishMixing()
    {
        Destroy(lastSpawnedParticles);
       lastSpawnedSlush = Instantiate(brainSlush, transform.position, Quaternion.identity);
        
    }

    private GameObject emptyCup;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "emptySlush")
        {
            emptyCup = other.gameObject;
            emptyCup.GetComponent<EmptyCupBehaviour>().isInMixer = true;
            isEmpty = false;
        }
            

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "emptySlush")
        {
            isEmpty = true;
            if (emptyCup != null)
            {
                emptyCup.GetComponent<EmptyCupBehaviour>().isInMixer = true;
                emptyCup = null;
            }
        }
            
    }

}
