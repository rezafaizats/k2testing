using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;

public static class PhotoHosting
{
    public static IEnumerator Upload(Action<PhotoHostingResponse> onPayloadSuccess, Action onPaylaodFailed, string filename, byte[] fileBinary) {
        WWWForm form = new();

        // form.AddBinaryData("file", fileBinary);
        form.AddBinaryData("files[]", fileBinary);
        
        Debug.Log($"Trying to upload {form.headers} with filename {filename}");
        using (var unityWebRequest = UnityWebRequest.Post("https://uguu.se/upload", form))
        {
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
