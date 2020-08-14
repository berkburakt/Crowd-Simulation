using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;

public class Character_Menu_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    Character[] character = null;

    public GameObject bodyMale;
    public GameObject bodyFemale;
    //public TextMeshPro instrText;

    public GameObject clone;
    int count = 0;
    int bodyCount = 1;
    int index;

    float timer;
    bool paused;

    public void begin()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-us");
        timer = 0;
        paused = false;
        GetComponent<AudioSource>().Play();
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string value = File.ReadAllText(Application.dataPath + "/save.txt");
            character = JsonHelper.FromJson<Character>(value);
            index = character.Length - 1;
            if (character[index].gender == "Male")
            {
                clone = Instantiate(bodyMale, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0));
            }
            else
            {
                clone = Instantiate(bodyFemale, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0));
            }
            //clone.transform.position = new Vector3(0, 0.27f, 0);
            //clone.transform.position = new Vector3(0, 0.0f, 0);
            float height = float.Parse(character[index].height.ToString());
            height -= 1.65f;
            blendShape00(height);
            clone.transform.position += new Vector3(0, height, 0);
            randomBodyShape();
        }
        else
        {
            Debug.Log("save.txt file doesn't exist");
        }
    }

    public void randomBodyShape()
    {
        clone.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(3, UnityEngine.Random.Range(0, 100));
        clone.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(2, UnityEngine.Random.Range(0, 100));
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            timer += Time.deltaTime;
        }   
        if (SteamVR_Actions.default_GrabPinch.GetState(SteamVR_Input_Sources.Any))
        {
            Vector2 axis = SteamVR_Actions.default_TouchpadTouch.GetAxis(SteamVR_Input_Sources.Any);
            Debug.Log(axis);
            if (axis.x >= 0.6f)
            {
                clone.transform.rotation *= Quaternion.AngleAxis(-100f * Time.deltaTime, Vector3.up);
            }
            if (axis.x <= -0.6f)
            {
                clone.transform.rotation *= Quaternion.AngleAxis(100f * Time.deltaTime, Vector3.up);
            }
            if (axis.y <= -0.6f)
            {
                blendShape01(false);
            }
            if (axis.y >= 0.6f)
            {
                blendShape01(true);
            }
        }
    }

    public void next()
    {
        GetComponent<AudioSource>().Play();
        float value = clone.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(2) - clone.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(3); ;
        paused = false;
        if (count < 10)
        {
            if (PlayerPrefs.GetInt("preTest") == 1)
            {
                character[index].preBodyValues.Add(System.Math.Round(value, 2));
                character[index].preBodyTimes.Add(System.Math.Round(timer, 2));
            }
            else
            {
                character[index].postBodyValues.Add(System.Math.Round(value, 2));
                character[index].postBodyTimes.Add(System.Math.Round(timer, 2));
            }

        }      
        else
        {
            if (PlayerPrefs.GetInt("preTest") == 1)
            {
                character[index].preBodyIdealValues.Add(System.Math.Round(value, 2));
                character[index].preBodyIdealTimes.Add(System.Math.Round(timer, 2));
            }
            else
            {
                character[index].postBodyIdealValues.Add(System.Math.Round(value, 2));
                character[index].postBodyIdealTimes.Add(System.Math.Round(timer, 2));
            }

        }
        if (count == 9)
        {
            GameObject.Find("nextButton").GetComponent<nextButtonScript>().idealTest();
            paused = true;
        }
        count++;
        if (count == 20 && PlayerPrefs.GetInt("preTest") == 1)
        {
            GameObject.Find("nextButton").GetComponent<nextButtonScript>().finishTest();
        }
        else if (count == 20 && PlayerPrefs.GetInt("preTest") == 0)
        {
            SceneManager.LoadScene("Menu");
        }
        timer = 0;
        string json = JsonHelper.ToJson(character);
        File.WriteAllText(Application.dataPath + "/save.txt", json);

        randomBodyShape();
    }

    void blendShape01(bool pos)
    {
        float blendShapeValuePos;
        float blendShapeValueNeg;

        blendShapeValuePos = clone.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(3);
        blendShapeValueNeg = clone.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(2);
        

        if (blendShapeValuePos >= 0 && blendShapeValuePos < 100 && pos)
        {
            blendShapeValuePos += 1f;
            if(blendShapeValuePos > 100)
            {
                blendShapeValuePos = 100;
            }
        } else if (blendShapeValuePos > 0 && blendShapeValuePos <= 100 && !pos)
        {
            blendShapeValuePos -= 1f;
            if (blendShapeValuePos < 0)
            {
                blendShapeValuePos = 0;
            }
        } else if (blendShapeValueNeg > 0 && blendShapeValueNeg <= 100 && pos)
        {
            blendShapeValueNeg -= 1f;
            if (blendShapeValueNeg < 0)
            {
                blendShapeValueNeg = 0;
            }
        } else if(blendShapeValueNeg >= 0 && blendShapeValueNeg < 100 && !pos)
        {
            blendShapeValueNeg += 1f;
            if (blendShapeValueNeg > 100)
            {
                blendShapeValueNeg = 100;
            }
        }
        clone.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(3, blendShapeValuePos);
        clone.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(2, blendShapeValueNeg);

    }

    void blendShape00(float height)
    {
        float blendShapeValue;
        int bodyIndex;
        if (character[index].gender == "Male" && height > 0)
        {
            bodyIndex = 1;
        } else if (character[index].gender == "Female" && height > 0)
        {
            bodyIndex = 0;
        } else if (character[index].gender == "Female" && height < 0)
        {
            bodyIndex = 1;
            // Just negativing the height because blendshape value cannot be negative value.
            height *= -1;
        }
        else
        {
            bodyIndex = 0;
            // Just negativing the height because blendshape value cannot be negative value.
            height *= -1;
        }
        blendShapeValue = (height * 1000) / 2.7f;
        clone.gameObject.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(bodyIndex, blendShapeValue);
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
