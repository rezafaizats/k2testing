using System.Collections;
using System.Collections.Generic;
using com.rfilkov.kinect;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private KinectManager kinectManager;

    [SerializeField] private UnityEvent onUserEmpty;
    [SerializeField] private float userEmptyTimer = 15f;

    [SerializeField] private bool isUserEmpty = true;
    [SerializeField] private bool isIdleRevertBack = true;
    [SerializeField] private float currentUserEmptyTimer = 15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (kinectManager == null) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onUserEmpty?.Invoke();
            isUserEmpty = true;
            currentUserEmptyTimer = userEmptyTimer;
        }

        // Debug.Log($"Total user tracked {kinectManager.GetUsersCount()}");

        if (kinectManager.GetUsersCount() > 0) isUserEmpty = false;
        else isUserEmpty = true;
        
        if (kinectManager.GetUsersCount() == 0 && !isUserEmpty)
        {
            currentUserEmptyTimer -= Time.deltaTime;
            if (currentUserEmptyTimer <= 0f)
            {
                onUserEmpty?.Invoke();
                isUserEmpty = true;
                currentUserEmptyTimer = userEmptyTimer;
            }
        }
    }

    public void SetIdleReset(bool status) => isIdleRevertBack = status;

}
