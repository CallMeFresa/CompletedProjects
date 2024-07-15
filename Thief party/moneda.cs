using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class moneda : MonoBehaviour
{
    PhotonView pv;
    // Este es el codigo para ejecutar las particulas de destrucción de un objeto arrojadizo.

    //public ParticleSystem DestroyObject_Particle; // En este espacio van las particulas que se ejecutaran

    private void Update()
    {
        transform.Rotate(Vector3.right * 2.5f);
    }

    void Start()
    {
        pv = GetComponent<PhotonView>();
    }
    public void MoveTowardsPlayer(Vector3 playerPosition, float speed)
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
        transform.position = newPosition;
        Debug.Log("Movimiento iniciado en " + gameObject.name);
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player") && photonView.IsMine) // Toma los puntos y se los suma al jugador correspondiente.
    //    {
    //        if (photonView.IsMine)
    //        {
    //            PhotonNetwork.Destroy(gameObject);
    //        }
    //    }
    //    else // Destruye el objeto para todos los jugadores.
    //    {
    //        if (gameObject.CompareTag("Player"))
    //        {
    //            PhotonNetwork.Destroy(gameObject);
    //        }
    //    }
    //}
}