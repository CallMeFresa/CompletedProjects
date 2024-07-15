using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonesgameOver : MonoBehaviour
{
    [Header("Flowcharts que dan a game over")]
    public Flowchart Enoki_GAMEOVER;
    public Flowchart Liria_GAMEOVER;

    [Header("botones del game over")]
    public GameObject Button_RELOAD;
    public GameObject Button_BackToMenu;

    public bool Enoki;
    public bool Liria; 

    public void Update()
    {
        VerifyingPlayerState();

        Enoki = Enoki_GAMEOVER.GetBooleanVariable("DidPlayerDied");
        Liria = Liria_GAMEOVER.GetBooleanVariable("DidPlayerDied");

    }

    public void VerifyingPlayerState()
    {
        if (Enoki || Liria) // si son true
        {
            Button_RELOAD.SetActive(true); Button_BackToMenu.SetActive(true);
            
        }
        else 
        {
            Button_RELOAD.SetActive(false); Button_BackToMenu.SetActive(false);
        }
    }
}
