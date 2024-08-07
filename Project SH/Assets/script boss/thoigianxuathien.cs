
using UnityEngine;

public class thoigianxuathien : MonoBehaviour
{
    public GameObject targetObject;
    public float timeFail = 240f;
    // Start is called before the first frame update
    void Start()
    {
        targetObject.SetActive(false);
        Invoke("ReactivateObject",timeFail);

        
    }
    void ReactivateObject()
    {

    targetObject.SetActive(true); }

    // Update is called once per frame
   
}
