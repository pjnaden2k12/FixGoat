using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public GameObject targetObject; // GameObject muốn vô hiệu hóa
    public float timeFail = 1f;
    void Start()
    {
        // Vô hiệu hóa targetObject
        targetObject.SetActive(false);

        // Sau 5 giây, kích hoạt lại targetObject
        Invoke("ReactivateObject", timeFail);
    }

    void ReactivateObject()
    {
        targetObject.SetActive(true);
    }
}
