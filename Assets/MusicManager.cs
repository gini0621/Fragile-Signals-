using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;      // ���֨ӷ�
    public float currentTime;
    public float TotalTime = 200;
    public float FlowerMaxCount;
    public float MessageMaxCount;
    private float FlowerCurrentCount;
    private float MessageCurrentCount;


    //public GameObject flowerprefab;          // �ͦ�������w�s��
    public GameObject[] flowerprefabs;
    public GameObject[] messageprefab;
    public GameObject TD;
    public VideoPlayer SecondVideo;
    //public GameObject bloom;
    //public GameObject Dot;
    public GameObject[] Text;
    public float FlowerspawnInterval;   // �ͦ���ᶡ�j�ɶ�
    public float MessagespawnInterval;   // �ͦ��T�����j�ɶ�
    public Vector3 positionRange = new Vector3(10, 5, 10); // �H����m�d�� (X, Y, Z)
    public Vector2 FlowersizeRange = new Vector2(0.5f, 2f);      // �H���j�p�d�� (�̤p, �̤j)
    public Vector2 MessagesizeRange = new Vector2(0.1f, 1f);
    private float FlowerlastSpawnTime = 0f;  // �W�@���ͦ����󪺮ɶ�
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
        currentTime = audioSource.time;  // ���o���ַ�e����ɶ�

        // �ھڮɶ�������ĪG
        if (currentTime >= 106f  && currentTime <= 115f)
        {
            // �ˬd�O�_��F�ͦ����󪺮ɶ�
            if (currentTime - FlowerlastSpawnTime >= FlowerspawnInterval)
            {
                FlowerRandomObject();
                FlowerlastSpawnTime = currentTime; // ��s�̫�@���ͦ��ɶ�
            }



        }
        if (currentTime >= 115f)
        {
            foreach (GameObject flower in activeFlowers)
            {
                Destroy(flower); // �R������
            }
            activeFlowers.Clear(); // �M�Űl�ܦC��
        }



        if (currentTime >= 107f && currentTime <= 110f)
        {
            Text[0].SetActive(true);

        }

        // �ھڮɶ�����T���ĪG
       // if (currentTime >= 114f && currentTime <= 117f)
       // {
           // Debug.Log($"88");
            // �ˬd�O�_��F�ͦ����󪺮ɶ�
           // if (currentTime - MessagelastSpawnTime >= MessagespawnInterval)
            //{
               // Debug.Log($"96");
                //MessageRandomObject();
               // MessagelastSpawnTime = currentTime; // ��s�̫�@���ͦ��ɶ�
           // }

           // Text[2].SetActive(true);
           // Text[1].SetActive(false);

       // }

        // �ھڮɶ�����TD��ϥX�{�ɶ�
        if (currentTime >= 150f && currentTime <= 164f)
        {
           TD.SetActive (true);

        }
        else
        {
           TD.SetActive (false);
        }

        //�ھڮɶ�����ۮإX�{�ɶ�
        if (currentTime >= 101f && currentTime <= 180f)
        {
            SecondVideo.Play();
            

        }
        else
        {
            SecondVideo.Stop();

        }
       
      
        //�T�{�@�U
        if (currentTime >= 121f && currentTime <= 126f)
        {
            
            Text[3].SetActive(true);
            Text[2].SetActive(false);

        }
        

        //02:06��1�i�H���K��pose��
        if (currentTime >= 126f && currentTime <= 130f)
        {
            
            Text[4].SetActive(true);
            Text[3].SetActive(false);

        }

        //02:06��1�z�o�Ӥ����I
        if (currentTime >= 131f && currentTime <= 133f)
        {
            
            Text[5].SetActive(true);
            Text[4].SetActive(false);

        }


        //02:13��2�A��@�i�n���n��o����t�@��pose�n�F
        if (currentTime >= 134f && currentTime <= 137f)
        {
           
            Text[6].SetActive(true);
            Text[5].SetActive(false);

        }

        //02:13�o�˫��g�I
        if (currentTime >= 138f && currentTime <= 140f)
        {

            Text[7].SetActive(true);
            Text[6].SetActive(false);

        }

        //02:20��3 ��ı�o�ڭ̥i�H�A�դ@�ӥi�R�@�I���� �Q����i�H����b�Y�W��ŤM����_�_��
        if (currentTime >= 141f && currentTime <= 144f)
        {

            Text[8].SetActive(true);
            Text[7].SetActive(false);

        }

        //02:20��3 �z�W�i�R���I
        if (currentTime >= 145f && currentTime <= 147f)
        {

            Text[9].SetActive(true);
            Text[8].SetActive(false);

        }

        //02:27��4|�藍�_�ڷQ�A�ոլݤ@�өʷP�@�I��
        if (currentTime >= 148f && currentTime <= 150f)
        {

            Text[10].SetActive(true);
            Text[9].SetActive(false);

        }
        else
        {
            Text[10].SetActive(false);
        }



        // �ھڮɶ�����bloom�X�{�ɶ�
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

        // �ھڮɶ�����dot�X�{�ɶ�
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
        // �H���ͦ���m
        Vector3 randomPosition = new Vector3(
            Random.Range(-positionRange.x, positionRange.x),
            Random.Range(-positionRange.y, positionRange.y),
            Random.Range(-positionRange.z, positionRange.z)
        );

        // �ͦ�����
        if (FlowerCurrentCount < FlowerMaxCount)
        {
            int randomIndex = Random.Range(0, flowerprefabs.Length);
            GameObject newObject = Instantiate(flowerprefabs[randomIndex], randomPosition, Quaternion.identity);
            FlowerCurrentCount++;

            // �H���]�w�j�p
            float randomScale = Random.Range(FlowersizeRange.x, FlowersizeRange.y);
            newObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            // �H���]�w�ưʤ�V
            FlowerControl flowerControl = newObject.GetComponent<FlowerControl>();
            if (flowerControl != null)
            {
                // �H�����t�ưʤ�V (-1 �� 1)
                flowerControl.AllowedSwipeDirection = Random.Range(0, 2) == 0 ? -1 : 1;

                flowerControl.maxMovementRange = Random.Range(6f, 8f); // �H���d�� 10 ~ 15

                // ���U���պ�ť��
                CubeGestureListener.RegisterFlower(flowerControl);
            }
            activeFlowers.Add(newObject);

        }



    }


    void MessageRandomObject()
    {
        Debug.Log($"++");
        // �H���ͦ���m
        Vector3 randomPosition = new Vector3(
            Random.Range(-positionRange.x, positionRange.x),
            Random.Range(-positionRange.y, positionRange.y),
            Random.Range(-positionRange.z, positionRange.z)
        );

        // �ͦ�����
        if (MessageCurrentCount < MessageMaxCount)
        {
            int randomIndex = Random.Range(0, messageprefab.Length);
            GameObject newObject = Instantiate(messageprefab[randomIndex], randomPosition, Quaternion.identity);
            MessageCurrentCount++;

            // �H���]�w�j�p
            float randomScale = Random.Range(MessagesizeRange.x, MessagesizeRange.y);
            newObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        }



    }
}
