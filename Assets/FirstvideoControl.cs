using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FirstvideoControl : MonoBehaviour
{
    public VideoPlayer videoPlayer;    // �v������
    public AudioSource audioSource;    // ���ּ���
    private bool videoStopped = false; // �O���v���O�_�w����

    void Start()
    {
        // �}�l����v���M����
        videoPlayer.Play();
        // audioSource.Play();
    }

    void Update()
    {
        
        if (audioSource.time >= 60f && audioSource.time <= 103f)
        {
            videoPlayer.Stop();     // �����v��
            videoStopped = true;    // �T�O�v���u����@��
        }
    }
}
