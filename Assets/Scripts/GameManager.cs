using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Material[] woodMaterials;
    public GameObject woodModel;
    int woodMaterialIndex = 0;
    float defaultColliderHeight = 2.022196f;
    BoxCollider[] colliders;
    Wood woodScript;

    public static GameManager Instance;
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
    }

    private void Start()
    {
        int randomMat = Random.Range(0, woodMaterials.Length);
        woodMaterialIndex = randomMat;
        SetWoodMaterial(randomMat);

        ColliderObj[] colliderObjects = FindObjectsOfType<ColliderObj>();

        colliders = new BoxCollider[colliderObjects.Length];
        for (int i = 0; i < colliderObjects.Length; i++)
        {
            colliders[i] = colliderObjects[i].gameObject.GetComponent<BoxCollider>();
        }

        woodScript = Wood.Instance;
        woodScript.woodMaterialIndex = woodMaterialIndex;
    }

    public void ChangeWoodMaterial()
    {
        woodMaterialIndex = Random.Range(0, woodMaterials.Length);
        SetWoodMaterial(woodMaterialIndex);

    }

    void SetWoodMaterial(int materialIndex)
    {
        woodModel.GetComponent<SkinnedMeshRenderer>().material = woodMaterials[materialIndex];
        woodScript.woodMaterialIndex = materialIndex;
    }

    public void ResetWood()
    {
        SkinnedMeshRenderer renderer= woodModel.GetComponent<SkinnedMeshRenderer>();
        Mesh r = renderer.sharedMesh;

        for (int i = 0; i < r.blendShapeCount; i++)
        {
            renderer.SetBlendShapeWeight(i, 0);
            colliders[i].size = new Vector3(colliders[i].size.x,
                defaultColliderHeight, colliders[i].size.z);
        }
    }

}
