using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Material[] woodMaterials;
    public GameObject woodModel;
    int woodMaterialIndex = 0;
    float defaultColliderHeight = 2.022196f;
    BoxCollider[] colliders;
    public Wood woodScript;
    public GameObject savedPotteryBtnPrefab;
    public GameObject contentParent;
    public GameObject listPanel;
    public GameObject gamePanel;
    public GameObject showcasePanel;

    public GameObject gameElements;

    [HideInInspector] public GameObject showcaseModel = null;

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

        woodScript.woodMaterialIndex = woodMaterialIndex;
    }

    public void ChangeWoodMaterial()
    {
        woodMaterialIndex = Random.Range(0, woodMaterials.Length);
        SetWoodMaterial(woodMaterialIndex);

    }

    public void SetWoodMaterial(int materialIndex)
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

    public void ListSavedPotteriesBtn()
    {
        listPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameElements.SetActive(false);

        SaveManager.Instance.LoadPottery();

        for (int i = 0; i < contentParent.transform.childCount; i++)
        {
            Destroy(contentParent.transform.GetChild(i).gameObject);
            Debug.Log("destroying");
        }

        for (int i = 0; i < SaveManager.Instance.potteryDatas.Count; i++)
        {
            Debug.Log("creating");
            GameObject btn = Instantiate(savedPotteryBtnPrefab, contentParent.transform);
            btn.GetComponent<Button>().onClick.AddListener(delegate { SaveManager.Instance.CreatePottery(btn); });
        }

    }

    public void BackToGameFromShowcaseBtn()
    {
        Destroy(showcaseModel);
        showcasePanel.SetActive(false);

        listPanel.SetActive(true);
    }

    public void CloseListPanelBtn()
    {
        listPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameElements.SetActive(true);
    }
}
