using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public float rotateSpeed = 10f;
    void RotateWood()
    {
        transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime * 10);
    }

    private void FixedUpdate()
    {
        RotateWood();
    }

}
