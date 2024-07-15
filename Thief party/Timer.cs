using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;


public class Timer : MonoBehaviourPunCallbacks
{
    public float timerValue = 180f;
    public bool isTimerOn = false;
    public TextMeshProUGUI UITimer;
    public PlayerMovement player;
    public TextMeshProUGUI UITimer_pausa;

    void Start()
    {
        // Iniciar el temporizador
        StartTimer();
        GameObject canvas = GameObject.Find("UI");
        //Transform paneltransform = canvas.transform.Find("tiempo acabado");
        //GameObject panel = paneltransform.gameObject;

    }

    private void Update()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        if (photonView.IsMine && isTimerOn)
        {
            InitializeTimer();
        }
        if (timerValue == 0)
        {
            player.PanelTiempo();

            GameObject.Find("StartGame").transform.GetChild(0).gameObject.SetActive(false); // desactivar monedas y power Ups
            GameObject.Find("Caballero").gameObject.SetActive(false);
            GameObject.Find("UI").transform.GetChild(3).gameObject.SetActive(false);
            GameObject.Find("UI/UI_Player").gameObject.SetActive(false);
            GameObject.Find("UI/Panel_Pausa").gameObject.SetActive(false);
        }
    }

    public void StartTimer()
    {
        isTimerOn = true;
    }

    public void StopTimer()
    {
        isTimerOn = false;
        
      
    }
    
    public void InitializeTimer()
    {
        if (timerValue > 0)
        {
            timerValue -= Time.deltaTime;
        }
        else
        {
            timerValue = 0;
            StopTimer();
            
        }
        
        ShowTime(timerValue);

        // Sincronizar el valor del temporizador a través de la red
        photonView.RPC("UpdateTimer", RpcTarget.All, timerValue);
    }
    
    [PunRPC]
    void UpdateTimer(float newTimerValue)
    {
        timerValue = newTimerValue;
        ShowTime(timerValue);
    }

    void ShowTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        UITimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        UITimer_pausa.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
    #region // viejo
    //public void TimerBegins()
    // {
    //     if (!isTimerOn)
    //     {
    //         isTimerOn = true;
    //         timeRemaining = timer; 

    //     }
    // }

    // public void ticks()
    // {

    // }
    #endregion

