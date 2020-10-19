using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MountCamera : NetworkBehaviour
{
    public GameObject CameraMountPoint;

    void Start()
    {
        if (isLocalPlayer)
        {
            Transform cameraTransform = GameObject.Find("SceneMainCamera").transform;  //Find main camera which is part of the scene instead of the prefab
            cameraTransform.parent = CameraMountPoint.transform;  //Make the camera a child of the mount point
            cameraTransform.position = CameraMountPoint.transform.position;  //Set position/rotation same as the mount point
            cameraTransform.rotation = CameraMountPoint.transform.rotation;
        }
    }
}
