using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PopulateTextureGrid : MonoBehaviour
{
    private void Start()
    {
        if (!Directory.Exists(Application.dataPath + "/texturepacks"))
        {
            Directory.CreateDirectory(Application.dataPath + "/texturepacks");
        }
        if (!Directory.Exists(Application.dataPath + "/texturepacks/Default"))
        {
            Directory.CreateDirectory(Application.dataPath + "/texturepacks/Default");
        }
        int length = (Application.dataPath + "/texturepacks/").Length;
        foreach (string text in Directory.GetDirectories(Application.dataPath + "/texturepacks/", "*", SearchOption.TopDirectoryOnly))
        {
            Object.Instantiate<GameObject>(this.prefab, base.gameObject.transform).GetComponent<Text>().text = text.Split(new char[]
            {
                '/'
            })[text.Split(new char[]
            {
                '/'
            }).Length - 1];
        }
    }
    public GameObject prefab;
}
