using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public GameObject sliderValueDisplay;
    public string playerPrefString;

    void Start()
    {
        this.GetComponent<Slider>().value = PlayerPrefs.GetFloat(playerPrefString);
        sliderValueDisplay.GetComponent<Text>().text = Mathf.Round(PlayerPrefs.GetFloat(playerPrefString) * 100f) + "%";
    }

    public void updateVolume(float vol)
    {
        PlayerPrefs.SetFloat(playerPrefString, vol);
        sliderValueDisplay.GetComponent<Text>().text = Mathf.Round(vol * 100f) + "%";
    }
}
