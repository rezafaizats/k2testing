﻿using UnityEngine;
using System.Collections;
using com.rfilkov.kinect;
using System;

namespace com.rfilkov.components
{
    /// <summary>
    /// BackgroundDepthCamColorImage is component that displays the depth camera aligned color image on RawImage texture, usually the scene background.
    /// </summary>
    public class BackgroundDepthCamColorImage : MonoBehaviour
    {
        [Tooltip("Depth sensor index - 0 is the 1st one, 1 - the 2nd one, etc.")]
        public int sensorIndex = 0;

        [Tooltip("RawImage used to display the color camera feed.")]
        public UnityEngine.UI.RawImage backgroundImage;

        [Tooltip("RenderTexture to render the image.")]
        public RenderTexture backgroundTexture;

        [Tooltip("Camera used to display the background image. Set it, if you'd like to allow background image to resize, to match the color image's aspect ratio.")]
        public Camera backgroundCamera;

        [Tooltip("Opaqueness factor of the raw-image.")]
        [Range(0f, 1f)]
        public float opaqueness = 1.0f;


        // last camera rect width & height
        private float lastCamRectW = 0;
        private float lastCamRectH = 0;

        // reference to the kinectManager
        private KinectManager kinectManager = null;
        private KinectInterop.SensorData sensorData = null;
        private Vector2 initialAnchorPos = Vector2.zero;

        //// depth-camera aligned frames
        //private ulong lastDepthCamColorFrameTime = 0;


        void Start()
        {
            if (backgroundImage == null)
            {
                backgroundImage = GetComponent<UnityEngine.UI.RawImage>();
            }

            kinectManager = KinectManager.Instance;
            sensorData = kinectManager != null ? kinectManager.GetSensorData(sensorIndex) : null;

            if(sensorData != null)
            {
                // enable depth camera aligned color frames 
                sensorData.sensorInterface.EnableDepthCameraColorFrame(sensorData, true);
            }
        }


        void OnDestroy()
        {
            if (sensorData != null)
            {
                // disable depth camera aligned color frames 
                sensorData.sensorInterface.EnableDepthCameraColorFrame(sensorData, false);
            }
        }


        void Update()
        {
            if (kinectManager && kinectManager.IsInitialized())
            {
                float cameraWidth = backgroundCamera ? backgroundCamera.pixelRect.width : 0f;
                float cameraHeight = backgroundCamera ? backgroundCamera.pixelRect.height : 0f;

                if(sensorData.depthCamColorImageTexture == null)
                {
                    // enable depth camera aligned color frames 
                    sensorData = kinectManager.GetSensorData(sensorIndex);  // sensor data may be re-created after sensor-int restart
                    sensorData.sensorInterface.EnableDepthCameraColorFrame(sensorData, true);
                }

                if (backgroundImage && sensorData.depthCamColorImageTexture != null && (backgroundImage.texture == null ||
                    backgroundImage.texture.width != sensorData.depthCamColorImageTexture.width || backgroundImage.texture.height != sensorData.depthCamColorImageTexture.height ||
                    lastCamRectW != cameraWidth || lastCamRectH != cameraHeight))
                {
                    lastCamRectW = cameraWidth;
                    lastCamRectH = cameraHeight;

                    backgroundImage.texture = sensorData.depthCamColorImageTexture;
                    backgroundImage.rectTransform.localScale = sensorData.depthImageScale;
                    backgroundImage.color = new Color(1f, 1f, 1f, opaqueness);  // Color.white;

                    if (backgroundCamera != null)
                    {
                        // adjust image's size and position to match the stream aspect ratio
                        int colorImageWidth = sensorData.depthImageWidth;
                        int colorImageHeight = sensorData.depthImageHeight;
                        if (colorImageWidth == 0 || colorImageHeight == 0)
                            return;

                        RectTransform rectImage = backgroundImage.rectTransform;
                        float rectWidth = (rectImage.anchorMin.x != rectImage.anchorMax.x) ? cameraWidth * (rectImage.anchorMax.x - rectImage.anchorMin.x) : rectImage.sizeDelta.x;
                        float rectHeight = (rectImage.anchorMin.y != rectImage.anchorMax.y) ? cameraHeight * (rectImage.anchorMax.y - rectImage.anchorMin.y) : rectImage.sizeDelta.y;

                        if (colorImageWidth > colorImageHeight)
                            rectWidth = rectHeight * colorImageWidth / colorImageHeight;
                        else
                            rectHeight = rectWidth * colorImageHeight / colorImageWidth;

                        Vector2 pivotOffset = (rectImage.pivot - new Vector2(0.5f, 0.5f)) * 2f;
                        Vector2 imageScale = sensorData.depthImageScale;
                        Vector2 anchorPos = rectImage.anchoredPosition + pivotOffset * imageScale * new Vector2(rectWidth, rectHeight);

                        if (rectImage.anchorMin.x != rectImage.anchorMax.x)
                        {
                            rectWidth = -(cameraWidth - rectWidth);
                        }

                        if (rectImage.anchorMin.y != rectImage.anchorMax.y)
                        {
                            rectHeight = -(cameraHeight - rectHeight);
                        }

                        rectImage.sizeDelta = new Vector2(rectWidth, rectHeight);
                        rectImage.anchoredPosition = initialAnchorPos = anchorPos;
                    }
                }

                if (sensorData.depthCamColorImageTexture != null && backgroundTexture != null)
                {
                    Vector2 imageScale = sensorData.colorImageScale;
                    KinectInterop.TransformTexture(sensorData.depthCamColorImageTexture, backgroundTexture, 0, imageScale.x < 0f, imageScale.y < 0f, backgroundCamera == null);
                }
            }
            else
            {
                // reset the background texture, if needed
                if (backgroundImage && backgroundImage.texture != null)
                {
                    backgroundImage.texture = null;

                    if (sensorData != null)
                    {
                        // disable depth camera aligned color frames 
                        sensorData.sensorInterface.EnableDepthCameraColorFrame(sensorData, false);
                    }
                }
            }

            //RectTransform rectTransform = backgroundImage.rectTransform;
            //Debug.Log("pivot: " + rectTransform.pivot + ", anchorPos: " + rectTransform.anchoredPosition + ", \nanchorMin: " + rectTransform.anchorMin + ", anchorMax: " + rectTransform.anchorMax);
        }

    }
}

