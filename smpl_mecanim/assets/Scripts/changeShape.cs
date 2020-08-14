using UnityEngine;

public class changeShape : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        float height = Camera.main.transform.position.y;
        height -= 1.7f;
        blendShape00(height);
        transform.position += new Vector3(0,height,0);
    }
    // Update is called once per frame
    void Update()
    {

        //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation *= Quaternion.AngleAxis(-100f * Time.deltaTime, Vector3.up);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation *= Quaternion.AngleAxis(100f * Time.deltaTime, Vector3.up);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            blendShape01(true);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            blendShape01(false);
        }
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

    void blendShape00(float height)
    {
        float blendShapeValue;
        int bodyIndex;
        if ((gameObject.tag == "body_male" && height > 0))
        {
            bodyIndex = 1;
        }
        else
        {
            bodyIndex = 0;
            height *= -1;
        }
        blendShapeValue = (height * 1000)/2.7f;
        transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(bodyIndex, blendShapeValue);
    }
}