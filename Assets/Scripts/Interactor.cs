using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactiveRange;
    private InteractiveObject interactiveObject;
    private Camera cam;
    private RaycastHit hit;
    private ReticleController reticleController;

    void Start()
    {
        cam = Camera.main;
        reticleController = GameObject.FindObjectOfType<ReticleController>();
    }

    void Update()
    {
        Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactiveRange);
        if (hit.transform)
        {
            interactiveObject = hit.transform.GetComponent<InteractiveObject>();
        }
        else
        {
            interactiveObject = null;
        }

        reticleController.ShowIcon(interactiveObject);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactiveObject)
            {
                interactiveObject.PerformAction();
            }
        }
    }
}
