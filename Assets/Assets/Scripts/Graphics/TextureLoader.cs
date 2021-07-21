using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TextureLoader : MonoBehaviour
{
    public Sprite[] texturables;
    public static Dictionary<string, Sprite> textures = new Dictionary<string, Sprite>();


    // Start is called before the first frame update
    void Awake()
    {
        if (!System.IO.Directory.Exists(Application.dataPath + "/" + "tex"))
                System.IO.Directory.CreateDirectory(Application.dataPath + "/" + "tex");

        foreach (Sprite sprite in texturables)
        {
            if (System.IO.File.Exists(Application.dataPath + "/" + "tex" + "/" + sprite.name + ".png"))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(Application.dataPath + "/" + "tex" + "/" + sprite.name + ".png");
                if (bytes.Length > 0)
                {
                    Texture2D tex = Instantiate(texturables[0].texture);
                    
                    tex.LoadImage(bytes);
                    Sprite spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f), tex.width > tex.height ? tex.width : tex.height);
                    spr.name = "ACACACCAC";
                    textures[sprite.name] = spr;
                }
            }
            else
            {
                System.IO.File.Create(Application.dataPath + "/" + "tex" + "/" + sprite.name + ".png");
            }
        }
    }
}