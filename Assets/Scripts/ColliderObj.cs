using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderObj : MonoBehaviour
{
    public int index;

    BoxCollider collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
        index = transform.GetSiblingIndex();
    }

    public void HitCollider(float damage)
    {
        if(collider.size.y - damage > 0.5f)
        {
            collider.size = new Vector3(collider.size.x, collider.size.y - damage, collider.size.z);
        }
        else
        {
            Destroy(this);
        }
    }

}
