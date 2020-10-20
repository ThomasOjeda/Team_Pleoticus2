using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomDisplayer : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Room room;
    [SerializeField] private Transform parent;

    public GameObject spawnedRoom;

    private void Start()
    {
        Image roomImage = GetComponent<Image>();
        roomImage.sprite = room.roomPreview;
        // Se debería agregar un pasillo de inicio
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject roomObject = new GameObject("Room Display");
        Image roomImage = roomObject.AddComponent<Image>();
        roomImage.sprite = room.roomImage;
        float tileSize = parent.GetComponentInParent<Map>().TileSize;
        float canvasScale = GetComponentInParent<Canvas>().transform.localScale.x;
        ((RectTransform)roomImage.transform).sizeDelta = new Vector2(tileSize * room.tilesWidth * canvasScale, tileSize * room.tilesHeight * canvasScale);
        RoomDisplay roomDisplay = roomObject.AddComponent<RoomDisplay>();
        roomDisplay.transform.SetParent(parent);
        roomObject.AddComponent<Button>();
        spawnedRoom = (GameObject)Instantiate(roomObject, transform.position, Quaternion.identity, parent);
        Destroy(roomObject);

        Debug.Log("scale: " + parent.parent.parent.parent.localScale);

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 cursorPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        spawnedRoom.transform.position = eventData.position;

    }

    public void OnPointerUp(PointerEventData eventData)
    { 
        Vector3 mousePosition = eventData.position;
        spawnedRoom.GetComponent<RoomDisplay>().centerRoomOnTile(mousePosition);

        // Faltan realizar chequeos de puertas y habitaciones contiguas
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && spawnedRoom != null)
        {
            Debug.Log("rotate");
            spawnedRoom.transform.Rotate(0f, 0, 90f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.ShowTooltip("<b>   "+room.name+"</b>\n" +"<i>"+room.description+"</i>");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip();
    }

}
