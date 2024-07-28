using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Video Player component

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play(); // Bắt đầu phát video khi bắt đầu trò chơi
        }
    }
}
