using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FirstvideoControl : MonoBehaviour
{
    public VideoPlayer videoPlayer;    // 影片播放器
    public AudioSource audioSource;    // 音樂播放器
    private bool videoStopped = false; // 記錄影片是否已停止

    void Start()
    {
        // 開始播放影片和音樂
        videoPlayer.Play();
        // audioSource.Play();
    }

    void Update()
    {
        
        if (audioSource.time >= 60f && audioSource.time <= 103f)
        {
            videoPlayer.Stop();     // 停止播放影片
            videoStopped = true;    // 確保影片只停止一次
        }
    }
}
