using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGui : MonoBehaviour
{
    public List<GameObject> objects;
    private bool visible = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (visible)
            {
                SetObjectState(false);
                visible = false;
                return;
            }
            SetObjectState(true);
            visible = true;
        }
    }

    private void SetObjectState(bool enabled)
    {
        foreach (GameObject gameObject in objects)
            gameObject.SetActive(enabled);
    }
}
