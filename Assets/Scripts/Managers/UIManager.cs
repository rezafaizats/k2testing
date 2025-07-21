using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Photo UI
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI uploadErrorText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateCountdown(bool isActive)
    {
        countdownText.gameObject.SetActive(isActive);
    }

    public void DisplayTimer(string timer)
    {
        countdownText.text = timer;
    }
}
