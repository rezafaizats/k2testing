using System.Collections;
using System.Collections.Generic;
using com.rfilkov.components;
using UnityEngine;
using UnityEngine.Events;

public class ButtonInteraction : MonoBehaviour
{
    public UnityEvent OnButtonVirtualClick;

    private Vector3 screenNormalPos = Vector3.zero;
	private InteractionManager.HandEventType lastHandEvent = InteractionManager.HandEventType.None;

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

}
