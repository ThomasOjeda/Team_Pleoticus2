﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform ubicacionTeleport;

   

    void OnTriggerEnter(Collider other)
    {
        print("TeleportActivado");
        other.transform.position = ubicacionTeleport.transform.position;
    }


}
