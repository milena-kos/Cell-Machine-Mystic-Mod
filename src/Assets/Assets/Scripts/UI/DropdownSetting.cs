using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownSetting : MonoBehaviour
{
    public string playerPrefString;
    Dropdown dropdown;

    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.value = PlayerPrefs.GetInt(playerPrefString);
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });
    }

    void DropdownValueChanged(Dropdown change)
    {
        PlayerPrefs.SetInt(playerPrefString, change.value);
    }
}
