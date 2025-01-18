using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;      // 音樂來源
    public float currentTime;
    public float TotalTime = 200;
    public float FlowerMaxCount;
    public float MessageMaxCount;
    private float FlowerCurrentCount;
    private float MessageCurrentCount;


    //public GameObject flowerprefab;          // 生成的物件預製體
    public GameObject[] flowerprefabs;
    public GameObject[] messageprefab;
    public GameObject TD;
    public VideoPlayer SecondVideo;
    //public GameObject bloom;
    //public GameObject Dot;
    public GameObject[] Text;
    public float FlowerspawnInterval;   // 生成花花間隔時間
    public float MessagespawnInterval;   // 生成訊息間隔時間
    public Vector3 positionRange = new Vector3(10, 5, 10); // 隨機位置範圍 (X, Y, Z)
    public Vector2 FlowersizeRange = new Vector2(0.5f, 2f);      // 隨機大小範圍 (最小, 最大)
    public Vector2 MessagesizeRange = new Vector2(0.1f, 1f);
    private float FlowerlastSpawnTime = 0f;  // 上一次生成物件的時間
    private float MessagelastSpawnTime = 0f;
    private List<GameObject> activeFlowers = new List<GameObject>();
    public GameObject FinishCanvas;

    private void Start()
    {
        TD.SetActive(false);
        FinishCanvas.SetActive(false);
        foreach (GameObject text in Text)
        {
            text.SetActive(false);
        }
    }

    void Update()
    {
        currentTime = audioSource.time;  // 取得音樂當前播放時間

        // 根據時間控制花花效果
        if (currentTime >= 106f  && currentTime <= 115f)
        {
            // 檢查是否到了生成物件的時間
            if (currentTime - FlowerlastSpawnTime >= FlowerspawnInterval)
            {
                FlowerRandomObject();
                FlowerlastSpawnTime = currentTime; // 更新最後一次生成時間
            }



        }
        if (currentTime >= 115f)
        {
            foreach (GameObject flower in activeFlowers)
            {
                Destroy(flower); // 刪除物件
            }
            activeFlowers.Clear(); // 清空追蹤列表
        }



        if (currentTime >= 107f && currentTime <= 110f)
        {
            Text[0].SetActive(true);

        }

        // 根據時間控制訊息效果
       // if (currentTime >= 114f && currentTime <= 117f)
       // {
           // Debug.Log($"88");
            // 檢查是否到了生成物件的時間
           // if (currentTime - MessagelastSpawnTime >= MessagespawnInterval)
            //{
               // Debug.Log($"96");
                //MessageRandomObject();
               // MessagelastSpawnTime = currentTime; // 更新最後一次生成時間
           // }

           // Text[2].SetActive(true);
           // Text[1].SetActive(false);

       // }

        // 根據時間控制TD算圖出現時間
        if (currentTime >= 150f && currentTime <= 164f)
        {
           TD.SetActive (true);

        }
        else
        {
           TD.SetActive (false);
        }

        //根據時間控制相框出現時間
        if (currentTime >= 101f && currentTime <= 180f)
        {
            SecondVideo.Play();
            

        }
        else
        {
            SecondVideo.Stop();

        }
       
      
        //確認一下
        if (currentTime >= 121f && currentTime <= 126f)
        {
            
            Text[3].SetActive(true);
            Text[2].SetActive(false);

        }
        

        //02:06拍1可以順便個pose～
        if (currentTime >= 126f && currentTime <= 130f)
        {
            
            Text[4].SetActive(true);
            Text[3].SetActive(false);

        }

        //02:06拍1哇這個不錯！
        if (currentTime >= 131f && currentTime <= 133f)
        {
            
            Text[5].SetActive(true);
            Text[4].SetActive(false);

        }


        //02:13拍2再拍一張好不好～這次改另一個pose好了
        if (currentTime >= 134f && currentTime <= 137f)
        {
           
            Text[6].SetActive(true);
            Text[5].SetActive(false);

        }

        //02:13這樣很讚！
        if (currentTime >= 138f && currentTime <= 140f)
        {

            Text[7].SetActive(true);
            Text[6].SetActive(false);

        }

        //02:20拍3 我覺得我們可以再試一個可愛一點的～ 想不到可以把手放在頭上比剪刀當兔寶寶～
        if (currentTime >= 141f && currentTime <= 144f)
        {

            Text[8].SetActive(true);
            Text[7].SetActive(false);

        }

        //02:20拍3 哇超可愛的！
        if (currentTime >= 145f && currentTime <= 147f)
        {

            Text[9].SetActive(true);
            Text[8].SetActive(false);

        }

        //02:27拍4|對不起我想再試試看一個性感一點的
        if (currentTime >= 148f && currentTime <= 150f)
        {

            Text[10].SetActive(true);
            Text[9].SetActive(false);

        }
        else
        {
            Text[10].SetActive(false);
        }



        // 根據時間控制bloom出現時間
        if (currentTime >= 110f && currentTime <= 113f)
        {
            //bloom.SetActive(true);
            Text[1].SetActive(true);
            Text[0].SetActive(false);

        }
        else
        {
            //bloom.SetActive(false);
        }

        // 根據時間控制dot出現時間
        if (currentTime >= 110f && currentTime <= 113f)
        {
            //Dot.SetActive(true);

        }
        else
        {
            //Dot.SetActive(false);
        }
        if(currentTime >= 357f)
        {
            //FinishCanvas.SetActive(true);
        }




    }

    void FlowerRandomObject()
    {
        // 隨機生成位置
        Vector3 randomPosition = new Vector3(
            Random.Range(-positionRange.x, positionRange.x),
            Random.Range(-positionRange.y, positionRange.y),
            Random.Range(-positionRange.z, positionRange.z)
        );

        // 生成物件
        if (FlowerCurrentCount < FlowerMaxCount)
        {
            int randomIndex = Random.Range(0, flowerprefabs.Length);
            GameObject newObject = Instantiate(flowerprefabs[randomIndex], randomPosition, Quaternion.identity);
            FlowerCurrentCount++;

            // 隨機設定大小
            float randomScale = Random.Range(FlowersizeRange.x, FlowersizeRange.y);
            newObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            // 隨機設定滑動方向
            FlowerControl flowerControl = newObject.GetComponent<FlowerControl>();
            if (flowerControl != null)
            {
                // 隨機分配滑動方向 (-1 或 1)
                flowerControl.AllowedSwipeDirection = Random.Range(0, 2) == 0 ? -1 : 1;

                flowerControl.maxMovementRange = Random.Range(6f, 8f); // 隨機範圍 10 ~ 15

                // 註冊到手勢監聽器
                CubeGestureListener.RegisterFlower(flowerControl);
            }
            activeFlowers.Add(newObject);

        }



    }


    void MessageRandomObject()
    {
        Debug.Log($"++");
        // 隨機生成位置
        Vector3 randomPosition = new Vector3(
            Random.Range(-positionRange.x, positionRange.x),
            Random.Range(-positionRange.y, positionRange.y),
            Random.Range(-positionRange.z, positionRange.z)
        );

        // 生成物件
        if (MessageCurrentCount < MessageMaxCount)
        {
            int randomIndex = Random.Range(0, messageprefab.Length);
            GameObject newObject = Instantiate(messageprefab[randomIndex], randomPosition, Quaternion.identity);
            MessageCurrentCount++;

            // 隨機設定大小
            float randomScale = Random.Range(MessagesizeRange.x, MessagesizeRange.y);
            newObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        }



    }
}
