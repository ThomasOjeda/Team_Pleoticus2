using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirConTecla : MonoBehaviour
{
    public string tecla;
    public GameObject canvas;
    public GameObject jugador;

    private Component movimiento;
    private Component camara;

    private Boolean activo;

    // Start is called before the first frame update
    void Start()
    {
        activo = false;
        movimiento = jugador.GetComponent("Character Controller");
        camara = jugador.GetComponent("Mouse Look");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(tecla))
        {
            if (activo)
            {
                canvas.SetActive(false);
                activo = false;
                movimiento.enabled = true;
                camara.SetActive(true);
            }
            else
            {
                canvas.SetActive(true);
                activo = true;
                movimiento.enabled = false;
                camara.SetActive(false);
            }
        }
    }
}
