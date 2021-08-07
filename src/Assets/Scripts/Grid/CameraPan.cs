using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    private float orthographicSize;
    private float x;
    private float y;
    private Vector3 touchStart;
    private Camera cam;

    private void Start()
    {
		    instance = this;
        cam = base.GetComponent<Camera>();
    }

    public void PositionCamera()
    {
        this.x = (float)CellFunctions.gridWidth * 0.5f - 0.5f;
        this.y = (float)CellFunctions.gridHeight * 0.5f - 0.5f;
        this.orthographicSize = (float)CellFunctions.gridHeight * 0.5f + 2f;
        base.transform.position = new Vector3(this.x, this.y, -10f);
        base.GetComponent<Camera>().orthographicSize = this.orthographicSize;
    }

    public void Update()
	{
      if (Input.touchCount == 2)
      {
          Touch touchZero = Input.GetTouch(0);
          Touch touchOne = Input.GetTouch(1);

          Vector3 touchZeroPrev = touchZero.position - touchZero.deltaPosition;
          Vector3 touchOnePrev = touchOne.position - touchOne.deltaPosition;

          float prevMagnitude = (touchZeroPrev - touchOnePrev).magnitude;
          float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

          float pinchDifference = currentMagnitude - prevMagnitude;

          if (pinchDifference < -3f || pinchDifference > 3f)
          {
              this.orthographicSize -= (pinchDifference / this.orthographicSize) / 2f;
          }

          Vector3 average = cam.ScreenToWorldPoint((touchZero.position + touchOne.position) / 2f);
          Vector3 prevAverage = cam.ScreenToWorldPoint((touchZeroPrev + touchOnePrev) / 2f);
          Vector3 averageDifference = average - prevAverage;

          this.x -= averageDifference.x / 2f;
          this.y -= averageDifference.y / 2f;
      }

      this.orthographicSize -= Input.mouseScrollDelta.y * 0.5f * Mathf.Sqrt(this.orthographicSize);
      if ((this.orthographicSize < 3) || (Single.IsNaN(this.orthographicSize)))
      {
          this.orthographicSize = 3f;
      }
      if (this.orthographicSize > 2000)
      {
          this.orthographicSize = 2000f;
      }


      if (Input.GetKey("left ctrl"))
      {
          this.x += Input.GetAxis("Horizontal") * 0.5f * PlayerPrefs.GetFloat("MovementSpeed", 1f);
          this.y += Input.GetAxis("Vertical") * 0.5f * PlayerPrefs.GetFloat("MovementSpeed", 1f);
      }
      else
      {
          this.x += Input.GetAxis("Horizontal") * 0.2f * PlayerPrefs.GetFloat("MovementSpeed", 1f);
          this.y += Input.GetAxis("Vertical") * 0.2f * PlayerPrefs.GetFloat("MovementSpeed", 1f);
      }
  		base.transform.position = new Vector3(this.x, this.y, -10f);
  		cam.orthographicSize = this.orthographicSize;
	}

    public static CameraPan instance;
}
