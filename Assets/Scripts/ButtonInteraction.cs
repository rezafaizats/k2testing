using System.Collections;
using System.Collections.Generic;
using com.rfilkov.components;
using UnityEngine;
using UnityEngine.Events;

public class ButtonInteraction : MonoBehaviour, InteractionListenerInterface
{
    public UnityEvent OnButtonVirtualClick;
    public InteractionManager interactionManager;

    private Vector3 screenNormalPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        Debug.Log($"{gameObject.name} is invoked");
        OnButtonVirtualClick?.Invoke();
    }

    public void HandGripDetected(ulong userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
    {
		if (!isHandInteracting || !interactionManager)
			return;
		if (userId != interactionManager.GetUserID())
			return;

		// lastHandEvent = InteractionManager.HandEventType.Grip;
		//isLeftHandDrag = !isRightHand;
		screenNormalPos = handScreenPos;
    }

    public void HandReleaseDetected(ulong userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
    {
		if (!isHandInteracting || !interactionManager)
			return;
		if (userId != interactionManager.GetUserID())
			return;

		// lastHandEvent = InteractionManager.HandEventType.Release;
		//isLeftHandDrag = !isRightHand;
		screenNormalPos = handScreenPos;
    }

    public bool HandClickDetected(ulong userId, int userIndex, bool isRightHand, Vector3 handScreenPos)
    {
        Debug.Log($"{gameObject.name} is clicked");
        OnButtonVirtualClick?.Invoke();
        return true;
    }
}
