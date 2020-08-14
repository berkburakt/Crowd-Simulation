using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class updateScore : MonoBehaviour
{
    // Start is called before the first frame update


    public TextMeshPro scoreText;
    public TextMeshPro timerText;
    Character[] character = null;
    
    int score;
    int teleportCount;
    float distance;
    float timeLeft = 1200.0f;
    

    private void Start()
    {
        distance = PlayerPrefs.GetFloat("distance");
        teleportCount = PlayerPrefs.GetInt("triggerCount");
        score = PlayerPrefs.GetInt("score");
        scoreText.text = score.ToString();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeLeft / 60F);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        
        timerText.text = niceTime.ToString();
        if (timeLeft < 0)
        {
            timerText.text = "End";
        }
    }
    public void UpdateScore()
    {
        score++;
        PlayerPrefs.SetInt("score", score);
        scoreText.text = score.ToString();
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string value = File.ReadAllText(Application.dataPath + "/save.txt");
            character = JsonHelper.FromJson<Character>(value);
            character[character.Length - 1].score = score;
            string json = JsonHelper.ToJson(character);
            File.WriteAllText(Application.dataPath + "/save.txt", json);
        } else
        {
            Debug.Log("save.txt file doesn't found");
        }
    }

    public void UpdateTrigger()
    {       
        teleportCount++;
        PlayerPrefs.SetInt("triggerCount", teleportCount);
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string value = File.ReadAllText(Application.dataPath + "/save.txt");
            character = JsonHelper.FromJson<Character>(value);
            character[character.Length - 1].teleportCount = teleportCount;
            string json = JsonHelper.ToJson(character);
            File.WriteAllText(Application.dataPath + "/save.txt", json);
        } else
        {
            Debug.Log("save.txt file doesn't found");
        }
    }

    public void UpdateDistance(float newDistance)
    {
        distance += newDistance;
        PlayerPrefs.SetFloat("distance", distance);
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string value = File.ReadAllText(Application.dataPath + "/save.txt");
            character = JsonHelper.FromJson<Character>(value);
            character[character.Length - 1].distance = System.Math.Round(distance, 2);
            string json = JsonHelper.ToJson(character);
            File.WriteAllText(Application.dataPath + "/save.txt", json);
        } else
        {
            Debug.Log("save.txt file doesn't found");
        }
    }

    [Serializable]
    private class Character
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
