using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomDisplay : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        (transform.parent.GetComponentInParent<Map>()).SetTilesUnoccupied(gameObject, Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("dragging");
        transform.position = eventData.position;
    }

    public void centerRoomOnTile(Vector3 position)
    {
        (transform.parent.GetComponentInParent<Map>()).CenterRoomOnTile(gameObject, position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer up");
        Vector3 mousePosition = eventData.position;
        centerRoomOnTile(mousePosition);
    }

    
}
