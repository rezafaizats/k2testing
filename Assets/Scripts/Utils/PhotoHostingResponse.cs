using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PhotoHostingResponse
{
    public bool success;
    public Files[] files;
}

[Serializable]
public struct Files
{
    public string hash;
    public string filename;
    public string url;
    public int size;
    public bool dupe;
}