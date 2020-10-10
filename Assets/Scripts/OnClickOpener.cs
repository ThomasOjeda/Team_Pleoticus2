using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickOpener : MonoBehaviour
{
    public GameObject toopen;

    public void openPopUp()
    {
        /*bool isActive = toopen.activeSelf;
        toopen.SetActive(!isActive);*/
        toopen.SetActive(true);
    }
}
