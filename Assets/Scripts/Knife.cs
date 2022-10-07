using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float movementSpeed;
    Rigidbody rb;
    Vector3 movementVector;
    bool isMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isMoving = Input.GetMouseButton(0);

        if (isMoving)
        {
            movementVector = movementSpeed * Time.deltaTime * new Vector3(Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y"), 0f);
        }


    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.position += movementVector;
        }
    }

}
