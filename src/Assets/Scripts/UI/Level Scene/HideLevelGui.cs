using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLevelGui : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectsToHide;

    [SerializeField]
    private KeyCode key = KeyCode.F1;

    private bool visible = true;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (visible)
            {
                SetObjectsVisible(false);
                visible = false;
                return;
            }
            SetObjectsVisible(true);
            visible = true;
        }
    }

    private void SetObjectsVisible(bool visible)
    {
        foreach (GameObject gameObject in objectsToHide)
            gameObject.SetActive(visible);
    }
}
