using UnityEngine;
using UnityEngine.SceneManagement; // Để chuyển đổi scene

public class LoginButtonHandler : MonoBehaviour
{
    public void OnLoginButtonClick()
    {
        // Chuyển đến scene có tên là "Login"
        SceneManager.LoadScene("Login");
    }
}