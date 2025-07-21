using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Outfit UI
    [SerializeField] private TextMeshProUGUI outfitNameText;

    //Photo UI
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI uploadErrorText;

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
}
