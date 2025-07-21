using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;

public static class PhotoHosting
{
    public static IEnumerator Upload(Action<PhotoHostingResponse> onPayloadSuccess, Action onPaylaodFailed, string filename, byte[] fileBinary, DateTime expires, int maxDownloads = 1, bool autoDelete = true) {
        WWWForm form = new();

        // form.AddBinaryData("file", fileBinary);
        form.AddBinaryData("file", fileBinary);
        // form.AddField("expires", expires.ToString("yyyy-MM-ddThh:mm:ss.000Z"));
        // form.AddField("id", "2ee47cd8-8c47-4779-bd34-4718443195d8");
        // form.AddField("key", "7FUPNX3.YJKAA4M-GC8M2G6-KZDYRFS-AK7PM7J");
        // form.AddField("maxDownloads", maxDownloads);
        // form.AddField("autoDelete", autoDelete.ToString());

        Debug.Log($"Trying to upload {form} with filename {filename}");
        using(var unityWebRequest = UnityWebRequest.Post("https://tmpfiles.org/api/v1/upload", form)) {
            // unityWebRequest.SetRequestHeader("Authorization", "Token 555myToken555");

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result != UnityWebRequest.Result.Success) 
            {
                onPaylaodFailed?.Invoke();
                Debug.Log($"Failed to upload : {unityWebRequest.result} - {unityWebRequest.error}");
            }
            else 
            {
                var json = unityWebRequest.downloadHandler.text;
                Debug.Log($"Finished Uploading. {json}");
                var response = JsonConvert.DeserializeObject<PhotoHostingResponse>(json);

                onPayloadSuccess?.Invoke(response);
                Debug.Log($"Finished Uploading. {response}");
            }
        }
        
    }
}
