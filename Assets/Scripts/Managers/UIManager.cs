using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    //Outfit UI
    [SerializeField] private TextMeshProUGUI outfitNameText;

    //Photo UI
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI uploadErrorText;

    //QR UI
    [SerializeField] private TextMeshProUGUI qrTimerText;
    [SerializeField] private float qrTimerDuration = 30f;
    [SerializeField] private UnityEvent onQRTimerDone;

    private bool isQRTimerStarted = false;
    private float currentQRTimer = 0f;

    void Update()
    {
        if (!isQRTimerStarted) return;
        currentQRTimer -= Time.deltaTime;
        qrTimerText.text = currentQRTimer.ToString("F0");

        if (currentQRTimer <= 0f)
        {
            onQRTimerDone?.Invoke();
            currentQRTimer = qrTimerDuration;
            isQRTimerStarted = false;
        }
    }

    public void ChangeOutfitName(string name)
    {
        outfitNameText.text = name;
    }

    public void ActivateCountdown(bool isActive)
    {
        countdownText.gameObject.SetActive(isActive);
    }

    public void ActivateError(bool isActive)
    {
        uploadErrorText.gameObject.SetActive(isActive);
    }

    public void SetErrorLog(string log)
    {
        uploadErrorText.text = log;
    }

    public void DisplayTimer(string timer)
    {
        countdownText.text = timer;
    }

    public void StartQrTimer()
    {
        currentQRTimer = qrTimerDuration;
        isQRTimerStarted = true;
    }
}
