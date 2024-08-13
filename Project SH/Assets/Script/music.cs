using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Toggle soundToggle; // Toggle để tắt/bật âm thanh
    private AudioSource musicSource;

    void Start()
    {
        // Tìm AudioSource trong scene
        musicSource = FindObjectOfType<AudioSource>();

        // Kiểm tra trạng thái của âm thanh khi bắt đầu
        if (PlayerPrefs.HasKey("SoundEnabled"))
        {
            bool soundEnabled = PlayerPrefs.GetInt("SoundEnabled") == 1;
            soundToggle.isOn = soundEnabled;
            musicSource.mute = !soundEnabled;
        }
        else
        {
            // Mặc định âm thanh bật
            soundToggle.isOn = true;
            musicSource.mute = false;
        }

        // Gán sự kiện cho toggle
        soundToggle.onValueChanged.AddListener(ToggleSound);
    }

    void ToggleSound(bool isOn)
    {
        musicSource.mute = !isOn;
        PlayerPrefs.SetInt("SoundEnabled", isOn ? 1 : 0); // Lưu trạng thái âm thanh
    }
}
