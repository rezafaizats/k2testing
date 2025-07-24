using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScreenshotManager : MonoBehaviour
{
    [SerializeField] private RawImage photoResultRaw;
    [SerializeField] private RawImage qrResultRaw;
    [SerializeField] private float screenshotTimer = 3f;

    [SerializeField] private UnityEvent onScreenshotStart;
    [SerializeField] private UnityEvent<string> onScreenshotTimerUpdate;
    [SerializeField] private UnityEvent onScreenshotTimerDone;
    [SerializeField] private UnityEvent onScreenshotDone;


    [SerializeField] private UnityEvent onUploadStart;
    [SerializeField] private UnityEvent onUploadSuccess;
    [SerializeField] private UnityEvent onUploadFailed;

    private bool isScreenshotStarted = false;
    private string fileScreenshotResultPath;
    private string fileScreenshotName;
    private float currentScreenshotTimer = 3f;
    private float screenshotProcessingBuffer = 0.5f;
    private bool isUploading = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isScreenshotStarted) return;
        currentScreenshotTimer -= Time.deltaTime;
        onScreenshotTimerUpdate?.Invoke(currentScreenshotTimer.ToString("F0"));
    }

    public void StartScreenshot()
    {
        onScreenshotStart?.Invoke();
        StartCoroutine(DelayScreenshot());
    }

    private IEnumerator DelayScreenshot()
    {
        Debug.Log(currentScreenshotTimer);
        isScreenshotStarted = true;

        yield return new WaitForSeconds(screenshotTimer);

        isScreenshotStarted = false;

        currentScreenshotTimer = screenshotTimer;
        onScreenshotTimerDone?.Invoke();

        var filePath = Directory.GetCurrentDirectory();
        fileScreenshotName = "ScreenCapture_" + System.DateTime.UtcNow.ToString("ydd-MM-yyyy-HH-mm-ss") + ".png";
        fileScreenshotResultPath = Screenshotutils.SaveScreenshotDisplay(filePath, fileScreenshotName, (error) => Debug.LogError($"Screenshot failed : {error}!"));

        //Delay for processing buffer
        yield return new WaitForSeconds(screenshotProcessingBuffer);
        photoResultRaw.texture = Screenshotutils.GetScreenshotDisplay(fileScreenshotResultPath, photoResultRaw, (error) => Debug.LogError($"Screenshot failed : {error}!"));
        onScreenshotDone?.Invoke();
    }

    public void UploadScreenshot()
    {
        if (!File.Exists(fileScreenshotResultPath) || isUploading)
        {
            Debug.LogWarning($"Either file doesnt exist at : {fileScreenshotResultPath} or it's still uploading. Uploading status {isUploading}");
            return;
        }

        isUploading = true;
        onUploadStart?.Invoke();

        var screenshotResultyByte = File.ReadAllBytes(fileScreenshotResultPath);
        if (screenshotResultyByte == null) Debug.LogError("No screenshot is grabbed.");
        StartCoroutine(PhotoHosting.Upload(OnUploadSuccess, OnUploadFailed, fileScreenshotName, screenshotResultyByte));
    }


    private void OnUploadFailed()
    {
        Debug.Log("Failed callback");
        // cameraUI.SetCanvasInteractable(true);
        // uploadingText.text = "Foto gagal diunggah.";

        isUploading = false;
        onUploadFailed?.Invoke();
    }

    private void OnUploadSuccess(PhotoHostingResponse response)
    {
        Debug.Log($"Success callback {response.files[0].url}");

        var successText = "Foto berhasil diunggah!";
        isUploading = false;
        Vector2Int qrResultSize = new Vector2Int(256, 256);
        var qrResultTexture = Screenshotutils.EncodeToQR(response.files[0].url, qrResultSize.x, qrResultSize.y, (error) => Debug.LogError($"Encoding failed! {error}"));
        qrResultRaw.texture = qrResultTexture;
        onUploadSuccess?.Invoke();
    }

}
