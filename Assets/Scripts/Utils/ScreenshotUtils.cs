using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

public static class Screenshotutils
{
    const string folderName = "/Assets/Screenshots/";

    public static string SaveScreenshotDisplay(string filePath, string fileName, UnityAction<string> onScreenshotFailed)
    {
        filePath = Path.Join(filePath, folderName);
        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        filePath = Path.Join(filePath, fileName);
        ScreenCapture.CaptureScreenshot(filePath);
        Debug.Log($"Screenshot captured at : {filePath}");
        return filePath;

    }

    public static Texture2D GetScreenshotDisplay(string filePath, RawImage photoResultRaw, UnityAction<string> onScreenshotFailed)
    {        
        Texture2D photoTexture = new Texture2D(photoResultRaw.texture.width, photoResultRaw.texture.height);

        byte[] photoTextureByte = File.ReadAllBytes(filePath);
        if (photoTextureByte == null) onScreenshotFailed?.Invoke($"Photo screenshot failed to read bytes on {filePath}");
        photoTexture.LoadImage(photoTextureByte);
        return photoTexture;
    }

    public static Texture2D EncodeToQR(string textToEncode, int width, int height, UnityAction<string> onEncodeFailed)
    {
        QRCodeWriter writer = new();

        var result = writer.encode(textToEncode, BarcodeFormat.QR_CODE, width, height);
        if (result == null)
        {
            onEncodeFailed?.Invoke($"Encode failed, QR failed to generate!");
            return null;
        }
        Texture2D qrCodeTexture = new Texture2D(result.Width, result.Height);
        for (int x = 0; x < result.Width; x++)
        {
            for (int y = 0; y < result.Height; y++)
            {
                qrCodeTexture.SetPixel(x, y, result[x, y] ? Color.black : Color.white);
            }
        }
        qrCodeTexture.Apply();
        if (qrCodeTexture == null)
        {
            onEncodeFailed?.Invoke($"Encode failed, Texture failed to be applied!");
            return null;
        }
        return qrCodeTexture;
    }

}
