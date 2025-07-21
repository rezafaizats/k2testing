using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PhotoHostingResponse
{
    public string status;
    public Data data;
}

[Serializable]
public struct Data
{
    public string url;
}