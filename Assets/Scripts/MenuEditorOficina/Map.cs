using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private Transform parent;

    public float TileSize
    {
        get { return ((RectTransform)tile.transform).sizeDelta.x; }
    }

    public Dictionary<Point, GameObject> Tiles { get; set; }

    void Start()
    {
        DrawTilesMap();
    }

    void Update()
    {
        // Falta actualizar cantidad de tiles según zoom.
        if(Input.mousePresent)
        {
            float scroll =Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0)
            {
                Debug.Log("zoom in");
            }

            else if (scroll < 0)
            {
                Debug.Log("zoom out");
            }
        }
    }

    private void DrawTilesMap()
    {
        Tiles = new Dictionary<Point, GameObject>();

        int yTiles = (int)(((RectTransform)parent).sizeDelta.y / TileSize);
        int xTiles = (int)(((RectTransform)parent).sizeDelta.x / TileSize);

        for (int y = 0; y < yTiles; y++)
        {
            for(int x = 0; x < xTiles; x++)
            {
                GameObject newTile = Instantiate(tile, parent);
                newTile.transform.SetAsFirstSibling();
                newTile.transform.localPosition = new Vector3(x * TileSize, -y * TileSize, 0);

                Tiles.Add(new Point(x, y), newTile);
            }
        }
    }

    public void CenterRoomOnTile(GameObject room, Vector3 position)
    {
        Vector3 localPosition = transform.InverseTransformPoint(position);
        int x = (int)(localPosition.x/TileSize);
        int y = (int)(-1*localPosition.y/TileSize);
        
        int xTiles = (int)(((RectTransform)parent).sizeDelta.x / TileSize);
        int yTiles = (int)(((RectTransform)parent).sizeDelta.y / TileSize);
        if (x < 0) x = 0;
        if (x > xTiles) x = xTiles;
        if (y < 0) y = 0;
        if (y > yTiles) y = yTiles;

        Point key = new Point(x, y);
        room.transform.position = (Tiles[key]).transform.position;
    }
    
}
