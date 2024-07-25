using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public GameObject targetObject; // GameObject muốn vô hiệu hóa

    void Start()
    {
        // Vô hiệu hóa targetObject
        targetObject.SetActive(false);

        // Sau 5 giây, kích hoạt lại targetObject
        Invoke("ReactivateObject", 120f);
    }

    void ReactivateObject()
    {
        targetObject.SetActive(true);
    }
}
