using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickOpener : MonoBehaviour
{
    public GameObject toopen;

    public void openPopUp()
    {
        toopen.SetActive(true);
    }
}
