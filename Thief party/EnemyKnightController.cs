using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Unity.VisualScripting;


public class EnemyKnightController : MonoBehaviour
{
    private PlayerMovement PlayerMov;
    public EasyTween RestarTime;
    private bool ActivoMenosTiempo = false;

    public NetworkManager NetMan;

    [Header("Componentes Caballero")]
    public NavMeshAgent knightagent;
    public Animator animKnight;
    public PhotonView photonView;

    [Header("Daño monedas/tiempo")]
    public int damage;
    private float lessTime = 0f;

    [Header("Referencia del timer")]
    public Timer refKnightTimer;

    [Header("Lista de referencias de jugadores y puntos de patrullaje")]
    public List<Transform> patrolPoints;
    public List<Transform> player;

    [Header("Transforms de siguientes targets")]
    public Transform pointTo;
    public Transform playerTo;

    public void UpdatePlayer()
    {
        StartCoroutine(UpdateP());
    }
    IEnumerator UpdateP()
    {
        yield return new WaitForSeconds(2);
        player.Clear();
        GameObject[] currentPlayer = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < currentPlayer.Length; i++)
        {
            player.Add(currentPlayer[i].transform);
        }
    }
    public void Start()
    {
        transform.position = new Vector3(-16.6599998f, -1.75800002f, 63.5999985f);

        //refKnightTimer = GameObject.FindGameObjectWithTag("timerTag").GetComponent<Timer>();
        knightagent = GetComponent<NavMeshAgent>();
        GameObject[] points = GameObject.FindGameObjectsWithTag("mainHallPatrolPoint");

        for (int i = 0; i < points.Length; i++)
        {
            patrolPoints.Add(points[i].transform);
        }

        int ran = Random.Range(0, patrolPoints.Count);
        pointTo = patrolPoints[ran];

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            PlayerMov = playerObject.GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        foreach (var item in player)
        {
            if (item == null)
            {
                return;
            }
        }
        if (NetMan.StartGame == true)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            PlayerClose();
            float disToPlayer = Vector3.Distance(transform.position, playerTo.position);
            if (disToPlayer < 8)
            {
                knightagent.SetDestination(playerTo.position);
                if (disToPlayer < 5)
                {
                    #region//animaciones
                    knightagent.speed = 3.8f;
                    animKnight.SetBool("run", true);
                    animKnight.SetBool("attack", false);
                    #endregion
                }

                if (disToPlayer < 2f)
                {
                    #region // animaciones
                    knightagent.speed = 0f;
                    animKnight.SetBool("attack", true);
                    animKnight.SetBool("run", false);
                    #endregion
                }
            }
            else
            {
                animKnight.SetBool("run", false);
                animKnight.SetBool("attack", false);
                OnPatrol();
            }
        }
            
    }

    void OnPatrol()
    {
        float distance = Vector3.Distance(transform.position, pointTo.position);
        if (distance < 0.5f)
        {
            int ran = Random.Range(0, patrolPoints.Count);
            pointTo = patrolPoints[ran];
            #region // animaciones

            animKnight.SetFloat("speed", knightagent.desiredVelocity.sqrMagnitude);
            animKnight.SetBool("attack", false);
            animKnight.SetBool("run", false);
            #endregion
        }
        knightagent.SetDestination(pointTo.position);
        animKnight.SetFloat("speed", 1);
        knightagent.speed = 3f;
    }

    void PlayerClose()
    {
        foreach (var item in player)
        {
            if(item == null)
            {
                return;
            }
        }
        playerTo = GetClosestPlayers(player);
    }

    Transform GetClosestPlayers(List<Transform> players)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in players)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    [PunRPC]
    void LessTimeOnTouch(int playerActorNumber)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //StartCoroutine(tiempored());
        }
    }

    //IEnumerator tiempored()
    //{
    //    foreach (Timer REFTime)
    //    {
    //        REFTime.timerValue -= lessTime;
    //        //StartCoroutine(RestarTiempo());

    //        refKnightTimer.timerValue -= lessTime;
    //        yield return new WaitForSeconds(7);

    //    }
    //}

    public IEnumerator RestarTiempo()
    {
        if(!RestarTime.gameObject.activeSelf)
        {
            RestarTime.OpenCloseObjectAnimation();
        }
        
        yield return new WaitForSeconds(0.7f);

        if (RestarTime.gameObject.activeSelf)
        {
            RestarTime.OpenCloseObjectAnimation();

            yield return new WaitForSeconds(0.2f);

            RestarTime.gameObject.SetActive(false);
        }
    }

    public IEnumerator CooldownTimer()
    {
        refKnightTimer.timerValue -= lessTime;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(7);
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {   
        
        foreach (var item in player)
        {
            if(item == null)
            {
                return;
            }
        }
        //Debug.Log("Detectando player siendo atacado!!");
        if (other.CompareTag("Player"))
        {
            //PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            if (other.GetComponent<PhotonView>().IsMine)
            {

                StartCoroutine(CooldownTimer());
                //StartCoroutine(tiempored());
                
                //if (playerMovement.contador > 0)
                //{
                //StartCoroutine(playerMovement.EfectoPerderMonedas());
                //playerMovement.contador = 0;
                //playerMovement.Puntos.text = playerMovement.contador.ToString();
                other.GetComponent<PhotonView>().RPC("SendScore", RpcTarget.All, 0);
                //}
                //photonView.RPC("LessTimeOnTouch", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber);
                other.GetComponent<PhotonView>().RPC("restTimer", RpcTarget.All);
            }
            if (photonView.IsMine)
            {
                //LessTimeOnTouch();
            }
        }
    }
}

