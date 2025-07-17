using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualCursor : MonoBehaviour
{
    //UI Raycast
    [SerializeField]  GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = this.transform.localPosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count > 0)
        {
            Debug.Log("Hit " + results[0].gameObject.name);
            if (results[0].gameObject.TryGetComponent<ButtonInteraction>(out var button)) button.Interact();            
        }
    }
}
