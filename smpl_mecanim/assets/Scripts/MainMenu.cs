using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField heightText;
    public Text participantId;
    public Text WarningText;
    public Dropdown genderDropdown;
    public Dropdown groupDropdown;
    SaveObject[] saveObject = null;
    
    int length;
    private void Start()
    {
        Load();
        participantId.text = "ParticipantID: " + length;
    }
    public void Save()
    {
        Load();
        saveObject[length] = new SaveObject();
        saveObject[length].id = length;
        saveObject[length].score = 0;
        saveObject[length].teleportCount = 0;
        saveObject[length].distance = 0;
        saveObject[length].height = System.Math.Round(float.Parse(heightText.text), 2);
        saveObject[length].group = groupDropdown.options[groupDropdown.value].text;
        saveObject[length].gender = genderDropdown.options[genderDropdown.value].text;
        

        string json = JsonHelper.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("triggerCount", 0);
        PlayerPrefs.SetInt("distance", 0);
        PlayerPrefs.SetString("group", groupDropdown.options[groupDropdown.value].text);
        PlayerPrefs.SetInt("preTest", 1);
        //PlayerPrefs.SetFloat("playerHeight", System.Math.Round(float.Parse(heightText.text), 2));
        SceneManager.LoadScene("Training");
    }

    public void Load()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-us");
        
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string value = File.ReadAllText(Application.dataPath + "/save.txt");
            saveObject = JsonHelper.FromJson<SaveObject>(value);
            length = saveObject.Length;
            SaveObject[] temp = new SaveObject[length + 1];
            saveObject.CopyTo(temp, 0);
            saveObject = temp;
        }
        else
        {
            saveObject = new SaveObject[1];
            length = 0;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    [Serializable]
    private class SaveObject
    {
        public int id;
        public int score;
        public int teleportCount;
        public double height;
        public double distance;
        public string gender;
        public string group;
        public List<double> preBodyValues;
        public List<double> preBodyIdealValues;
        public List<double> postBodyValues;
        public List<double> postBodyIdealValues;
        public List<double> preBodyTimes;
        public List<double> preBodyIdealTimes;
        public List<double> postBodyTimes;
        public List<double> postBodyIdealTimes;
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

}
