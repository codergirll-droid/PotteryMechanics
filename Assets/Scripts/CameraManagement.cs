using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagement : MonoBehaviour
{
    public float fieldOfView;
    Camera c;
    readonly float originalFOV = 60;
    readonly float originalWidth = 1080;
    readonly float originalHeight = 1920;
    float originalAspectRatio;

    private void Start()
    {
        originalAspectRatio = (originalWidth / originalHeight);

        c = GetComponent<Camera>();


        float x = Screen.width;
        float y = Screen.height;
        float newAspectRatio = x / y;

        fieldOfView = (float)(originalAspectRatio * originalFOV) / newAspectRatio;

        c.fieldOfView = fieldOfView;
    }
}
