using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSensorScript : MonoBehaviour
{
    public bool dead = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Danger"))
        {
            dead = true;
        }
    }
}
