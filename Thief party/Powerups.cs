using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;
public class Powerups : MonoBehaviour
{
    public Vector3 DirRot; // Permite al objeto con el codigo rotar en cualquier eje marcado por el Dev en el inspector.

    private Vector3 escalaOriginal;

    public enum powerup { Velocidad, Lentitud, Borracho, Invisibiladad, Desmayo, PuntosMenos, x2, Iman };
    public powerup poweruptype;

    private int viewID;
    private PlayerMovement player;
    PhotonView photonView;

    public void Start()
    {
        escalaOriginal = transform.localScale;
    }

    private void Update()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<PlayerMovement>();
        }
        

    }

    void FixedUpdate()
    {
        transform.Rotate(DirRot); // El objeto rota en la dirección establecida en el inspector.
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.CompareTag("Player"))
        {
            StartCoroutine(escalarPowerUps());
            PhotonView pv = other.gameObject.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                switch (poweruptype)
                {
                    //case powerup.Velocidad:
                    //    StartCoroutine(Velocidad(other));
                    //    break;
                    //case powerup.Lentitud:
                    //    PlayerMovement playerMovemen = other.gameObject.GetComponent<PlayerMovement>();
                    //    StartCoroutine(playerMovemen.lentitud(other));
                    //    break;

                    case powerup.Invisibiladad:
                        int viewID = pv.ViewID;
                        pv.RPC("MakeInvisible", RpcTarget.All, viewID);
                        break;
                    case powerup.x2:
                        StartCoroutine(x2(other));
                        break;
                    case powerup.PuntosMenos:
                        PlayerMovement playerMovement1 = other.gameObject.GetComponent<PlayerMovement>();
                        StartCoroutine(playerMovement1.puntosmenos(other));
                        break;
                    //case powerup.Desmayo:
                    //    PlayerMovement playerMovementq = other.gameObject.GetComponent<PlayerMovement>();
                    //    StartCoroutine(playerMovementq.desmatos(other));
                    //    break;
                    case powerup.Borracho:

                        PhotonView[] allPhotonViews = FindObjectsOfType<PhotonView>();

                        foreach (PhotonView playerPhotonView in allPhotonViews)
                        {
                            if (playerPhotonView != null)
                            {
                                if (playerPhotonView.gameObject.CompareTag("Player"))
                                {
                                    int playerId = playerPhotonView.ViewID;
                                    //Debug.Log("Aplicando power-up de borracho al jugador con ID: " + playerId);
                                    playerPhotonView.RPC("SetBorrachoState", RpcTarget.All, true, playerId);
                                }
                            }
                        }
                        break;
                    case powerup.Iman:
                        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
                        StartCoroutine(playerMovement.Iman());
                        break;
                }
            }


        }
    }

    IEnumerator escalarPowerUps()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false; 
        gameObject.transform.localScale = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(10);

        gameObject.transform.localScale = escalaOriginal;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    IEnumerator Velocidad(Collider playerCollider)
    {
        PlayerMovement playerMovement = playerCollider.GetComponent<PlayerMovement>();
        float originalSpeed = 5;
        playerMovement.MoveSpeed = 8;
        yield return new WaitForSeconds(5);
        playerMovement.MoveSpeed = originalSpeed;
    }

   IEnumerator x2(Collider playerCollider)
    {
        PlayerMovement playerMovement = playerCollider.GetComponent<PlayerMovement>();
        playerMovement.powerupactivo = true;
        //playerMovement.contador = playerMovement.contador += 20;
        //Debug.Log("agarro");
        player.X2.Play();
        yield return new WaitForSeconds(10);
        playerMovement.powerupactivo = false;
    }
}



