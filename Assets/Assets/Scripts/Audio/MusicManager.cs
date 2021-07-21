using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{

    AudioSource looping;
    AudioSource fadeIn;
    static bool debounce;


    // Start is called before the first frame update
    void Start()
    {

        if (debounce) 
        {
            return;
        }
        debounce = true;
        DontDestroyOnLoad(gameObject);

        AudioSource[] songClips = GetComponents<AudioSource>();
        fadeIn = songClips[0];
        looping = songClips[1];

        if (!PlayerPrefs.HasKey("Music Volume"))
        {
            PlayerPrefs.SetFloat("Music Volume", 1f);
        }

        if (!PlayerPrefs.HasKey("FX Volume"))
        {
            PlayerPrefs.SetFloat("FX Volume", 1f);
        }


        fadeIn.volume = PlayerPrefs.GetFloat("Music Volume");
        looping.volume = PlayerPrefs.GetFloat("Music Volume");

        fadeIn.Play();
        looping.PlayScheduled(AudioSettings.dspTime + fadeIn.clip.length);

    }

    public void volumeUpdate(float vol)
    {
        fadeIn.volume = vol;
        looping.volume = vol;
        PlayerPrefs.SetFloat("Music Volume", vol);
    }

}
