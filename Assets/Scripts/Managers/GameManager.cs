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

    private bool isUserEmpty = true;
    private float currentUserEmptyTimer = 15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (kinectManager == null) return;

        if (kinectManager.GetUsersCount() > 0) isUserEmpty = false;

        if (!isUserEmpty) return;

        if (kinectManager.GetUsersCount() == 0) {
            currentUserEmptyTimer -= Time.deltaTime;
            if (currentUserEmptyTimer <= 0f)
            {
                onUserEmpty?.Invoke();
                isUserEmpty = true;
                currentUserEmptyTimer = userEmptyTimer;
            }
        }
    }

}
