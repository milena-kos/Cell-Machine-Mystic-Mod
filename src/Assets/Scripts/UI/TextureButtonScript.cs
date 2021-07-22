using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureButtonScript : MonoBehaviour
{
    private void Update()
    {
        if (PlayerPrefs.GetString("Texture", "Default") == base.GetComponent<Text>().text)
        {
            base.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            return;
        }
        base.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
    }

    // Token: 0x060000C1 RID: 193 RVA: 0x00006714 File Offset: 0x00004914
    public void OnClick()
    {
        PlayerPrefs.SetString("Texture", base.GetComponent<Text>().text);
        TextureLoader.LoadTextureSet(base.GetComponent<Text>().text);
    }
}
