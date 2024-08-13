using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class UserAccountManager : MonoBehaviour
{
    // Các panel
    public GameObject MainPanel; // Panel chính với nút Đăng ký, Đăng nhập
    public GameObject RegisterPanel; // Panel đăng ký
    public GameObject LoginPanel; // Panel đăng nhập
    public GameObject LoginSuccessPanel; // Panel sau khi đăng nhập thành công
    public GameObject ConfirmDeletePanel; // Panel xác nhận xóa dữ liệu

    // Các trường đăng ký
    public TMP_InputField userRegister;
    public TMP_InputField passwdRegister;
    public TMP_InputField confirmPassword;
    public TextMeshProUGUI thongbaoRegister;

    // Các trường đăng nhập
    public TMP_InputField userLogin;
    public TMP_InputField passwdLogin;
    public TextMeshProUGUI thongbaoLogin;

    // Hàm khởi tạo
    void Start()
    {
        // Kiểm tra trạng thái đăng nhập khi khởi động
        if (PlayerPrefs.HasKey("token"))
        {
            OnLoginSuccess();
        }
        else
        {
            MainPanel.SetActive(true);
        }

        // Ẩn panel xác nhận xóa dữ liệu khi khởi động
        ConfirmDeletePanel.SetActive(false);
    }

    // Mở panel đăng ký
    public void OpenRegisterPanel()
    {
        MainPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }

    // Mở panel đăng nhập
    public void OpenLoginPanel()
    {
        MainPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }

    // Đóng panel đăng ký hoặc đăng nhập, trở về Main Panel
    public void ClosePanel()
    {
        RegisterPanel.SetActive(false);
        LoginPanel.SetActive(false);
        MainPanel.SetActive(true);
    }

    // Đăng ký tài khoản
    public void RegisterButton()
    {
        StartCoroutine(Register());
    }

    // Đăng nhập tài khoản
    public void LoginButton()
    {
        StartCoroutine(Login());
    }

    // Hiển thị panel xác nhận xóa dữ liệu
    public void ShowConfirmDeletePanel()
    {
        ConfirmDeletePanel.SetActive(true);
    }

    // Xóa dữ liệu người dùng
    public void DeleteAllData()
    {
        // Xóa thông tin người dùng
        PlayerPrefs.DeleteAll();

        // Xóa tệp văn bản
        string folderName = "resourceSave";
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        string resourcesFilePath = Path.Combine(folderPath, "resources.txt");
        if (File.Exists(resourcesFilePath))
        {
            File.Delete(resourcesFilePath);
        }

        // Đưa người dùng về màn hình chính
        MainPanel.SetActive(true);
        LoginSuccessPanel.SetActive(false);

        Debug.Log("All data deleted!");

        // Ẩn panel xác nhận
        ConfirmDeletePanel.SetActive(false);
    }

    // Hủy bỏ xóa dữ liệu
    public void CancelDelete()
    {
        ConfirmDeletePanel.SetActive(false);
    }

    // Phương thức xử lý đăng ký
    private IEnumerator Register()
    {
        if (passwdRegister.text != confirmPassword.text)
        {
            thongbaoRegister.text = "Password no same!";
            yield break;
        }

        WWWForm dataForm = new WWWForm();
        dataForm.AddField("user", userRegister.text);
        dataForm.AddField("passwd", passwdRegister.text);

        UnityWebRequest www = UnityWebRequest.Post("https://fpl.expvn.com/dangky.php", dataForm);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            thongbaoRegister.text = "Ket noi khong thanh cong";
        }
        else
        {
            string get = www.downloadHandler.text;
            switch (get)
            {
                case "exist": thongbaoRegister.text = "Tai khoan da ton tai"; break;
                case "OK": thongbaoRegister.text = "Dang ky thanh cong"; break;
                case "ERROR": thongbaoRegister.text = "Dang ky khong thanh cong"; break;
                default: thongbaoRegister.text = "Khong ket noi duoc toi server"; break;
            }
        }
    }

    // Phương thức xử lý đăng nhập
    private IEnumerator Login()
    {
        WWWForm dataForm = new WWWForm();
        dataForm.AddField("user", userLogin.text);
        dataForm.AddField("passwd", passwdLogin.text);

        UnityWebRequest www = UnityWebRequest.Post("https://fpl.expvn.com/dangnhap.php", dataForm);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            thongbaoLogin.text = "Connect Fail";
        }
        else
        {
            string get = www.downloadHandler.text;
            if (get == "empty")
            {
                thongbaoLogin.text = "Emty";
            }
            else if (string.IsNullOrEmpty(get))
            {
                thongbaoLogin.text = "User or pass don't true";
            }
            else if (get.Contains("Error"))
            {
                thongbaoLogin.text = "Sever no connect";
            }
            else
            {
                thongbaoLogin.text = "Login done";
                PlayerPrefs.SetString("token", get);
                OnLoginSuccess();
            }
        }
    }

    // Phương thức xử lý khi đăng nhập thành công
    private void OnLoginSuccess()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(false);
        MainPanel.SetActive(false);
        LoginSuccessPanel.SetActive(true);
    }
}
