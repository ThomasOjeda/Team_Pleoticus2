using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private Transform parent;

    [SerializeField] private int rows;
    [SerializeField] private int columns;

    public float TileSize
    {
        get { return ((RectTransform)tile.transform).sizeDelta.x; }
    }

    // Diccionario<CoordenadaXY de Tile, KeyValuePair<Tile, Room>>
    public Dictionary<Point, KeyValuePair<GameObject, GameObject>> Tiles { get; set; }

    void Start()
    {
        DrawTilesMap();
    }

    void Update()
    {
        // Falta actualizar cantidad de tiles según zoom.
        if(Input.mousePresent)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0)
            {
                //transform.localScale = transform.localScale + new Vector3(0.1f, 0.1f);
            }

            else if (scroll < 0)
            {
                //transform.localScale = transform.localScale - new Vector3(0.1f, 0.1f);
            }
        }
    }

    private void DrawTilesMap()
    {
        Tiles = new Dictionary<Point, KeyValuePair<GameObject, GameObject>>();

        int xTiles = (int)(((RectTransform)parent).sizeDelta.x / TileSize);
        int yTiles = (int)(((RectTransform)parent).sizeDelta.y / TileSize);

        for (int y = 0; y < yTiles; y++)
        {
            for(int x = 0; x < xTiles; x++)
            {
                GameObject newTile = Instantiate(tile, parent);
                newTile.transform.SetAsFirstSibling();
                newTile.transform.localPosition = new Vector3(x * TileSize, -y * TileSize, 0);

                Tiles.Add(new Point(x, y), new KeyValuePair<GameObject, GameObject>(newTile, null));
            }
        }
    }

    public List<Point> GetTilesUnoccupied(Point mousePosition, int roomImageWidth, int roomImageHeight)
    {
        List<Point> tiles = new List<Point>();
        for (int j = (int)(mousePosition.Y - roomImageHeight/2); j < (int)(mousePosition.Y + roomImageWidth/2); j++)
        {
            for (int i = (int)(mousePosition.X - roomImageWidth/2); i < (int)(mousePosition.X + roomImageHeight/2); i++)
            {
                if (Tiles[new Point(i, j)].Value == false)
                {
                    Debug.Log(i + ", " + j);
                    tiles.Add(new Point(i, j));
                }
            }
        }

        return tiles;
    }

    public void SetTilesUnoccupied(GameObject room, Vector3 position)
    {
        Vector2 mousePosition = transform.InverseTransformPoint(position);
        Point mousePositionPoint = new Point((int)(mousePosition.x / TileSize), (int)(-1 * mousePosition.y / TileSize));

        float tileWidth = room.GetComponent<Image>().rectTransform.sizeDelta.x / GetComponentInParent<Canvas>().transform.localScale.x;
        float tileHeight = room.GetComponent<Image>().rectTransform.sizeDelta.y / GetComponentInParent<Canvas>().transform.localScale.x;

        int roomImageWidth = (int)(tileWidth / TileSize);
        int roomImageHeight = (int)(tileHeight / TileSize);



        for (int j = (int)(mousePositionPoint.Y - roomImageHeight); j < (int)(mousePositionPoint.Y + roomImageWidth); j++)
        {
            for (int i = (int)(mousePositionPoint.X - roomImageWidth); i < (int)(mousePositionPoint.X + roomImageHeight); i++)
            {
                Point p = new Point(i, j);
                if (Tiles.ContainsKey(p))
                {
                    if (Tiles[p].Value == room)
                    {
                        Tiles[p] = new KeyValuePair<GameObject, GameObject>(Tiles[p].Key, null);
                    }
                }                
            }
        }
    }

    // FALTA ELIMINAR SI PEGA AFUERA DEL MAPA
    public void CenterRoomOnTile(GameObject room, Vector3 position)
    {
        Vector2 mousePosition = transform.InverseTransformPoint(position);
        Point mousePositionPoint = new Point((int)(mousePosition.x / TileSize), (int)(-1 * mousePosition.y / TileSize));

        float tileWidth = room.GetComponent<Image>().rectTransform.sizeDelta.x / GetComponentInParent<Canvas>().transform.localScale.x;
        float tileHeight = room.GetComponent<Image>().rectTransform.sizeDelta.y / GetComponentInParent<Canvas>().transform.localScale.x;

        int roomImageWidth = (int)(tileWidth / TileSize);
        int roomImageHeight = (int)(tileHeight / TileSize);

        Debug.Log(mousePositionPoint.X + " x " + mousePositionPoint.Y);

        if ((mousePositionPoint.X - roomImageWidth / 2) < 0) mousePositionPoint = new Point(mousePositionPoint.X + roomImageWidth / 2, mousePositionPoint.Y);
        if ((mousePositionPoint.Y - roomImageHeight / 2) < 0) mousePositionPoint = new Point(mousePositionPoint.X, mousePositionPoint.Y + roomImageHeight / 2);

        List<Point> newOccupiedTiles = GetTilesUnoccupied(mousePositionPoint, roomImageWidth, roomImageHeight);
        if (newOccupiedTiles.Count != 0)
        {
            if(newOccupiedTiles.Count != roomImageWidth*roomImageHeight)
            {
                Debug.Log("alguna tile está ocupada");
                List<Point> alternatives = new List<Point>();
                Point top = new Point(mousePositionPoint.X, mousePositionPoint.Y - roomImageHeight / 2);
                Point right = new Point(mousePositionPoint.X + roomImageWidth / 2, mousePositionPoint.Y);
                Point bottom = new Point(mousePositionPoint.X, mousePositionPoint.Y + roomImageHeight / 2);
                Point left = new Point(mousePositionPoint.X - roomImageWidth / 2, mousePositionPoint.Y);
                Point topLeft = new Point(mousePositionPoint.X - roomImageWidth / 2, mousePositionPoint.Y - roomImageHeight / 2);
                Point topRight = new Point(mousePositionPoint.X + roomImageWidth / 2, mousePositionPoint.Y - roomImageHeight / 2);
                Point bottomRight = new Point(mousePositionPoint.X + roomImageWidth / 2, mousePositionPoint.Y + roomImageHeight / 2);
                Point bottomLeft = new Point(mousePositionPoint.X - roomImageWidth / 2, mousePositionPoint.Y + roomImageHeight / 2);
                alternatives.Add(top);
                alternatives.Add(right);
                alternatives.Add(bottom);
                alternatives.Add(left);
                alternatives.Add(topLeft);
                alternatives.Add(topRight);
                alternatives.Add(bottomRight);
                alternatives.Add(bottomLeft);

                bool existAnAlternative = false;
                foreach(Point p in alternatives)
                {
                    Point point = p;
                    if ((p.X - roomImageWidth / 2) < 0) point = new Point(p.X + roomImageWidth / 2, p.Y);
                    if ((p.Y - roomImageHeight / 2) < 0) point = new Point(p.X, p.Y + roomImageHeight / 2);

                    newOccupiedTiles = GetTilesUnoccupied(point, roomImageWidth, roomImageHeight);

                    if (newOccupiedTiles.Count == roomImageWidth * roomImageHeight)
                    {
                        existAnAlternative = true;
                        room.transform.position = (Tiles[point]).Key.transform.position;
                        foreach (Point p1 in newOccupiedTiles)
                        {
                            Tiles[p1] = new KeyValuePair<GameObject, GameObject>(Tiles[p1].Key, room);
                        }
                        break;
                    }

                }
                if(!existAnAlternative)
                {
                    Debug.Log("no existe alternativa");
                    Destroy(room);
                }

            }
            else
            {
                room.transform.position = (Tiles[mousePositionPoint]).Key.transform.position;
                foreach (Point p in newOccupiedTiles)
                {
                    Tiles[p] = new KeyValuePair<GameObject, GameObject>(Tiles[p].Key, room);
                }
            }
        }
        else
        {
            Debug.Log("todas las tiles están ocupadas");
            Destroy(room);
        }
        
        //int xTiles = (int)(((RectTransform)parent).sizeDelta.x / TileSize);
        //int yTiles = (int)(((RectTransform)parent).sizeDelta.y / TileSize);
        //if (x < 0) x = 0;
        //if (x > xTiles) x = xTiles;
        //if (y < 0) y = 0;
        //if (y > yTiles) y = yTiles;

        //Point key = new Point(x, y);
        
    }
}
