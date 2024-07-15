using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using TMPro;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public CharacterController characterController;
    public Animator animator;
    private EnemyKnightController Enemy;
    private EasyTween RankF;

    [Header("GAMEOBJECTS / TRANSFORMS")]
    public GameObject camara;
    public GameObject ui_Game_Script;
    public GameObject NetManager;
    public GameObject alphaJoints;
    public GameObject alphaSurface;
    public GameObject Inst_Particulas;
    public GameObject TeclaE;
    public GameObject CanvasTeclaE;
    public GameObject powerprefab;
    public GameObject powerprefab2;
    public GameObject prefablentitud;
    public GameObject modelopower;
    public GameObject modelopower2;
    public GameObject modelolentitud;
    public Transform powerspawn;

    [Header("FLOATS")]
    public float MoveSpeed = 5f;
    public float rotationspeed = 2f;
    public float fuerzalanzamiento = 50;
    public float gravity = -9;
    public float invertir = 1;
    public float imanradio;
    public float imanvelocidad;

    [Header("INT")]
    public int contador = 0;
    public int contador_SAFE = 0;

    [Header("BOOLS")]
    public bool desmayo = false;
    public bool powerupactivo = false;
    public bool borracho = false;
    public bool iman = false;
    public bool lanzado = false;
    public bool destruido = false;
    private bool RankFinal = false;

    [Header("TEXTOS")]
    public TextMeshPro PlayerName;
    public TextMeshProUGUI Puntos;
    public TextMeshProUGUI PuntosGuardados;

    [Header("SONIDOS")]
    private AudioSource TomarMoneda;
    private AudioSource Tap;

    [Header("SONIDOS POWER.UPS")]
    private AudioSource Borrach;
    private AudioSource Invisible;
    private AudioSource MenosMonedas;
    private AudioSource Caballero;
    public AudioSource X2; // Es p�blica para poder acceder a ella dede el codigo de powerUp

    [Header("PARTICULAS")]
    public GameObject Velocidad, Lentitud, Borracho, Invisibiladad, Desmayo, PuntosMenos, x2, IMAN;
    powercollision powers;

    private void Start()
    {
        Enemy = GameObject.Find("Caballero").GetComponent<EnemyKnightController>();
        powers = GetComponent<powercollision>();
        animator = GetComponent<Animator>();
        camara = GameObject.Find("Main Camera");
        alphaSurface = GameObject.Find("Mesh");
        alphaJoints = GameObject.Find("Mesh");
        RankF = GameObject.Find("RankFinal").GetComponent<EasyTween>();

        if (photonView.IsMine) //Jugador Local
        {
            ui_Game_Script = GameObject.Find("Game Manager");
            NetManager = GameObject.Find("Network Manager");
            camara.GetComponent<CameraFollow>().target = gameObject.transform;
            camara.GetComponent<CameraFollow>().StartFollow();
            camara.SetActive(true);
            photonView.Owner.NickName = PlayerPrefs.GetString("nick");
            Puntos = GameObject.Find("Puntos").GetComponent<TextMeshProUGUI>();
            //PuntosGuardados = GameObject.Find("Puntos Guardados").GetComponent<TextMeshProUGUI>();
            TomarMoneda = GameObject.Find("MonedasSonido").GetComponent<AudioSource>();
            Tap = GameObject.Find("TapSonido").GetComponent<AudioSource>();
            Inst_Particulas = GameObject.Find("Particulas");
            GameObject.Find("UI/PantallaCarga").gameObject.SetActive(false);
            if (PhotonNetwork.PlayerList.Length >= NetManager.GetComponent<NetworkManager>().MaxPlayers)
            {
                GameObject[] players;
                players = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].GetComponent<PhotonView>().RPC("StartGame", RpcTarget.All);
                }

                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
            }


            GameObject[] enemies;
            enemies = GameObject.FindGameObjectsWithTag("enemyKNIGHT");
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyKnightController>().UpdatePlayer();
            }
        }
        else
        {
            PlayerName.text = photonView.Owner.NickName;
        }
        NetManager = GameObject.Find("Network Manager");
        GameObject.Find("Game Manager").GetComponent<Ranking>().GetUserScore();
        GameObject.Find("UI/EsperandoJugadores/ContadorJugadores").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList.Length.ToString() ;
        GameObject.Find("UI/EsperandoJugadores/MAXJugadores").GetComponent<TextMeshProUGUI>().text = NetManager.GetComponent<NetworkManager>().MaxPlayers.ToString();
        GameObject.Find("UI/Panel_Pausa/Barra_Superior/EsperandoJugadores-2/ContadorJugadores").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList.Length.ToString();
        GameObject.Find("UI/Panel_Pausa/Barra_Superior/EsperandoJugadores-2/MaxJugadores").GetComponent<TextMeshProUGUI>().text = NetManager.GetComponent<NetworkManager>().MaxPlayers.ToString();
        Borrach = GameObject.Find("Sonidos/PowerUps/Borracho").GetComponent<AudioSource>();
        Invisible = GameObject.Find("Sonidos/PowerUps/Invisible").GetComponent<AudioSource>();
        X2 = GameObject.Find("Sonidos/PowerUps/X2").GetComponent<AudioSource>();
        MenosMonedas = GameObject.Find("Sonidos/PowerUps/MenosMonedas").GetComponent<AudioSource>();
        Caballero = GameObject.Find("Caballero/Sonidos/RestarTiempo").GetComponent<AudioSource>();
    }

    #region // Conteo regresivo Empezar
    [PunRPC]
    public void StartGame()
    {
        if (photonView.IsMine)
        {
            StartCoroutine(Empezar());

            IEnumerator Empezar()
            {
                ui_Game_Script.GetComponent<UI_Game>().AsegurandoMonedas = true;
                animator.SetBool("camina", false);

                yield return new WaitForSeconds(3);

                Tap.Play();
                GameObject.Find("UI/PantallaIniciar").transform.GetChild(0).gameObject.SetActive(true);

                yield return new WaitForSeconds(1);

                Tap.Play();
                GameObject.Find("UI/PantallaIniciar/Panel/Comenzamos").transform.GetChild(0).gameObject.SetActive(true);

                yield return new WaitForSeconds(1);

                Tap.Play();
                GameObject.Find("UI/PantallaIniciar/Panel/Comenzamos").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("UI/PantallaIniciar/Panel/Comenzamos").transform.GetChild(1).gameObject.SetActive(true);

                yield return new WaitForSeconds(1);

                Tap.Play();
                GameObject.Find("UI/PantallaIniciar/Panel/Comenzamos").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("UI/PantallaIniciar/Panel/Comenzamos").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("UI/PantallaIniciar/Panel/Comenzamos").transform.GetChild(2).gameObject.SetActive(true);

                yield return new WaitForSeconds(1);

                Tap.Play();
                GameObject.Find("UI/PantallaIniciar/Panel/Comenzamos").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("UI/PantallaIniciar/Panel/Comenzamos").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("UI/PantallaIniciar/Panel/Comenzamos").transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("UI/PantallaIniciar/Panel/Comenzamos").transform.GetChild(3).gameObject.SetActive(true);

                yield return new WaitForSeconds(0.5f);

                GameObject.Find("UI/PantallaIniciar").gameObject.SetActive(false);
                ui_Game_Script.GetComponent<UI_Game>().AsegurandoMonedas = false;
                GameObject.Find("UI").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("UI").transform.GetChild(4).gameObject.SetActive(false);
                //GameObject.Find("UI/Panel_Pausa/Barra_Superior").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("UI/Panel_Pausa/Barra_Superior").transform.GetChild(4).gameObject.SetActive(false);
                NetManager.GetComponent<NetworkManager>().StartGame = true;
                GameObject.Find("StartGame").transform.GetChild(0).gameObject.SetActive(true); // Activar monedas y power Ups
                GameObject.Find("Caballero").transform.position = new Vector3(-16.6599998f, 0.402999997f, 63.5999985f);
                GameObject.Find("Caballero/Sonidos/PasosCaballero").GetComponent<AudioSource>().Play();
            }
        }
    }
    #endregion

    [PunRPC]
    public void Rank()
    {
        if (RankFinal == false)
        {
            RankF.OpenCloseObjectAnimation();
            animator.SetBool("camina", false);
            RankFinal = true;
        }

        if (RankFinal == true)
        {

        }
    }

    [PunRPC]
    public void restTimer()
    {
        GameObject.Find("UI").transform.GetChild(3).GetChild(1).GetComponent<Timer>().timerValue -= 5;
        if (!Enemy.RestarTime.gameObject.activeSelf)
        {
            StartCoroutine(Enemy.RestarTiempo());
        }
    }

    private void Update()
    {
        Movimiento();
        PlayerName.transform.parent.transform.LookAt(camara.transform.position);
        TeclaE.transform.parent.transform.LookAt(camara.transform.position);
        moneda[] coins = FindObjectsOfType<moneda>();
        //if (photonView.IsMine && coins.Length == 0)
        //{
        //    RankF.OpenCloseObjectAnimation();
        //}
        //if (photonView.IsMine && coins.Length == 0)
        //{
        //    if (photonView.IsMine)
        //    {
        //        GameObject[] players;
        //        players = GameObject.FindGameObjectsWithTag("Player");
        //        for (int i = 0; i < players.Length; i++)
        //        {
        //            if (players[i].GetComponent<PhotonView>().IsMine == false)
        //            {
        //                players[i].GetComponent<PhotonView>().RPC("perdedor", RpcTarget.All);
        //            }
        //        }
        //    }
        //}

        if (photonView.IsMine)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                GameObject.Find("UI").transform.GetChild(0).GetComponent<EasyTween>().OpenCloseObjectAnimation();
            }
        }
    }

    public void PanelTiempo()
    {
        if (photonView.IsMine)
        {
            GetComponent<PhotonView>().RPC("Rank", RpcTarget.All);
        }
        else
        {
            GameObject[] players;
            players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<PhotonView>().IsMine == false)
                {
                    players[i].GetComponent<PhotonView>().RPC("Rank", RpcTarget.All);
                }
            }
        }
        #region // pantalla perdedor???
        //if (contador <= 10 && photonView.IsMine == false)
        //{
        //    GameObject[] players;
        //    players = GameObject.FindGameObjectsWithTag("Player");
        //    for (int i = 0; i < players.Length; i++)
        //    {
        //        if (players[i].GetComponent<PhotonView>())
        //        {
        //            players[i].GetComponent<PhotonView>().RPC("perdedor", RpcTarget.All);
        //        }
        //    }
        //}
        #endregion
    }

    public void Desmayar()
    {
        desmayo = true;
    }


    public void Animo()
    {
        desmayo = false;
    }

    

    void Movimiento() // Movimiento del personaje segun ciertos estados, como pausa o powerUp borracho.
    {
        if (photonView.IsMine && ui_Game_Script.GetComponent<UI_Game>().CanMove == true)
        {

            if (!desmayo)
            {
                float h = Input.GetAxis("Horizontal") * invertir;
                float v = Input.GetAxis("Vertical") * invertir;

                Vector3 movementDirection = new Vector3(h, 0, v);
                movementDirection.Normalize();

                // Aplica la velocidad de movimiento
                Vector3 movement = movementDirection * MoveSpeed * Time.deltaTime;

                // Rota al personaje hacia la direcci�n de movimiento si hay movimiento
                if (movementDirection != Vector3.zero)
                {
                    transform.forward = movementDirection;
                }

                // Aplica la gravedad al movimiento en el eje y
                movement.y += gravity * Time.deltaTime;

                // Mueve al personaje usando CharacterController.Move()
                characterController.Move(movement);

                // Controla las animaciones de caminar
                if (v != 0 || h != 0)
                {
                    animator.SetBool("camina", true);

                    if ((v != 0 || h != 0) && borracho == true)
                    {
                        animator.SetBool("Desmayo", false);
                        animator.SetBool("Borracho", true);
                    }
                }
                else
                {
                    animator.SetBool("camina", false);
                    animator.SetBool("Desmayo", false);

                    if ((v == 0 || h == 0) && borracho == true)
                    {
                        animator.SetBool("Desmayo", true);
                    }
                }
            }
        }
        else if(photonView.IsMine && ui_Game_Script.GetComponent<UI_Game>().CanMove == false)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                animator.SetBool("camina", false);
            }
        }
    }

    //inicio del power up de invisibilidad
    [PunRPC] //se hace que el power up sea para todos
    public void MakeInvisible(int viewID)
    {
        PhotonView pv = PhotonView.Find(viewID);
        if (pv != null && pv.IsMine)
        {
            StartCoroutine(InvisibilidadRoutine(pv));
        }
    }

    IEnumerator InvisibilidadRoutine(PhotonView pv)
    {
        string alphaSurfaceName = "Mesh";
        string alphaJointsName = "Mesh";

        Invisible.Play();
        pv.RPC("SetVisibility", RpcTarget.AllBuffered, false, alphaSurfaceName, alphaJointsName);
        yield return new WaitForSeconds(10);
        // Restauras la visibilidad del jugador correspondiente
        pv.RPC("SetVisibility", RpcTarget.AllBuffered, true, alphaSurfaceName, alphaJointsName);
    }

    [PunRPC]
    public void SetVisibility(bool isVisible, string alphaSurfaceName, string alphaJointsName)
    {
        GameObject alphaSurface = transform.Find(alphaSurfaceName).gameObject;
        GameObject alphaJoints = transform.Find(alphaJointsName).gameObject;

        if (alphaSurface != null && alphaJoints != null)
        {
            alphaSurface.SetActive(isVisible);
            alphaJoints.SetActive(isVisible);
        }
    }
    //finalizacion del power up de invisibilidad

    [PunRPC]
    public void SendScore(int state)
    {
        if (state == 0) // Toca el caballeero pieerde todas las monedas
        {
            contador = 0;
            GameObject.Find("Game Manager").GetComponent<Ranking>().UpdateUserScore(photonView.Owner.NickName, contador);
            Caballero.Play();
        }
        if (state == 1) // Power up de puntos menos pierde 10 monedas
        {
            if(contador >= 10)
            {
                contador -= 10;
                GameObject.Find("Game Manager").GetComponent<Ranking>().UpdateUserScore(photonView.Owner.NickName, contador);
            }
        }
        if (state == 2) // contador de monedas suma 10 monedas
        {
            GameObject.Find("Game Manager").GetComponent<Ranking>().UpdateUserScore(photonView.Owner.NickName, contador);
        }
        if (photonView.IsMine) // Actualiza el contador de la UI
        {
            Puntos.text = contador.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        #region //Sumar Puntos
        if (other.gameObject.CompareTag("puntos") && photonView.IsMine) // Toma los puntos y se los suma al jugador correspondiente.
        {
            iTween.ShakePosition(camara, iTween.Hash("amount", new Vector3(0.1f, 0.1f, 0.1f), "time", 0.02f));
            TomarMoneda.Play();
            if (powerupactivo)
            {
                contador += 20;

            }
            else if(!powerupactivo)
            {
                contador += 10;
            }
            photonView.RPC("SendScore", RpcTarget.All, 2);
            Puntos.text = contador.ToString();
            StartCoroutine(EfectoSumarMonedas());
            PhotonNetwork.Instantiate("TomarCoins-Particulas", Inst_Particulas.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("puntos") && !photonView.IsMine) // Toma los puntos y se los suma al jugador correspondiente.
        {
            if (powerupactivo)
            {
                contador += 20;
            }
            else if (!powerupactivo)
            {
                contador += 10;
            }
            photonView.RPC("SendScore", RpcTarget.All, 2);
            Destroy(other.gameObject);
        }

        #endregion
        #region //Activar Particulas

        if (photonView.IsMine)
        {
            if (other.gameObject.CompareTag("Particula_X2"))
            {
                StartCoroutine(activar());

                IEnumerator activar()
                {
                    x2.SetActive(true);
                    ui_Game_Script.GetComponent<UI_Game>().X2.OpenCloseObjectAnimation();

                    yield return new WaitForSeconds(10);

                    x2.SetActive(false);
                    ui_Game_Script.GetComponent<UI_Game>().X2.OpenCloseObjectAnimation();
                }
            }

            //if (other.gameObject.CompareTag("Particula_Velocidad"))
            //{
            //    StartCoroutine(activar());

            //    IEnumerator activar()
            //    {
            //        Velocidad.SetActive(true);
            //        ui_Game_Script.GetComponent<UI_Game>().Velocidad.OpenCloseObjectAnimation();

            //        yield return new WaitForSeconds(6.5f);

            //        Velocidad.SetActive(false);
            //        ui_Game_Script.GetComponent<UI_Game>().Velocidad.OpenCloseObjectAnimation();
            //    }
            //}

            if (other.gameObject.CompareTag("Particula_Invisibilidad"))
            {
                StartCoroutine(activar());

                IEnumerator activar()
                {
                    Invisibiladad.SetActive(true);
                    ui_Game_Script.GetComponent<UI_Game>().Invisibilidad.OpenCloseObjectAnimation();

                    yield return new WaitForSeconds(10);

                    Invisibiladad.SetActive(false);
                    ui_Game_Script.GetComponent<UI_Game>().Invisibilidad.OpenCloseObjectAnimation();
                }
            }

            //if (other.gameObject.CompareTag("Particula_Iman"))
            //{
            //    StartCoroutine(activar());

            //    IEnumerator activar()
            //    {
            //        IMAN.SetActive(true);
            //        ui_Game_Script.GetComponent<UI_Game>().Iman.OpenCloseObjectAnimation();

            //        yield return new WaitForSeconds(10);

            //        IMAN.SetActive(false);
            //        ui_Game_Script.GetComponent<UI_Game>().Iman.OpenCloseObjectAnimation();
            //    }
            //}

            if (other.gameObject.CompareTag("Particula_Menos"))
            {
                StartCoroutine(activar());

                IEnumerator activar()
                {
                    PuntosMenos.SetActive(true);

                    yield return new WaitForSeconds(10);

                    PuntosMenos.SetActive(false);
                }
            }

            //if (other.gameObject.CompareTag("Particula_Lentitud"))
            //{
            //    StartCoroutine(activar());

            //    IEnumerator activar()
            //    {
            //        Lentitud.SetActive(true);
            //        ui_Game_Script.GetComponent<UI_Game>().Lentitud.OpenCloseObjectAnimation();

            //        yield return new WaitForSeconds(5);

            //        Lentitud.SetActive(false);
            //        ui_Game_Script.GetComponent<UI_Game>().Lentitud.OpenCloseObjectAnimation();
            //    }
            //}

            if (other.gameObject.CompareTag("Particula_Borracho"))
            {
                StartCoroutine(activar());

                IEnumerator activar()
                {
                    Borracho.SetActive(true);
                    ui_Game_Script.GetComponent<UI_Game>().Borracho.OpenCloseObjectAnimation();

                    yield return new WaitForSeconds(10);

                    Borracho.SetActive(false);
                    ui_Game_Script.GetComponent<UI_Game>().Borracho.OpenCloseObjectAnimation();
                }
            }

            //if (other.gameObject.CompareTag("Particula_Desmayo"))
            //{
            //    StartCoroutine(activar());

            //    IEnumerator activar()
            //    {
            //        Desmayo.SetActive(true);
            //        ui_Game_Script.GetComponent<UI_Game>().Desmayo.OpenCloseObjectAnimation();

            //        yield return new WaitForSeconds(5f);

            //        ui_Game_Script.GetComponent<UI_Game>().Desmayo.OpenCloseObjectAnimation();
            //        Desmayo.SetActive(false);
            //    }
            //}
        }
        else
        {
            if (other.gameObject.CompareTag("Particula_X2"))
            {
                StartCoroutine(activar());

                IEnumerator activar()
                {
                    x2.SetActive(true);

                    yield return new WaitForSeconds(11);

                    x2.SetActive(false);
                }
            }

            //if (other.gameObject.CompareTag("Particula_Velocidad"))
            //{
            //    StartCoroutine(activar());

            //    IEnumerator activar()
            //    {
            //        Velocidad.SetActive(true);

            //        yield return new WaitForSeconds(6.5f);

            //        Velocidad.SetActive(false);
            //    }
            //}

            //if (other.gameObject.CompareTag("Particula_Invisibilidad"))
            //{
            //    StartCoroutine(activar());

            //    IEnumerator activar()
            //    {
            //        Invisibiladad.SetActive(true);

            //        yield return new WaitForSeconds(11);

            //        Invisibiladad.SetActive(false);
            //    }
            //}

            //if (other.gameObject.CompareTag("Particula_Iman"))
            //{
            //    StartCoroutine(activar());

            //    IEnumerator activar()
            //    {
            //        IMAN.SetActive(true);

            //        yield return new WaitForSeconds(11);

            //        IMAN.SetActive(false);
            //    }
            //}

            if (other.gameObject.CompareTag("Particula_Menos"))
            {
                StartCoroutine(activar());

                IEnumerator activar()
                {
                    PuntosMenos.SetActive(true);

                    yield return new WaitForSeconds(11);

                    PuntosMenos.SetActive(false);
                }
            }

            if (other.gameObject.CompareTag("Particula_Lentitud"))
            {
                StartCoroutine(activar());

                IEnumerator activar()
                {
                    Lentitud.SetActive(true);

                    yield return new WaitForSeconds(6.5f);

                    Lentitud.SetActive(false);
                }
            }

            if (other.gameObject.CompareTag("Particula_Borracho"))
            {
                StartCoroutine(activar());

                IEnumerator activar()
                {
                    Borracho.SetActive(true);

                    yield return new WaitForSeconds(11);

                    Borracho.SetActive(false);
                }
            }

            if (other.gameObject.CompareTag("Particula_Desmayo"))
            {
                StartCoroutine(activar());

                IEnumerator activar()
                {
                    Desmayo.gameObject.SetActive(true);

                    yield return new WaitForSeconds(7f);

                    Desmayo.gameObject.SetActive(false);
                }
            }
        }

        #endregion
    }
    //[PunRPC]
    //void destruirmoneda()
    //{
    //    Destroy();
    //}

    #region //Efectos Textos

    IEnumerator EfectoSumarMonedas()
    {
        // Aumentar el tama�o del texto y cambiar el color
        Puntos.color = Color.green;

        float startTime = Time.time;
        while (Time.time < startTime + 0.1f)
        {
            float t = (Time.time - startTime) / 0.2f;
            Puntos.fontSize = Mathf.Lerp(60, 100, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        while (Time.time < startTime + 0.1f)
        {
            float t = (Time.time - startTime) / 0.2f;
            Puntos.fontSize = Mathf.Lerp(100, 60, t);
            yield return null;
        }

        //Disminuir el tama�o del texto y restaurar el color original
        Puntos.fontSize = 60;
        Puntos.color = Color.white;
    }

    //IEnumerator EfectoAsegurarMonedas()
    //{
    //    // Aumentar el tama�o del texto y cambiar el color
    //    Puntos.color = Color.cyan;
             
    //    float startTime = Time.time;
    //    while (Time.time < startTime + 0.1f)
    //    {
    //        float t = (Time.time - startTime) / 0.2f;
    //        Puntos.fontSize = Mathf.Lerp(60, 100, t);
    //        yield return null;
    //    }

    //    yield return new WaitForSeconds(0.2f);

    //    while (Time.time < startTime + 0.1f)
    //    {
    //        float t = (Time.time - startTime) / 0.2f;
    //        Puntos.fontSize = Mathf.Lerp(100, 60, t);
    //        yield return null;
    //    }

    //    //Disminuir el tama�o del texto y restaurar el color original
    //    Puntos.fontSize = 60;
    //    Puntos.color = Color.white;
    //}

    public IEnumerator EfectoPerderMonedas()
    {
        // Aumentar el tama�o del texto y cambiar el color
        Puntos.color = Color.red;

        float startTime = Time.time;
        while (Time.time < startTime + 0.1f)
        {
            float t = (Time.time - startTime) / 0.2f;
            Puntos.fontSize = Mathf.Lerp(60, 100, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        while (Time.time < startTime + 0.1f)
        {
            float t = (Time.time - startTime) / 0.2f;
            Puntos.fontSize = Mathf.Lerp(100, 60, t);
            yield return null;
        }

        // Disminuir el tama�o del texto y restaurar el color original
        Puntos.fontSize = 60;
        Puntos.color = Color.white;
    }

    #endregion
    //inicio del power up borracho
    #region // power ups 
    [PunRPC]
    public void SetBorrachoState(bool state, int playerId)
    {
        // Obtiene el PhotonView del jugador
        PhotonView photonView = PhotonView.Find(playerId);

        // Comprueba si el PhotonView existe
        if (photonView != null)
        {
            // Llama a la funci�n Borracho
            photonView.RPC("ApplyBorracho", RpcTarget.All, state);
        }
    }

    [PunRPC]
    public void ApplyBorracho(bool state)
    {
        StartCoroutine(vorracho(state));
    }
    IEnumerator vorracho(bool state)
    {
        invertir = state ? -1 : 1;
        borracho = true;
        animator.SetBool("Borracho", true);
        Borrach.Play();
        yield return new WaitForSeconds(10); // duration es la duraci�n del efecto
        borracho = false;
        animator.SetBool("Borracho", false);
        animator.SetBool("camina", true);
        invertir = 1;
    }
    //Finalizacion del power up borracho

    // Llamada para activar el power-up. Esta funci�n se llama en el cliente.
    public void ActivarIman()
    {
        photonView.RPC("ImanRPC", RpcTarget.All);
    }

    // RPC para iniciar la corrutina en todos los clientes.
    [PunRPC]
    public void ImanRPC()
    {
        StartCoroutine(Iman());
    }

    // Corrutina para el comportamiento del im�n.
    public IEnumerator Iman()
    {
        float startTime = Time.time;
        while (Time.time - startTime < imanradio)
        {
            Collider[] puntosrango = Physics.OverlapSphere(transform.position, imanradio);
            moneda[] monedasEnEscena = FindObjectsOfType<moneda>();
            foreach (Collider puntos in puntosrango)
            {
                if (puntos.gameObject.CompareTag("puntos"))
                {
                    
                    foreach (var moneda in monedasEnEscena)
                    {
                        moneda.MoveTowardsPlayer(transform.position, 0.5f);
                    }
                    //puntos.transform.position = Vector3.MoveTowards(puntos.transform.position, transform.position, imanvelocidad * Time.deltaTime);

                }
            }
            yield return null;
        }
    }
    // inicio del power up desmayo
    //public void desmaio()
    //{

    //    if (Input.GetButton("Fire1"))
    //    {
    //        GameObject nuevopower = PhotonNetwork.Instantiate(powerprefab.name, powerspawn.position, powerspawn.rotation);
    //        //nuevopower.GetComponent<Rigidbody>().velocity = powerprefab.transform.TransformDirection(Vector3.forward * 20);
    //        Rigidbody rb = nuevopower.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.useGravity = false;
    //            float fuerza = 10.0f;
    //            GameObject player = GameObject.FindWithTag("Player");
    //            Vector3 direccionadelante = player.transform.forward;
    //            Vector3 force = new Vector3(direccionadelante.x, 0, direccionadelante.z) * fuerza;
    //            rb.AddForce(force, ForceMode.Impulse);
    //            StartCoroutine(caida(rb, 0.5f));
    //            //photonView.RPC("ActualizarLanzado", RpcTarget.All, lanzado);
    //            destruido = true;
    //            Destroy(nuevopower, 8);
    //            //Invoke("powers.efecto", 7);

    //        }
    //    }

    //} 
    //public IEnumerator desmatos(Collider playerCollider)
    //{
    //    modelopower.SetActive(true);
    //    yield return new WaitUntil(() => Input.GetButton("Fire1"));
    //    desmaio();

    //    //lanzado = true;
    //    PhotonNetwork.RaiseEvent(1, true, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable);
    //    modelopower.SetActive(false);
    //    yield return new WaitForSeconds(2);
    //    //lanzado = false;
    //    PhotonNetwork.RaiseEvent(1, false, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable); 
    //    //playerMovement.powerdesmayo.SetActive(false);

    //}

    public void puntosMenos()
    {
        if (Input.GetButton("Fire1"))
        {
            GameObject nuevopower = PhotonNetwork.Instantiate(powerprefab2.name, powerspawn.position, powerspawn.rotation);
            //nuevopower.GetComponent<Rigidbody>().velocity = powerprefab.transform.TransformDirection(Vector3.forward * 20);
            Rigidbody rb = nuevopower.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                float fuerza = 10.0f;
                GameObject player = GameObject.FindWithTag("Player");
                Vector3 direccionadelante = player.transform.forward;
                Vector3 force = new Vector3(direccionadelante.x, 0, direccionadelante.z) * fuerza;
                rb.AddForce(force, ForceMode.Impulse);
                StartCoroutine(caida(rb, 0.5f));
                PuntosMenosCorrutina();
                //photonView.RPC("ActualizarLanzado", RpcTarget.All, lanzado);
                Destroy(nuevopower, 5);
                destruido = true;
                //Debug.Log("Objeto lanzado con �xito.");
            }
        }
    }
    public IEnumerator puntosmenos(Collider playercollider)
    {
        MenosMonedas.Play();
        modelopower2.SetActive(true);
        yield return new WaitUntil(() => Input.GetButton("Fire1"));
        puntosMenos();
        //PhotonNetwork.RaiseEvent(1, true, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable);
        modelopower2.SetActive(false);
        yield return new WaitForSeconds(2);
        ////PhotonNetwork.RaiseEvent(1, false, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable); ;
    }

    public void PuntosMenosCorrutina()
    {

        StartCoroutine(Puntosmenos());

        IEnumerator Puntosmenos()
        {
            if (!photonView.IsMine)
            {
                powerupactivo = true;
                contador -= 10;
                if (contador <= 0)
                {
                    contador = 0;
                }
                
                StartCoroutine(EfectoPerderMonedas());
                MenosMonedas.Play();
                Puntos.text = contador.ToString();
                photonView.RPC("SendScore", RpcTarget.All, 1);
                yield return new WaitForSeconds(10);
                powerupactivo = false;
                //if (destruido)
                //{
                //    powerupactivo = false;
                //    destruido = false;
                //}
            }
        }

       
    }

    IEnumerator caida(Rigidbody rb, float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        rb.useGravity = true;
    }
    
    public void lentitud()
    {
        if (Input.GetButton("Fire1"))
        {
            GameObject nuevopower = PhotonNetwork.Instantiate(prefablentitud.name, powerspawn.position, powerspawn.rotation);
            //nuevopower.GetComponent<Rigidbody>().velocity = powerprefab.transform.TransformDirection(Vector3.forward * 20);
            Rigidbody rb = nuevopower.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                float fuerza = 10.0f;
                GameObject player = GameObject.FindWithTag("Player");
                Vector3 direccionadelante = player.transform.forward;
                Vector3 force = new Vector3(direccionadelante.x, 0, direccionadelante.z) * fuerza;
                rb.AddForce(force, ForceMode.Impulse);
                StartCoroutine(caida(rb, 0.5f));
                //photonView.RPC("ActualizarLanzado", RpcTarget.All, lanzado);
                destruido=true;

                Destroy(nuevopower, 8);
                
                    
                
                Debug.Log("Objeto lanzado con �xito.");
            }
        }
    }
    public IEnumerator lentitud(Collider playercollider)
    {
        modelolentitud.SetActive(true);
        yield return new WaitUntil(() => Input.GetButton("Fire1"));
        lentitud();
        PhotonNetwork.RaiseEvent(1, true, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable);
        modelolentitud.SetActive(false);
        yield return new WaitForSeconds(2);
        PhotonNetwork.RaiseEvent(1, false, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable); ;
    }
    #endregion
    //se actualiza la variable lanzado para todos los jugadores presentes
    //protected virtual void OnEnable()
    //{

    //    PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    //}

    //protected virtual void OnDisable()
    //{

    //    PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    //}

    //private void NetworkingClient_EventReceived(EventData obj)
    //{
    //    if (obj.Code == 1)
    //    {
    //        lanzado = (bool)obj.CustomData;
    //        //Debug.Log("Valor de lanzado actualizado: " + lanzado);
    //    }
    //}
    //[PunRPC]
    //public void ActualizarLanzado(bool nuevoValor)
    //{
    //    lanzado = nuevoValor;
    //    //Debug.Log("Valor de lanzado actualizado: " + lanzado);
    //}
}