using System.Collections;
using UnityEngine;

public class TimerInstantiate : MonoBehaviour
{

    public GameObject timer;
    private GameObject timerClone;
    public GameObject canvas;




    public GameObject instantiateTimer()
    {
        timerClone = Instantiate(timer, canvas.transform);
        timerClone.SetActive(true);
        timerClone.transform.position = gameObject.transform.position+Vector3.down*2;
        return timerClone;

    }

    public void instantiateTimerVoid()
    {
        timerClone = Instantiate(timer, canvas.transform);
        timerClone.SetActive(true);
        timerClone.transform.position = gameObject.transform.position + Vector3.down * 2;

    }
    public void destroyTimer()
    {
        if (timerClone != null)
            Destroy(timerClone.gameObject);
    }



   
}
