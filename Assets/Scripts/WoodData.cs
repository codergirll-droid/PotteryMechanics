using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WoodData
{
    int woodMaterialIndex;
    float[] blendShapeValues;

    public WoodData(Wood wood)
    {
        woodMaterialIndex = wood.woodMaterialIndex;

        SkinnedMeshRenderer renderer = wood.GetComponent<SkinnedMeshRenderer>();
        Mesh r = renderer.sharedMesh;
        blendShapeValues = new float[r.blendShapeCount];

        for (int i = 0; i < blendShapeValues.Length; i++)
        {
            blendShapeValues[i] = renderer.GetBlendShapeWeight(i);

        }
    }

}
