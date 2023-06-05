using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class SceneGenerator : MonoBehaviour
{

    private Dictionary<string, Dictionary<string, string>> _buildingsDict;

    public GameObject twoFloorBuilding;
    public GameObject threeFloorBuilding;

    public GameObject baseGround;

    void Start()
    {
        string buildingsJsonPath = Application.persistentDataPath + "/BuildingInfo.json";
        var buildings_json = File.ReadAllText(buildingsJsonPath);

        _buildingsDict = JsonConvert.DeserializeObject < Dictionary<string, Dictionary<string, string>>>(buildings_json);

        float imageSizeX = int.Parse(_buildingsDict["image_width"]["width"]);
        float imageSizeY = int.Parse(_buildingsDict["image_height"]["height"]);

        foreach (string key in _buildingsDict.Keys)
        {
            if(key != "image_width" && key != "image_height")
            {
                Debug.Log("HELLO");

                float posX = int.Parse(_buildingsDict[key]["PosX"]);
                float posY = int.Parse(_buildingsDict[key]["PosY"]);

                GameObject building;

                if (int.Parse(_buildingsDict[key]["Floors"]) == 2)
                {
                    building = twoFloorBuilding;
                }
                else
                {
                    building = threeFloorBuilding;
                }

                GameObject instantiatedBuilding = Instantiate<GameObject>(building, new Vector3(posX, 50, posY), Quaternion.identity);

                instantiatedBuilding.transform.parent = baseGround.transform;

                Debug.Log("PosX was: " + posX);
                Debug.Log("PosY was: " + posY);

                posX = ShiftNumber(posX, imageSizeX, 0, 0.5f, -0.5f);
                posY = ShiftNumber(posY, imageSizeY, 0, 0.5f, -0.5f);

                Debug.Log("PosX is: " + posX);
                Debug.Log("PosY is: " + posY);

                instantiatedBuilding.transform.localPosition = new Vector3(posX, 0, -posY);

               
            }

        }
    }

    private float ShiftNumber(float num, float oldMax, float oldMin, float newMax, float newMin)
    {
        float OldRange = (oldMax - oldMin);
        float NewRange = (newMax - newMin);
        return (((num - oldMin) * NewRange) / OldRange) + newMin;
    }

}
