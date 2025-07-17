using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonInteraction : MonoBehaviour
{
    public UnityEvent OnButtonVirtualClick;
    
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
    }

}
