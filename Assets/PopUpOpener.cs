using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpOpener : MonoBehaviour
{
    public GameObject popup;

    public void openPopUp()
    {
        if (popup!=null)
        popup.SetActive(true);
    }
}