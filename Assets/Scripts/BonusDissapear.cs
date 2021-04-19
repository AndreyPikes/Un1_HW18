using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDissapear : MonoBehaviour
{    
    public static int score = 0; //подсчет очков   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            score++;
            BonusSoundScript.SoundBonus();            
            Destroy(gameObject); //прибавляем очков, воспроизводим звук, удаляем бонус                    
        }
    }    
}
