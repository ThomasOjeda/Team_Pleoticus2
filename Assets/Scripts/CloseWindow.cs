using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    public GameObject toclose;
    // Start is called before the first frame update
    public void closeWindow()
    {
        toclose.SetActive(false);
    }

}
