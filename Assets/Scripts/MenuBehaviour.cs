using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    public void pause()
    {
        if(SceneManager.GetActiveScene().buildIndex!=0)
            Time.timeScale = 0;
        menu.SetActive(true);
    }
    public void resume()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }


}
