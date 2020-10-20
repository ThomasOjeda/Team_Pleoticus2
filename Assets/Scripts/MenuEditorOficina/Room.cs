using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room")]
public class Room : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite roomPreview;
    public Sprite roomImage;

    public int tilesWidth;
    public int tilesHeight;
}
