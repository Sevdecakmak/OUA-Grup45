using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndSceneSwitcher : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public int nextSceneIndex;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Video bittiğinde OnVideoEnd metodunu çağır
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Video bittiğinde sahneyi değiştir
        SceneManager.LoadScene(nextSceneIndex);
    }
}

