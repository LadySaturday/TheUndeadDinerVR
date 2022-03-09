using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeVelocity : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
