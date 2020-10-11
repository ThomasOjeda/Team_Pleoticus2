using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private Vector3 openPosition, closedPosition;
    [SerializeField] private float animationTime;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private MovementType movementType;
    private Hashtable iTweenArgs;
    private enum MovementType { Slide, Rotate };

    void Start()
    {
        iTweenArgs = iTween.Hash();
        iTweenArgs.Add("position", openPosition);
        iTweenArgs.Add("time", animationTime);
        iTweenArgs.Add("islocal", true);

    }

    void Update()
    {

    }

    public void PerformAction()
    {
        if (isOpen)
        {
            iTweenArgs["position"] = closedPosition;
            iTweenArgs["rotation"] = closedPosition;
        }
        else
        {
            iTweenArgs["position"] = openPosition;
            iTweenArgs["rotation"] = openPosition;
        }

        isOpen = !isOpen;

        switch (movementType)
        {
            case MovementType.Slide:
                iTween.MoveTo(gameObject, iTweenArgs);
                break;
            case MovementType.Rotate:
                iTween.RotateTo(gameObject, iTweenArgs);
                break;
        }
    }
}
