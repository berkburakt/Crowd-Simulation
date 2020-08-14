using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class trainingManager : MonoBehaviour
{
    public GameObject body;
    public TextMeshPro instrText;
    bool training2 = false;
    GameObject clone;

    void Start()
    {
        //instrText = GetComponent<TextMeshPro>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !training2)
        {
            //instrText.SetText("It Wroks!!!");
            instrText.text = "Training 2: Collecting Apples\n\nYour task is to collect the apple by moving towards the person and touch the apple with the controller";
            clone = Instantiate(body, new Vector3(0, 0, 0), Quaternion.identity);
            clone.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
            Destroy(clone.GetComponent<PlayerWalk>());
            clone.GetComponent<Animator>().speed = 1.0f;
            clone.GetComponent<Animator>().SetFloat("Speed", 1);
            clone.GetComponent<Animator>().SetFloat("Direction", 0);
            clone.AddComponent<trainingScript>();
            training2 = true;
        }
        else if (Input.GetKeyDown(KeyCode.Return) && training2)
        {
            clone.transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            SceneManager.LoadScene("Character_Room");
        }
    }
}
