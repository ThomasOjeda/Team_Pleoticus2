using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using Mirror;

public class scriptjugadormultijugador : NetworkBehaviour
{
    [SerializeField] public Vector3 movement = new Vector3();

    [Client]
    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority) { return; }

        if (!Input.GetKeyDown(KeyCode.Space)) { return; }

        transform.Translate(movement);
    }
}
