using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSoundScript : MonoBehaviour
{
    public static AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    public static void SoundJump()
    {
        sound.Play();
    }
}
