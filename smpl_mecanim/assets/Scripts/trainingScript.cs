using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;

public class trainingScript : MonoBehaviour
{
    // Start is called before the first frame update
    float rotateAngelWall = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
        {
            Debug.Log("annen");
            rotateAngelWall = Random.Range(0, 2) == 0 ? 70 : -70;
        }
        else if (transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.activeSelf && (other.name == "RightHand" || other.name == "LeftHand"))
        {
            transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            GameObject.Find("trainingManager").GetComponent<AudioSource>().Play();
            StartCoroutine(appleAgain());
        } 
    }

    IEnumerator appleAgain()
    {
        yield return new WaitForSeconds(1);
        transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {     
        if (other.tag == "wall")
        {
            transform.rotation *= Quaternion.AngleAxis(rotateAngelWall * Time.deltaTime, Vector3.up);
        }         
    }

    private void OnTriggerExit(Collider other)
    {      
        if (other.tag == "wall")
        {
            transform.rotation *= Quaternion.AngleAxis(-rotateAngelWall * Time.deltaTime, Vector3.up);
        }
    }
}
