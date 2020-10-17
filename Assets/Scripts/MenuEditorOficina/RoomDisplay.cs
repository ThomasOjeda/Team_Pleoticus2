using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomDisplay : MonoBehaviour, IBeginDragHandler
{
    // Falta movimiento una vez que se encuentra en la grilla
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("begin drag");
        transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin drag");
        transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("dragging");
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void centerRoomOnTile(Vector3 position)
    {
        (transform.parent.GetComponentInParent<Map>()).CenterRoomOnTile(gameObject, Input.mousePosition);
    }
}
