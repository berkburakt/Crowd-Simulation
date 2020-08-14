using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerWalk : MonoBehaviour
{

    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 1.0f;
        anim.SetFloat("Speed", 1);
        anim.SetFloat("Direction", 0);
    }

    // Update is called once per frame
    void Update()
    {
        var targetPoint = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
        var targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
        var targetReverseRotation = Quaternion.LookRotation(transform.position - targetPoint, Vector3.up);

        Transform head = transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).transform;

        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        /*transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform.rotation = Quaternion.Euler(30, -20, 0);
        transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).transform.rotation = Quaternion.Euler(30, -20, 0);*/
        /*if (Input.GetKey(KeyCode.UpArrow))
        {

            anim.SetFloat("Direction", 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
            head.LookAt(Camera.main.transform);
            GetComponent<SphereCollider>().enabled = false;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            GetComponent<SphereCollider>().enabled = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (anim)
            {
                anim.SetFloat("Direction", 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetReverseRotation, Time.deltaTime * 5.0f);
                GetComponent<SphereCollider>().enabled = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            GetComponent<SphereCollider>().enabled = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            blendShape00(0.012f, true);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            blendShape00(-0.012f, false);
        }
        if (Input.GetKey("q"))
        {
            blendShape01(true);
        }
        if (Input.GetKey("w"))
        {
            blendShape01(false);
        }
        if (Input.GetKey("space"))
        {
            anim.SetFloat("Speed", 0);
            anim.SetFloat("Direction", 0);
        }
        if (Input.GetKey("s"))
        {
            anim.SetFloat("Speed", 1.0f);
            anim.SetFloat("Direction", 0);
            GetComponent<SphereCollider>().enabled = true;
        }
        if (anim.GetFloat("Speed") == 0)
        {
            GetComponent<SphereCollider>().enabled = false;
        }*/
    }

    void blendShape01(bool pos)
    {
        float blendShapeValuePos;
        float blendShapeValueNeg;
        int bodyIndexPos;
        int bodyIndexNeg;
        if ((gameObject.tag == "body_male" && pos) || (gameObject.tag == "body_female" && pos))
        {
            bodyIndexPos = 3;
            bodyIndexNeg = 2;
        }
        else
        {
            bodyIndexPos = 2;
            bodyIndexNeg = 3;
        }

        blendShapeValuePos = transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(bodyIndexPos);
        blendShapeValueNeg = transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(bodyIndexNeg);
        blendShapeValuePos += 2.5f;
        blendShapeValueNeg -= 2.5f;
        if (blendShapeValuePos >= 100)
        {
            blendShapeValuePos = 100f;
        }
        if (blendShapeValueNeg <= 0)
        {
            blendShapeValueNeg = 0;
        }
        transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(bodyIndexPos, blendShapeValuePos);
        transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(bodyIndexNeg, blendShapeValueNeg);
    }

    void blendShape00(float colliderHeight, bool pos)
    {
        float blendShapeValuePos;
        float blendShapeValueNeg;
        int bodyIndexPos;
        int bodyIndexNeg;
        if ((gameObject.tag == "body_male" && pos) || (gameObject.tag == "body_female" && !pos))
        {
            bodyIndexPos = 1;
            bodyIndexNeg = 0;
        }
        else
        {
            bodyIndexPos = 0;
            bodyIndexNeg = 1;
        }

        blendShapeValuePos = transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(bodyIndexPos);
        blendShapeValueNeg = transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(bodyIndexNeg);
        blendShapeValuePos += 2.5f;
        blendShapeValueNeg -= 2.5f;
        if (blendShapeValuePos >= 100)
        {
            blendShapeValuePos = 100f;
        }
        else
        {
            GetComponent<CapsuleCollider>().height += colliderHeight;
        }
        if (blendShapeValueNeg <= 0)
        {
            blendShapeValueNeg = 0;
        }
        else
        {
            GetComponent<CapsuleCollider>().height += colliderHeight;

        }
        transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(bodyIndexPos, blendShapeValuePos);
        transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(bodyIndexNeg, blendShapeValueNeg);
    }

    float rotateAngle;
    float rotateAngelWall;
    private Quaternion _lookRotation;
    private Vector3 _direction;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
        {

            //_direction = (-other.transform.position - transform.position).normalized;
            //_lookRotation = Quaternion.LookRotation(_direction);
            rotateAngelWall = Random.Range(0, 2) == 0 ? 70 : -70;          
        }
        else if (transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.activeSelf && (other.name == "RightHand" || other.name == "LeftHand"))
        {
            transform.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
            
            FindObjectOfType<SpawnBody>().GetComponent<SpawnBody>().selectRandom();
            GameObject.Find("ScoreSound").GetComponent<AudioSource>().Play();
            GameObject.Find("ScoreSound").GetComponent<updateScore>().UpdateScore();
            
        }
        else if (other.tag != "ground")
        {
            rotateAngle = Random.Range(0, 2) == 0 ? 20 : -20;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "wall")
        {
            transform.rotation *= Quaternion.AngleAxis(rotateAngelWall * Time.deltaTime, Vector3.up);
        } else if(other.tag != "ground")
        {
            transform.rotation *= Quaternion.AngleAxis(rotateAngle * Time.deltaTime, Vector3.up);
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "wall")
        {
            transform.rotation *= Quaternion.AngleAxis(-rotateAngelWall * Time.deltaTime, Vector3.up);
        }
        else if (other.tag != "ground")
        {
            transform.rotation *= Quaternion.AngleAxis(-rotateAngle * Time.deltaTime, Vector3.up);
            
        }
    }
}