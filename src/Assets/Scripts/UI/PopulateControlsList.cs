using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class PopulateControlsList : MonoBehaviour
{
    private void Start()
    {
        TextInfo ti = new CultureInfo("en-US", false).TextInfo;
        foreach (int i in Enum.GetValues(typeof(InputKeys_e)))
        {
            GameObject card = UnityEngine.Object.Instantiate(prefab, gameObject.transform);
            GameObject optionLabel = card.transform.GetChild(0).gameObject;

            optionLabel.GetComponent<TextMeshProUGUI>().text = ti.ToTitleCase(Enum.GetName(typeof(InputKeys_e), i).ToLower().Replace('_', ' '));
        }
    }
    public GameObject prefab;
}