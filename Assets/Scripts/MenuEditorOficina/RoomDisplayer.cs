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
    private float screenWidth;

    private void Start()
    {
        screenWidth = Screen.width;
        // Se debería agregar un pasillo de inicio
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject roomObject = new GameObject("Room Display");
        Image roomImage = roomObject.AddComponent<Image>();
        roomImage.sprite = room.roomImage;
        float tileSize = parent.GetComponentInParent<Map>().TileSize;
        ((RectTransform)roomImage.transform).sizeDelta = new Vector2(53.14f, 53.14f);
        RoomDisplay roomDisplay = roomObject.AddComponent<RoomDisplay>();
        roomDisplay.transform.SetParent(parent);
        roomObject.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        spawnedRoom = (GameObject)Instantiate(roomObject, transform.position, Quaternion.identity, parent);
        Destroy(roomObject);

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
        // Falta acomodar para que gire correctamente
        if (Input.GetKeyDown(KeyCode.R) && spawnedRoom != null)
        {
            Debug.Log("rotate");
            spawnedRoom.transform.Rotate(0f, 0, 90f);
        }

        // Re-hacer. Se agrega para poder redimensionar las rooms displays
        if(Screen.width != screenWidth)
        {
            float factor = Screen.width / screenWidth;
            float tileSize = parent.GetComponentInParent<Map>().TileSize;
            ((RectTransform)spawnedRoom.transform).sizeDelta = new Vector2((tileSize*factor) * 2, (tileSize*factor) * 2);
            screenWidth = Screen.width;
        }
    }

    // Agregar funcionamiento para mostrar nombre y descripción de cada sala
    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

}
