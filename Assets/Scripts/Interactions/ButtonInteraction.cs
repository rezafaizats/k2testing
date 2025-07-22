using System.Collections;
using System.Collections.Generic;
using com.rfilkov.components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    [SerializeField] private UnityEvent OnButtonVirtualClick;
    [SerializeField] private Button buttonUI;

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

    public bool IsInteractable() => buttonUI.interactable;

}
