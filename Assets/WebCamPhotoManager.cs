using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WebCamPhotoManager : MonoBehaviour
{
    public RawImage webcamDisplay;       // ����ṳ�Y�e���� UI ����
    public RawImage displayPhoto;       // �Ω���ܷӤ��� UI ����
    public AudioSource audioSource;     // ���֨ӷ�
    public string photoDirectory;       // �Ӥ��O�s���ؿ�

    private WebCamTexture webcamTexture;  // �ṳ�Y�e��
    private int photoCount = 1;           // �Ӥ��p�ƾ�
    public int WebSelect;                 // ����ṳ�Y����
    private string photo1Path;            // �Ĥ@�i�Ӥ����|
    private string photo2Path;            // �ĤG�i�Ӥ����|

    void Start()
    {
        // ��l�ƷӤ��O�s�ؿ�
        if (string.IsNullOrEmpty(photoDirectory))
        {
            photoDirectory = Path.Combine(Application.persistentDataPath, "SavedPhotos");
        }

        // �T�O�ؿ��s�b
        if (!Directory.Exists(photoDirectory))
        {
            Directory.CreateDirectory(photoDirectory);
        }
        Debug.Log("�Ӥ��O�s�ؿ�: " + photoDirectory);

        WebCamDevice[] devices = WebCamTexture.devices; // ���o�Ҧ��i�Ϊ���v���˸m

        if (devices.Length > 0)
        {
            Debug.Log("����ṳ�Y�G" + devices[WebSelect].name);
            webcamTexture = new WebCamTexture(devices[WebSelect].name); // ����ṳ�Y
            webcamDisplay.texture = webcamTexture;             // �]�m�� RawImage �W
            webcamTexture.Play();                              // �}�l����
        }
        else
        {
            Debug.LogError("�����i�Ϊ��ṳ�Y�I");
        }

        // �T�O��ܰϰ쬰��
        displayPhoto.texture = null;
        displayPhoto.gameObject.SetActive(false);
    }

    void Update()
    {
        float currentTime = audioSource.time; // ������ַ�e����ɶ�

        if (currentTime >= 101f && currentTime < 145f)
        {
            webcamDisplay.gameObject.SetActive(true);
            if (webcamTexture != null && webcamTexture.isPlaying) // �T�O��v�����b�B��A��s UI
            {
                webcamDisplay.texture = webcamTexture;
                webcamDisplay.material.mainTexture = webcamTexture;
            }
        }
        else
        {
            webcamDisplay.gameObject.SetActive(false);
        }



        // �b 117 ��ɩ�Ĥ@�i�Ӥ�
        if (currentTime >= 117f && currentTime < 118f && string.IsNullOrEmpty(photo1Path))
        {
            photo1Path = CapturePhoto();
            Debug.Log("�Ĥ@�i�Ӥ��w����: " + photo1Path);
        }

        // �b 119-125 ����ܲĤ@�i�Ӥ�
        if (currentTime >= 119f && currentTime <= 124.3f)
        {
            displayPhoto.gameObject.SetActive(true);
            ShowPhoto(photo1Path);
        }
        else if (currentTime > 124.3f)
        {
            displayPhoto.gameObject.SetActive(false);
        }

        // �b 130 ��ɩ�ĤG�i�Ӥ�
        if (currentTime >= 130f && currentTime < 131f && string.IsNullOrEmpty(photo2Path))
        {
            photo2Path = CapturePhoto();
            Debug.Log("�ĤG�i�Ӥ��w����: " + photo2Path);
        }

        // �b 132-140 ����ܲĤG�i�Ӥ�
        if (currentTime >= 132f && currentTime <= 139.3f)
        {
            displayPhoto.gameObject.SetActive(true);
            ShowPhoto(photo2Path);
        }
        else if (currentTime > 139.3f)
        {
            displayPhoto.gameObject.SetActive(false);
        }
    }

    string CapturePhoto()
    {
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            Texture2D photo = new Texture2D(webcamTexture.width, webcamTexture.height); // �N��v���e���ഫ�� Texture2D
            photo.SetPixels(webcamTexture.GetPixels());
            photo.Apply();

            // �O�s���|
            string photoPath = Path.Combine(photoDirectory, $"Photo_{photoCount}.jpg");
            photoCount++;

            try
            {
                // �O�s�Ӥ��A�s�X�Ϥ��� JPG �üg�J�ɮ�
                byte[] photoBytes = photo.EncodeToJPG();
                File.WriteAllBytes(photoPath, photoBytes);
                Debug.Log("�Ӥ��w���\�O�s�I���|�G" + photoPath);
                return photoPath;
            }
            catch (System.Exception ex)
            {
                Debug.LogError("�O�s����: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("��v�����i�ΡI");
        }

        return null;
    }

    void ShowPhoto(string photoPath)
    {
        if (!string.IsNullOrEmpty(photoPath) && File.Exists(photoPath))
        {
            Debug.Log("��ܷӤ�: " + photoPath);
            displayPhoto.gameObject.SetActive(true);
            displayPhoto.texture = LoadPhoto(photoPath);
        }
        else
        {
            displayPhoto.gameObject.SetActive(false);
        }
    }

    Texture2D LoadPhoto(string path) // �q���w���|���J�Ϥ��ê�^ Texture2D
    {
        byte[] bytes = File.ReadAllBytes(path);
        Texture2D photo = new Texture2D(1, 1);
        photo.LoadImage(bytes);
        return photo;
    }
}
