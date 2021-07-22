using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureLoader : MonoBehaviour
{
    public Sprite[] texturables;
    public static Dictionary<string, Sprite> textures = new Dictionary<string, Sprite>();

    private static TextureLoader i;

    public static void LoadTextureSet(string folderName)
    {
        if (!Directory.Exists(Application.dataPath + "/texturepacks"))
        {
            Directory.CreateDirectory(Application.dataPath + "/texturepacks");
        }

        print(folderName);

        if (folderName == "Default") { textures = new Dictionary<string, Sprite>(); return; }
        print("this ran");
        foreach (Sprite sprite in TextureLoader.i.texturables)
        {
            if (File.Exists(string.Concat(new string[]
            {
                Application.dataPath,
                "/texturepacks/",
                folderName,
                "/",
                sprite.name,
                ".png"
            })))
            {
                byte[] array2 = File.ReadAllBytes(string.Concat(new string[]
                {
                    Application.dataPath,
                    "/texturepacks/",
                    folderName,
                    "/",
                    sprite.name,
                    ".png"
                }));
                if (array2.Length != 0)
                {
                    Texture2D texture2D = Object.Instantiate<Texture2D>(TextureLoader.i.texturables[0].texture);
                    texture2D.LoadImage(array2);
                    Sprite sprite2 = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), (float)((texture2D.width > texture2D.height) ? texture2D.width : texture2D.height));
                    sprite2.name = "Sprite";
                    TextureLoader.textures[sprite.name] = sprite2;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        TextureLoader.i = this;
        TextureLoader.LoadTextureSet(PlayerPrefs.GetString("Texture", "Default"));
    }

}