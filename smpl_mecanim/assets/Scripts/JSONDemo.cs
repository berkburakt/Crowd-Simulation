using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class JSONDemo : MonoBehaviour
{

    string path;
    string jsonString;
    private JsonData itemData;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Helo World");
        path = Application.streamingAssetsPath + "/Data/hi_bmi.json";
        jsonString = File.ReadAllText(path);
        itemData = JsonMapper.ToObject(jsonString);

        Debug.Log(itemData["females"]["11"][0]);
        double answer = RangeConverter((double)itemData["females"]["11"][0]);
        Debug.Log("New Value: " + answer);
        Debug.Log(Mathf.Sign(-10));
    }
    public double RangeConverter(double OldValue)
    {
        double OldMin = 0;
        double OldMax = 5;
        double NewMin = 0;
        double NewMax = 100;
        double OldRange = (OldMax - OldMin);
        double NewRange = (NewMax - NewMin);
        double NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return NewValue;
    }
}

