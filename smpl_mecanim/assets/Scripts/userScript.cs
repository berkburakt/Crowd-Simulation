using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class userScript : MonoBehaviour
{
    Vector3 oldPosition;
    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(oldPosition != transform.position)
        {
            float distance = Vector3.Distance(oldPosition, transform.position);
            GameObject.Find("ScoreSound").GetComponent<updateScore>().UpdateDistance(distance);
            oldPosition = transform.position;
        }
        /*if (SteamVR_Actions.default_Teleport.GetStateDown(SteamVR_Input_Sources.Any))
        {
            oldPosition = transform.position;
            Debug.Log("Old Position: " + oldPosition);
        }
        if (SteamVR_Actions.default_Teleport.GetStateUp(SteamVR_Input_Sources.Any))
        {

            newPosition = transform.position;
            Debug.Log("New Position: " + newPosition);
            float distance = Vector3.Distance(oldPosition, newPosition);
            Debug.Log("Distance: " + distance);
            GameObject.Find("ScoreSound").GetComponent<updateScore>().UpdateDistance(distance);
        }*/
    }
}
