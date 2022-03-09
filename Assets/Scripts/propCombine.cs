using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propCombine : MonoBehaviour
{
    /// <summary>
    /// When combinable props interact
    /// </summary>
    /// 
    [SerializeField] private string combinableTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == combinableTag)
        {

        }
    }
}
