using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSoundScript : MonoBehaviour
{
    public static AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    public static void SoundDead()
    {
        sound.Play();
    }
}
