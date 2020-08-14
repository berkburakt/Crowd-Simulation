using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class nextButtonScript : MonoBehaviour
{
    public int bodyCount;
    public bool start;
    public bool test;
    public bool finish;
    public TextMeshPro titleText;
    public TextMeshPro bodyText;
    public TextMeshPro bodyCountDisplay;
    public GameObject textBackground;

    private void Start()
    {
        bodyCount = 1;
        start = true;
        test = false;
        finish = false;
        bodyCountDisplay.enabled = false;
        titleText.text = "Own Body Shape";
        bodyText.text = "Welcome to the experiment. Next, you will be asked to model your own body shape by using the buttons on your controller. You will have to repeat this process ten times. Each time, a new random body will appear in front of you. Your task is to transform this body into a body shape that resembles as close as possible your current body shape. When you are ready to take the experiment press the NEXT button in front of you by moving your controller towards the button.";
    }
    
    IEnumerator beginPlay()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("Manager").GetComponent<Character_Menu_Manager>().begin();
        bodyCountDisplay.enabled = true;
        bodyText.enabled = false;
        titleText.enabled = false;
        textBackground.SetActive(false);
    }

    IEnumerator nextBody()
    {
        GameObject.Find("Manager").GetComponent<Character_Menu_Manager>().clone.SetActive(false);
        bodyCountDisplay.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        bodyCountDisplay.gameObject.SetActive(true);
        GameObject.Find("Manager").GetComponent<Character_Menu_Manager>().clone.SetActive(true);
        GameObject.Find("Manager").GetComponent<Character_Menu_Manager>().next();
    }

    public void idealTest()
    {
        start = true;
        test = false;
        GameObject.Find("Manager").GetComponent<Character_Menu_Manager>().clone.SetActive(false);
        bodyText.enabled = true;
        titleText.enabled = true;
        textBackground.SetActive(true);
        bodyCountDisplay.enabled = false;
        titleText.text = "Ideal Body Shape";
        bodyText.text = "Please design the body shape you would like to have if you could. To do this, as in the previous trials, transform the body that appears in front of you using the buttons in your controller.\n Once you are ready, press the 'next' button to continue.";
        bodyCount = 1;      
    }

    public void finishTest()
    {
        start = false;
        test = false;
        finish = true;
        GameObject.Find("Manager").GetComponent<Character_Menu_Manager>().clone.SetActive(false);
        bodyText.enabled = true;
        titleText.enabled = true;
        textBackground.SetActive(true);
        bodyCountDisplay.gameObject.SetActive(false);
        titleText.text = "Next Stage";
        bodyText.text = "In the next stage, you will be asked to play a game. You will be placed in a <b>maze</b>. You can navigate this maze using your controller as a teleportation device (as you practiced before). In the maze, you will find other people walking around. Find the person with the apple over their head! <b>Your goal is to collect as many apples as you can.</b> To collect an apple, touch it with the controller: a new apple will appear above another person's head. You will have <b>20 minutes</b> to collect as many apples as you can.";
    }

    private void Update()
    {
        bodyCountDisplay.text = "Body " + bodyCount + " of 10";
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.name == "RightHand" || other.name == "LeftHand") && start)
        {
            start = false;
            test = true;
            GameObject.Find("Manager").GetComponent<AudioSource>().Play();
            StartCoroutine(beginPlay());
        } else if ((other.name == "RightHand" || other.name == "LeftHand") && test)
        {
            StartCoroutine(nextBody());
            bodyCount++;
        } else if ((other.name == "RightHand" || other.name == "LeftHand") && finish)
        {
            SceneManager.LoadScene("SMPL_mecanim");
        }

    }
}
