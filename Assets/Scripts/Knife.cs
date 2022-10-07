using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float movementSpeed;
    Rigidbody rb;
    Vector3 movementVector;
    bool isMoving = false;
    public float hitDamage;
    public Wood wood;
    public ParticleSystem woodParticles;

    public Vector3 initialPos;

    ParticleSystem.EmissionModule woodParticlesEmission;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        woodParticlesEmission = woodParticles.emission;
        initialPos = transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        woodParticlesEmission.enabled = false;

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

    private void OnTriggerStay(Collider other)
    {
        ColliderObj c = other.GetComponent<ColliderObj>();
        if (c != null)
        {
            woodParticlesEmission.enabled = true;
            //woodParticles.transform.position = collision.contacts[0].point;

            c.HitCollider(hitDamage);
            wood.Hit(c.index, hitDamage);
        }
    }

    /*
    private void OnCollisionStay(Collision collision)
    {
        ColliderObj c = collision.collider.GetComponent<ColliderObj>();
        if(c != null)
        {
            woodParticlesEmission.enabled = true;
            woodParticles.transform.position = collision.contacts[0].point;

            c.HitCollider(hitDamage);
            wood.Hit(c.index, hitDamage);
        }
    }
    */
}
