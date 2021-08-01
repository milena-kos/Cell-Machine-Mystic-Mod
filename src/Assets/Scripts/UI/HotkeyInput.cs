using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotkeyInput : MonoBehaviour, IDeselectHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Dictionary<KeyCode, string> keyNames;

    private void Start()
    {
        keyNames = new InputMap().KeyCodeMap();
    }

    private bool mouseIsOver = false;
    private bool isSelected = false;

    public void OnDeselect(BaseEventData data)
    {
        if (mouseIsOver) return;

        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        isSelected = false;
    }

    public void OnPointerClick(PointerEventData data)
    {
        isSelected = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
        gameObject.GetComponent<Image>().color = new Color32(107, 137, 255, 255);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseIsOver = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseIsOver = false;
    }

    public void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && isSelected)
        {
            if (!keyNames.ContainsKey(e.keyCode)) return;

            string settingName = gameObject.transform.parent.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text.ToUpper().Replace(" ", "_");
            //Event.current.
            bool hasMods = e.modifiers.ToString() != "None";
            GameObject elem = gameObject.transform.GetChild(0).gameObject;
            elem.GetComponent<TMPro.TextMeshProUGUI>().text = (hasMods ? e.modifiers.ToString() + ", " : "") + keyNames[e.keyCode];

            isSelected = false;
            gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            PlayerPrefs.SetString("Keybind-" + settingName, e.keyCode.ToString());
        }
    }
}
