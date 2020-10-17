using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickToggle : MonoBehaviour
{
    public GameObject totoggle;

    public void togglePopUp()
    {
        bool isActive = totoggle.activeSelf;
        totoggle.SetActive(!isActive);
    }
}
