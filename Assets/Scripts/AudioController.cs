using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioSource music;
    public AudioSource clink;

    public Text sound;

    bool isSoundOn = true;

    void Start()
    {
        isSoundOn = true;
    }

    public void SwitchAudio()
    {
        if (isSoundOn)
        {
            music.mute = true;
            clink.mute = true;
            sound.text = "SOUND IS OFF";
        }
        else
        {
            music.mute = false;
            clink.mute = false;
            sound.text = "SOUND IS ON";
        }
        isSoundOn = !isSoundOn;
    }
}
