using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontSensorScript : MonoBehaviour
{
    public bool permissionToJump = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Danger"))
            permissionToJump = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Danger"))
            permissionToJump = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Danger"))
            permissionToJump = false;
    }
}
