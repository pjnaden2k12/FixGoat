using UnityEngine;

public class TokenCheck : MonoBehaviour
{
    void Start()
    {
        string token = PlayerPrefs.GetString("token", string.Empty);
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogWarning("Token không hợp lệ hoặc chưa được lưu.");
        }
        else
        {
            Debug.Log("Token hiện tại: " + token);
        }
    }
}
