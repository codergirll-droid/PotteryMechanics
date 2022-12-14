using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public float rotateSpeed = 10f;
    SkinnedMeshRenderer skinnedMeshRenderer;
    public int woodMaterialIndex;

    public static Wood Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void RotateWood()
    {
        transform.Rotate(Vector3.right, rotateSpeed * Time.deltaTime * 10);
    }

    private void FixedUpdate()
    {
        RotateWood();
    }

    public void Hit(int keyIndex, float damage)
    {
        float colliderHeight = 2.022196f;
        float newWeight = skinnedMeshRenderer.GetBlendShapeWeight(keyIndex) + damage *(100f / colliderHeight);
        skinnedMeshRenderer.SetBlendShapeWeight(keyIndex, newWeight);
    }

}
