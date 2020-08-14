using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;
using System.IO;
using LitJson;

public class SpawnBody : MonoBehaviour

{
    public GameObject BodyMale;
    public GameObject BodyFemale;

    public Vector3 center;
    public Vector3 size;

    public int spawnNumberM;
    public int spawnNumberF;

    public Texture2D[] texturesMale;
    public Texture2D[] texturesFemale;

    public AudioClip[] audios;

    int count = 0;

    string path;
    string jsonString;
    private JsonData itemData;

    string pathLow;
    string jsonStringLow;
    private JsonData itemDataLow;

    GameObject[] bodyCloneArray = null;

    // Start is called before the first frame update
    void Start()
    {
        
        bodyCloneArray = new GameObject[spawnNumberF + spawnNumberM];
        int i;
        path = Application.streamingAssetsPath + "/Data/hi_bmi.json";
        jsonString = File.ReadAllText(path);
        itemData = JsonMapper.ToObject(jsonString);

        pathLow = Application.streamingAssetsPath + "/Data/low_bmi.json";
        jsonStringLow = File.ReadAllText(pathLow);
        itemDataLow = JsonMapper.ToObject(jsonStringLow);

        for (i = 0; i < spawnNumberM; i++)
        {
            Texture2D texture;
            if (i < texturesMale.Length)
            {
                texture = texturesMale[i];
            }
            else
            {
                texture = texturesMale[Random.Range(0,texturesMale.Length)];
            }
            spawnBody(BodyMale, texture, i, "male");
        }

        for(i = 0; i < spawnNumberF; i++)
        {
            Texture2D texture;
            if (i < texturesFemale.Length)
            {
                texture = texturesFemale[i];
            }
            else
            {
                texture = texturesFemale[Random.Range(0, texturesFemale.Length)];
            }
            spawnBody(BodyFemale, texture, i, "female");
        }
        selectRandom();
        StartCoroutine(changeScene());
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(1200);
        PlayerPrefs.SetInt("preTest", 0);
        SceneManager.LoadScene("Character_Room");
    }

    public void selectRandom()
    {
        int rand = Random.Range(0, (spawnNumberM + spawnNumberF));
        bodyCloneArray[rand].gameObject.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions.default_Teleport.GetStateDown(SteamVR_Input_Sources.Any))
        {
            GameObject.Find("ScoreSound").GetComponent<updateScore>().UpdateTrigger();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.SetInt("preTest", 0);
            SceneManager.LoadScene("Character_Room");
        }
    }

    public void spawnBody(GameObject body, Texture2D texture, int g, string gender)
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));      
        GameObject clone = Instantiate(body, pos, Quaternion.Euler(new Vector3(0, Random.Range(0.0f, 360.0f), 0)));
        clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetTexture("_MainTex", texture);
        if(PlayerPrefs.GetString("group") == "Group 1")
        {
            if(gender == "female")
            {
                double one = (double)itemData["females"][g][0];
                double two = (double)itemData["females"][g][1];
                double three = (double)itemData["females"][g][2];
                double four = (double)itemData["females"][g][3];
                double five = (double)itemData["females"][g][4];
                double six = (double)itemData["females"][g][5];
                double seven = (double)itemData["females"][g][6];
                double eight = (double)itemData["females"][g][7];
                double nine = (double)itemData["females"][g][8];
                double ten = (double)itemData["females"][g][9];


                if (one < 0)
                {
                    double c_one = -RangeConverter((double)itemData["females"][g][0]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, (float)c_one);
                }
                else
                {
                    double c_one = RangeConverter((double)itemData["females"][g][0]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, (float)c_one);
                }
                if (two < 0)
                {
                    double c_two = -RangeConverter((double)itemData["females"][g][1]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(3, (float)c_two);
                }
                else
                {
                    double c_two = RangeConverter((double)itemData["females"][g][1]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(2, (float)c_two);
                }
                if (three < 0)
                {
                    double c_three = -RangeConverter((double)itemData["females"][g][2]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(5, (float)c_three);
                }
                else
                {
                    double c_three = RangeConverter((double)itemData["females"][g][2]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(4, (float)c_three);
                }
                if (four < 0)
                {
                    double c_four = -RangeConverter((double)itemData["females"][g][3]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(7, (float)c_four);
                }
                else
                {
                    double c_four = RangeConverter((double)itemData["females"][g][3]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(6, (float)c_four);
                }
                if (five < 0)
                {
                    double c_five = -RangeConverter((double)itemData["females"][g][4]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(9, (float)c_five);
                }
                else
                {
                    double c_five = RangeConverter((double)itemData["females"][g][4]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(8, (float)c_five);
                }
                if (six < 0)
                {
                    double c_six = -RangeConverter((double)itemData["females"][g][5]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(11, (float)c_six);
                }
                else
                {
                    double c_six = RangeConverter((double)itemData["females"][g][5]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(10, (float)c_six);
                }
                if (seven < 0)
                {
                    double c_seven = -RangeConverter((double)itemData["females"][g][6]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(13, (float)c_seven);
                }
                else
                {
                    double c_seven = RangeConverter((double)itemData["females"][g][6]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(12, (float)c_seven);
                }
                if (eight < 0)
                {
                    double c_eight = -RangeConverter((double)itemData["females"][g][7]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(15, (float)c_eight);
                }
                else
                {
                    double c_eight = RangeConverter((double)itemData["females"][g][7]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(14, (float)c_eight);
                }
                if (nine < 0)
                {
                    double c_nine = -RangeConverter((double)itemData["females"][g][8]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(17, (float)c_nine);
                }
                else
                {
                    double c_nine = RangeConverter((double)itemData["females"][g][8]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(16, (float)c_nine);
                }
                if (ten < 0)
                {
                    double c_ten = -RangeConverter((double)itemData["females"][g][9]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(19, (float)c_ten);
                }
                else
                {
                    double c_ten = RangeConverter((double)itemData["females"][g][9]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(18, (float)c_ten);
                }

            }
            else
            {

                double one = RangeConverter((double)itemData["males"][g][0]);
                double two = RangeConverter((double)itemData["males"][g][1]);
                double three = RangeConverter((double)itemData["males"][g][2]);
                double four = RangeConverter((double)itemData["males"][g][3]);
                double five = RangeConverter((double)itemData["males"][g][4]);
                double six = RangeConverter((double)itemData["males"][g][5]);
                double seven = RangeConverter((double)itemData["males"][g][6]);
                double eight = RangeConverter((double)itemData["males"][g][7]);
                double nine = RangeConverter((double)itemData["males"][g][8]);
                double ten = RangeConverter((double)itemData["males"][g][9]);

                if (one < 0)
                {
                    double c_one = -RangeConverter((double)itemData["males"][g][0]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, (float)c_one);
                }
                else
                {
                    double c_one = RangeConverter((double)itemData["males"][g][0]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, (float)c_one);
                }
                if (two < 0)
                {
                    double c_two = -RangeConverter((double)itemData["males"][g][1]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(3, (float)c_two);
                }
                else
                {
                    double c_two = RangeConverter((double)itemData["males"][g][1]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(2, (float)c_two);
                }
                if (three < 0)
                {
                    double c_three = -RangeConverter((double)itemData["males"][g][2]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(5, (float)c_three);
                }
                else
                {
                    double c_three = RangeConverter((double)itemData["males"][g][2]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(4, (float)c_three);
                }
                if (four < 0)
                {
                    double c_four = -RangeConverter((double)itemData["males"][g][3]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(7, (float)c_four);
                }
                else
                {
                    double c_four = RangeConverter((double)itemData["males"][g][3]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(6, (float)c_four);
                }
                if (five < 0)
                {
                    double c_five = -RangeConverter((double)itemData["males"][g][4]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(9, (float)c_five);
                }
                else
                {
                    double c_five = RangeConverter((double)itemData["males"][g][4]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(8, (float)c_five);
                }
                if (six < 0)
                {
                    double c_six = -RangeConverter((double)itemData["males"][g][5]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(11, (float)c_six);
                }
                else
                {
                    double c_six = RangeConverter((double)itemData["males"][g][5]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(10, (float)c_six);
                }
                if (seven < 0)
                {
                    double c_seven = -RangeConverter((double)itemData["males"][g][6]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(13, (float)c_seven);
                }
                else
                {
                    double c_seven = RangeConverter((double)itemData["males"][g][6]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(12, (float)c_seven);
                }
                if (eight < 0)
                {
                    double c_eight = -RangeConverter((double)itemData["males"][g][7]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(15, (float)c_eight);
                }
                else
                {
                    double c_eight = RangeConverter((double)itemData["males"][g][7]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(14, (float)c_eight);
                }
                if (nine < 0)
                {
                    double c_nine = -RangeConverter((double)itemData["males"][g][8]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(17, (float)c_nine);
                }
                else
                {
                    double c_nine = RangeConverter((double)itemData["males"][g][8]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(16, (float)c_nine);
                }
                if (ten < 0)
                {
                    double c_ten = -RangeConverter((double)itemData["males"][g][9]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(19, (float)c_ten);
                }
                else
                {
                    double c_ten = RangeConverter((double)itemData["males"][g][9]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(18, (float)c_ten);
                }
            }

            
        } else //Else if the BMI is low Group2
        {
            if (gender == "female")
            {
                double one = (double)itemDataLow["females"][g][0];
                double two = (double)itemDataLow["females"][g][1];
                double three = (double)itemDataLow["females"][g][2];
                double four = (double)itemDataLow["females"][g][3];
                double five = (double)itemDataLow["females"][g][4];
                double six = (double)itemDataLow["females"][g][5];
                double seven = (double)itemDataLow["females"][g][6];
                double eight = (double)itemDataLow["females"][g][7];
                double nine = (double)itemDataLow["females"][g][8];
                double ten = (double)itemDataLow["females"][g][9];


                if (one < 0)
                {
                    double c_one = -RangeConverter((double)itemDataLow["females"][g][0]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, (float)c_one);
                }
                else
                {
                    double c_one = RangeConverter((double)itemDataLow["females"][g][0]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, (float)c_one);
                }
                if (two < 0)
                {
                    double c_two = -RangeConverter((double)itemDataLow["females"][g][1]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(3, (float)c_two);
                }
                else
                {
                    double c_two = RangeConverter((double)itemDataLow["females"][g][1]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(2, (float)c_two);
                }
                if (three < 0)
                {
                    double c_three = -RangeConverter((double)itemDataLow["females"][g][2]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(5, (float)c_three);
                }
                else
                {
                    double c_three = RangeConverter((double)itemDataLow["females"][g][2]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(4, (float)c_three);
                }
                if (four < 0)
                {
                    double c_four = -RangeConverter((double)itemDataLow["females"][g][3]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(7, (float)c_four);
                }
                else
                {
                    double c_four = RangeConverter((double)itemDataLow["females"][g][3]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(6, (float)c_four);
                }
                if (five < 0)
                {
                    double c_five = -RangeConverter((double)itemDataLow["females"][g][4]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(9, (float)c_five);
                }
                else
                {
                    double c_five = RangeConverter((double)itemDataLow["females"][g][4]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(8, (float)c_five);
                }
                if (six < 0)
                {
                    double c_six = -RangeConverter((double)itemDataLow["females"][g][5]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(11, (float)c_six);
                }
                else
                {
                    double c_six = RangeConverter((double)itemDataLow["females"][g][5]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(10, (float)c_six);
                }
                if (seven < 0)
                {
                    double c_seven = -RangeConverter((double)itemDataLow["females"][g][6]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(13, (float)c_seven);
                }
                else
                {
                    double c_seven = RangeConverter((double)itemDataLow["females"][g][6]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(12, (float)c_seven);
                }
                if (eight < 0)
                {
                    double c_eight = -RangeConverter((double)itemDataLow["females"][g][7]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(15, (float)c_eight);
                }
                else
                {
                    double c_eight = RangeConverter((double)itemDataLow["females"][g][7]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(14, (float)c_eight);
                }
                if (nine < 0)
                {
                    double c_nine = -RangeConverter((double)itemDataLow["females"][g][8]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(17, (float)c_nine);
                }
                else
                {
                    double c_nine = RangeConverter((double)itemDataLow["females"][g][8]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(16, (float)c_nine);
                }
                if (ten < 0)
                {
                    double c_ten = -RangeConverter((double)itemDataLow["females"][g][9]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(19, (float)c_ten);
                }
                else
                {
                    double c_ten = RangeConverter((double)itemDataLow["females"][g][9]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(18, (float)c_ten);
                }

            }
            else
            {

                double one = RangeConverter((double)itemDataLow["males"][g][0]);
                double two = RangeConverter((double)itemDataLow["males"][g][1]);
                double three = RangeConverter((double)itemDataLow["males"][g][2]);
                double four = RangeConverter((double)itemDataLow["males"][g][3]);
                double five = RangeConverter((double)itemDataLow["males"][g][4]);
                double six = RangeConverter((double)itemDataLow["males"][g][5]);
                double seven = RangeConverter((double)itemDataLow["males"][g][6]);
                double eight = RangeConverter((double)itemDataLow["males"][g][7]);
                double nine = RangeConverter((double)itemDataLow["males"][g][8]);
                double ten = RangeConverter((double)itemDataLow["males"][g][9]);

                if (one < 0)
                {
                    double c_one = -RangeConverter((double)itemDataLow["males"][g][0]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(1, (float)c_one);
                }
                else
                {
                    double c_one = RangeConverter((double)itemDataLow["males"][g][0]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, (float)c_one);
                }
                if (two < 0)
                {
                    double c_two = -RangeConverter((double)itemDataLow["males"][g][1]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(3, (float)c_two);
                }
                else
                {
                    double c_two = RangeConverter((double)itemDataLow["males"][g][1]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(2, (float)c_two);
                }
                if (three < 0)
                {
                    double c_three = -RangeConverter((double)itemDataLow["males"][g][2]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(5, (float)c_three);
                }
                else
                {
                    double c_three = RangeConverter((double)itemDataLow["males"][g][2]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(4, (float)c_three);
                }
                if (four < 0)
                {
                    double c_four = -RangeConverter((double)itemDataLow["males"][g][3]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(7, (float)c_four);
                }
                else
                {
                    double c_four = RangeConverter((double)itemDataLow["males"][g][3]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(6, (float)c_four);
                }
                if (five < 0)
                {
                    double c_five = -RangeConverter((double)itemDataLow["males"][g][4]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(9, (float)c_five);
                }
                else
                {
                    double c_five = RangeConverter((double)itemDataLow["males"][g][4]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(8, (float)c_five);
                }
                if (six < 0)
                {
                    double c_six = -RangeConverter((double)itemDataLow["males"][g][5]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(11, (float)c_six);
                }
                else
                {
                    double c_six = RangeConverter((double)itemDataLow["males"][g][5]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(10, (float)c_six);
                }
                if (seven < 0)
                {
                    double c_seven = -RangeConverter((double)itemDataLow["males"][g][6]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(13, (float)c_seven);
                }
                else
                {
                    double c_seven = RangeConverter((double)itemDataLow["males"][g][6]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(12, (float)c_seven);
                }
                if (eight < 0)
                {
                    double c_eight = -RangeConverter((double)itemDataLow["males"][g][7]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(15, (float)c_eight);
                }
                else
                {
                    double c_eight = RangeConverter((double)itemDataLow["males"][g][7]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(14, (float)c_eight);
                }
                if (nine < 0)
                {
                    double c_nine = -RangeConverter((double)itemDataLow["males"][g][8]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(17, (float)c_nine);
                }
                else
                {
                    double c_nine = RangeConverter((double)itemDataLow["males"][g][8]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(16, (float)c_nine);
                }
                if (ten < 0)
                {
                    double c_ten = -RangeConverter((double)itemDataLow["males"][g][9]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(19, (float)c_ten);
                }
                else
                {
                    double c_ten = RangeConverter((double)itemDataLow["males"][g][9]);
                    clone.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(18, (float)c_ten);
                }
            }
        }
        clone.transform.GetComponent<Animator>().SetFloat("Speed", 1.0f);
        bodyCloneArray[count] = clone;
        /*if (count == rand)
        {
            clone.gameObject.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
        }*/
        count++;
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
