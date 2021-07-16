using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloomSetting : MonoBehaviour
{

    //public RendererFeature renderFeature;
    //volumeOverride vo;

    void Start()
    {
        if (PlayerPrefs.GetInt("bloom", 1) == 0)
        {
            GetComponent<Volume>().profile.TryGet<Bloom>(out var bloom);
            bloom.active = false;
        }

        //GetComponent<Volume>().active = false;
    }
}
