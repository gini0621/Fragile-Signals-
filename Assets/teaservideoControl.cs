using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class teaservideoControl : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer IntroPlayer;    // �v������
    
    //public VideoPlayer FirstPlayer;
    public VideoPlayer ThirdPlayer;
    public AudioSource audioSource;    // ���ּ���
    private bool videoStopped = false; // �O���v���O�_�w����
    

    void Start()
    {
        // �}�l����v���M����
        IntroPlayer.Play();
        // audioSource.Play();
       
    }

    void Update()
    {
        // ���ּ���ɶ��W�L 60 ��ɰ���v��
        if (audioSource.time >= 103f)
        {
            IntroPlayer.Stop();     // �����v��
            videoStopped = true;    // �T�O�v���u����@��
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
