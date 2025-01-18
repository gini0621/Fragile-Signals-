using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WebCamPhotoManager : MonoBehaviour
{
    public RawImage webcamDisplay;       // 顯示攝像頭畫面的 UI 元素
    public RawImage displayPhoto;       // 用於顯示照片的 UI 元素
    public AudioSource audioSource;     // 音樂來源
    public string photoDirectory;       // 照片保存的目錄

    private WebCamTexture webcamTexture;  // 攝像頭畫面
    private int photoCount = 1;           // 照片計數器
    public int WebSelect;                 // 選擇攝像頭索引
    private string photo1Path;            // 第一張照片路徑
    private string photo2Path;            // 第二張照片路徑

    void Start()
    {
        // 初始化照片保存目錄
        if (string.IsNullOrEmpty(photoDirectory))
        {
            photoDirectory = Path.Combine(Application.persistentDataPath, "SavedPhotos");
        }

        // 確保目錄存在
        if (!Directory.Exists(photoDirectory))
        {
            Directory.CreateDirectory(photoDirectory);
        }
        Debug.Log("照片保存目錄: " + photoDirectory);

        WebCamDevice[] devices = WebCamTexture.devices; // 取得所有可用的攝影機裝置

        if (devices.Length > 0)
        {
            Debug.Log("找到攝像頭：" + devices[WebSelect].name);
            webcamTexture = new WebCamTexture(devices[WebSelect].name); // 選擇攝像頭
            webcamDisplay.texture = webcamTexture;             // 設置到 RawImage 上
            webcamTexture.Play();                              // 開始播放
        }
        else
        {
            Debug.LogError("未找到可用的攝像頭！");
        }

        // 確保顯示區域為空
        displayPhoto.texture = null;
        displayPhoto.gameObject.SetActive(false);
    }

    void Update()
    {
        float currentTime = audioSource.time; // 獲取音樂當前播放時間

        if (currentTime >= 101f && currentTime < 145f)
        {
            webcamDisplay.gameObject.SetActive(true);
            if (webcamTexture != null && webcamTexture.isPlaying) // 確保攝影機正在運行，更新 UI
            {
                webcamDisplay.texture = webcamTexture;
                webcamDisplay.material.mainTexture = webcamTexture;
            }
        }
        else
        {
            webcamDisplay.gameObject.SetActive(false);
        }



        // 在 117 秒時拍第一張照片
        if (currentTime >= 117f && currentTime < 118f && string.IsNullOrEmpty(photo1Path))
        {
            photo1Path = CapturePhoto();
            Debug.Log("第一張照片已拍攝: " + photo1Path);
        }

        // 在 119-125 秒顯示第一張照片
        if (currentTime >= 119f && currentTime <= 124.3f)
        {
            displayPhoto.gameObject.SetActive(true);
            ShowPhoto(photo1Path);
        }
        else if (currentTime > 124.3f)
        {
            displayPhoto.gameObject.SetActive(false);
        }

        // 在 130 秒時拍第二張照片
        if (currentTime >= 130f && currentTime < 131f && string.IsNullOrEmpty(photo2Path))
        {
            photo2Path = CapturePhoto();
            Debug.Log("第二張照片已拍攝: " + photo2Path);
        }

        // 在 132-140 秒顯示第二張照片
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
            Texture2D photo = new Texture2D(webcamTexture.width, webcamTexture.height); // 將攝影機畫面轉換為 Texture2D
            photo.SetPixels(webcamTexture.GetPixels());
            photo.Apply();

            // 保存路徑
            string photoPath = Path.Combine(photoDirectory, $"Photo_{photoCount}.jpg");
            photoCount++;

            try
            {
                // 保存照片，編碼圖片為 JPG 並寫入檔案
                byte[] photoBytes = photo.EncodeToJPG();
                File.WriteAllBytes(photoPath, photoBytes);
                Debug.Log("照片已成功保存！路徑：" + photoPath);
                return photoPath;
            }
            catch (System.Exception ex)
            {
                Debug.LogError("保存失敗: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("攝影機不可用！");
        }

        return null;
    }

    void ShowPhoto(string photoPath)
    {
        if (!string.IsNullOrEmpty(photoPath) && File.Exists(photoPath))
        {
            Debug.Log("顯示照片: " + photoPath);
            displayPhoto.gameObject.SetActive(true);
            displayPhoto.texture = LoadPhoto(photoPath);
        }
        else
        {
            displayPhoto.gameObject.SetActive(false);
        }
    }

    Texture2D LoadPhoto(string path) // 從指定路徑載入圖片並返回 Texture2D
    {
        byte[] bytes = File.ReadAllBytes(path);
        Texture2D photo = new Texture2D(1, 1);
        photo.LoadImage(bytes);
        return photo;
    }
}
