using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{
    const string POTTERY_SUB = "/pottery";
    const string POTTERY_COUNT_SUB = "/pottery.count";

    public GameObject woodPrefab;

    public List<WoodData> potteryDatas = new List<WoodData>(); 
    Wood wood;

    public static SaveManager Instance;
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
        wood = Wood.Instance;
    }

    public void SavePottery()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + POTTERY_SUB;
        string countPath = Application.persistentDataPath + POTTERY_COUNT_SUB;


        int potteryCount = 0;

        if (File.Exists(countPath))
        {
            FileStream a = new FileStream(countPath, FileMode.Open);
            potteryCount = (int)formatter.Deserialize(a);
            a.Close();
        }


        path = Application.persistentDataPath + POTTERY_SUB + potteryCount.ToString();

        Debug.Log("Data path is " + path);

        FileStream countStream = new FileStream(countPath, FileMode.Create);

        formatter.Serialize(countStream, potteryCount + 1); //number of potteries
        countStream.Close();

        FileStream stream = new FileStream(path, FileMode.Create);
        WoodData data = new WoodData(wood);

        formatter.Serialize(stream, data);
        stream.Close();


    }

    public void LoadPottery()
    {
        potteryDatas = null;
        potteryDatas = new List<WoodData>();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + POTTERY_SUB;
        string countPath = Application.persistentDataPath + POTTERY_COUNT_SUB;
        int potteryCount = 0;

        if (File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);
            potteryCount = (int)formatter.Deserialize(countStream);
            countStream.Close();
        }
        else
        {
            Debug.LogError("Path not found in " + countPath);
        }

        Debug.Log("Pottery count is " + potteryCount);

        for (int i = 0; i < potteryCount; i++)
        {
            if (File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                WoodData data = formatter.Deserialize(stream) as WoodData;

                stream.Close();

                //instantiate pottery here

                potteryDatas.Add(data);

                //int materialIndex = data.woodMaterialIndex;
                //float[] blendShapeValues = data.blendShapeValues;

                //CreatePottery(materialIndex, blendShapeValues);

            }
            else
            {
                //Debug.LogError("Path not found in " + path + i);
                Debug.Log("No saved potteries available");
            }
        }

        
    }

    public void CreatePottery(GameObject obj) 
    {
        int dataIndex = obj.transform.GetSiblingIndex();
        int materialIndex = potteryDatas[dataIndex].woodMaterialIndex;
        float[] blendShapeValues = potteryDatas[dataIndex].blendShapeValues;

        GameObject newWood = Instantiate(woodPrefab, Vector3.zero, Quaternion.Euler(-90, 0, 0));
        SkinnedMeshRenderer renderer = newWood.GetComponent<SkinnedMeshRenderer>();
        renderer.material = GameManager.Instance.woodMaterials[materialIndex];

        Mesh r = renderer.sharedMesh;

        for (int i = 0; i < r.blendShapeCount; i++)
        {
            renderer.SetBlendShapeWeight(i, blendShapeValues[i]);

        }

        GameManager.Instance.showcaseModel = newWood;
        GameManager.Instance.listPanel.SetActive(false);
        GameManager.Instance.showcasePanel.SetActive(true);
    }

}
 