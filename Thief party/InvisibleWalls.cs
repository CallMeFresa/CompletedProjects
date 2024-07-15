using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;

public class InvisibleWalls : MonoBehaviourPunCallbacks
{
    public Material materialInvisible;
    private Material materialOriginal;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>(); // Obtener el renderer del objeto al que está asignado el script
        materialOriginal = rend.material; // Almacenar el material original
    }

    void Update()
    {
        // Obtener el rayo desde el centro de la cámara
        Ray rayo = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitInfo;

        // Comprobar si el rayo colisiona con el objeto al que está asignado el script
        if (Physics.Raycast(rayo, out hitInfo) && hitInfo.collider.gameObject == gameObject)
        {
            // Cambiar el material del objeto al que está asignado el script
            rend.material = materialInvisible;
        }
        else
        {
            // Restaurar el material original si el rayo no colisiona con el objeto
            rend.material = materialOriginal;
        }
    }
}