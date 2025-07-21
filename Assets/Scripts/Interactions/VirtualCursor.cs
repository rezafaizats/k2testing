using System.Collections;
using System.Collections.Generic;
using com.rfilkov.components;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualCursor : MonoBehaviour, InteractionListenerInterface
{
    public InteractionManager interactionManager;
    public float interactionCooldown = 1f;

    private float currentInteractionCooldown = 0f;
    private Vector3 screenNormalPos = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(inputAxis);

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = this.transform.position;

        // Perform the raycast
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        if (results.Count > 0 && currentInteractionCooldown <= 0f)
        {
            // Debug.Log($"{results[0]}");
            if (results[0].gameObject.TryGetComponent<ButtonInteraction>(out var vButton))
            {
                Debug.Log("Button interact!");
                vButton.Interact();
                currentInteractionCooldown = interactionCooldown;
            }
        }

        if (currentInteractionCooldown > 0f) currentInteractionCooldown -= Time.deltaTime; 
    }
    
    public void HandGripDetected(ulong userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
    {
		if (!isHandInteracting || !interactionManager)
			return;
		if (userId != interactionManager.GetUserID())
			return;

		//isLeftHandDrag = !isRightHand;
		screenNormalPos = handScreenPos;
    }

    public void HandReleaseDetected(ulong userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
    {
		if (!isHandInteracting || !interactionManager)
			return;
		if (userId != interactionManager.GetUserID())
			return;

		//isLeftHandDrag = !isRightHand;
		screenNormalPos = handScreenPos;
    }

    public bool HandClickDetected(ulong userId, int userIndex, bool isRightHand, Vector3 handScreenPos)
    {
        return true;
    }
}
