using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class teaservideoControl : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer IntroPlayer;    // 影片播放器
    
    //public VideoPlayer FirstPlayer;
    public VideoPlayer ThirdPlayer;
    public AudioSource audioSource;    // 音樂播放器
    private bool videoStopped = false; // 記錄影片是否已停止
    

    void Start()
    {
        // 開始播放影片和音樂
        IntroPlayer.Play();
        // audioSource.Play();
       
    }

    void Update()
    {
        // 當音樂播放時間超過 60 秒時停止影片
        if (audioSource.time >= 103f)
        {
            IntroPlayer.Stop();     // 停止播放影片
            videoStopped = true;    // 確保影片只停止一次
        }


       // if (audioSource.time >= 60f && audioSource.time <= 103f)
      //  {
            //FirstPlayer.Play();     
            
      //  }
      //  else
       // {
           // FirstPlayer.Stop();
        //    videoStopped = true;
       // }


        if (audioSource.time >= 180f && audioSource.time <= 359f)
        {
            ThirdPlayer.Play();

        }
        else
        {
            ThirdPlayer.Stop();
            videoStopped = true;
        }
   

    }
}
