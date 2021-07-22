using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
public class OpenTextureFolder : MonoBehaviour
{
    public void OpenFolderInExplorer()
    {
        if (!Directory.Exists(Application.dataPath + "/texturepacks"))
        {
            Directory.CreateDirectory(Application.dataPath + "/texturepacks");
        }
        EditorUtility.OpenWithDefaultApp(Application.dataPath + "/texturepacks");
    }
}
