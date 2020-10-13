using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    private GameObject defaultIcon, interactIcon;

    void Start()
    {
        defaultIcon = GameObject.Find("Reticle");
        interactIcon = GameObject.Find("PointingHand");
        interactIcon.SetActive(false);
    }

    void Update()
    {
        
    }

    public void ShowIcon(bool isInteractIcon)
    {
        defaultIcon.SetActive(!isInteractIcon);
        interactIcon.SetActive(isInteractIcon);
    }
}
