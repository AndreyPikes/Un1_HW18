using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSensorScript : MonoBehaviour
{
    public bool permissionToMove = true;
    
    private void OnTriggerStay(Collider other)
    {
        permissionToMove = false;
    }
    private void OnTriggerExit(Collider other)
    {
        permissionToMove = true;
    }
}
