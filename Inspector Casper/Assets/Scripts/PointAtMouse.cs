using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtMouse : MonoBehaviour
{
    private Camera camera;
    private void Start()
    {
        camera = Camera.main;
    }

    void Update () 
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = camera.WorldToViewportPoint (transform.position);
         
        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = camera.ScreenToViewportPoint(Input.mousePosition);
         
        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle+90));
    }

    static float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
