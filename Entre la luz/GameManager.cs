using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data.SqlTypes;

public class GameManager : MonoBehaviour
{
    [Header("Player Script")]
    public Mov_Player PlayerScript;

    #region //variables NPCS, fungus y demas 
    [Header("Flowcharts de los NPCS")]
    public Flowchart flowchart_ISA;
    public Flowchart flowchart_LIRIA;
    public Flowchart flowchart_ENOKI;
    public Flowchart flowchart_ANGUS;

    [Header("Componente Boten de los NPCS")]
    public Button ButtonToChat_NPC1;
    public Button ButtonToChat_NPC2;
    public Button ButtonToChat_NPC3;
    public Button ButtonToChat_NPC4;

    [Header("Está activo el dialogo?")]
    public bool TalkingState_ISA;
    public bool TalkingState_LIRIA;
    public bool TalkingState_ENOKI;
    public bool TalkingState_ANGUS;
  
    #endregion

    #region // variables de props interactuables, sus fungs y demas
    [Header("Objetos interactuables")]
    public Flowchart ArmarioGetKey;

    [Header("Está activo el dialogo de los interactuables?")] 
    public bool TalkingState_PROPS;

    [Header("Botones de los props")]
    public Button ButtonToInteract_1;


    //[Header("Eljugador consiguio la llave")]
    //public bool PlayerFoundKey == false;
    #endregion

    public void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        TalkingState_ISA = flowchart_ISA.GetBooleanVariable("IsTalkingW_NPC1");
        TalkingState_LIRIA = flowchart_LIRIA.GetBooleanVariable("IsTalkingW_NPC2");
        TalkingState_ENOKI = flowchart_ENOKI.GetBooleanVariable("IsTalkingW_NPC3");
        TalkingState_ANGUS = flowchart_ANGUS.GetBooleanVariable("IsTalkingW_NPC4");
        TalkingState_PROPS = ArmarioGetKey.GetBooleanVariable("TalkingToProp");

        IsDialogueActive();
        ButtonToChat_NPC1.onClick.AddListener(TaskOnClick_1);
        ButtonToChat_NPC2.onClick.AddListener(TaskOnClick_2);
        ButtonToChat_NPC3.onClick.AddListener(TaskOnClick_3);
        ButtonToChat_NPC4.onClick.AddListener(TaskOnClick_4);
        ButtonToInteract_1.onClick.AddListener(TaskOnClick_Prop1);
        
    }


    public void TaskOnClick_1()
    {
        flowchart_ISA.SetBooleanVariable("IsTalkingW_NPC1", true); TalkingState_ISA = true;
    }

    public void TaskOnClick_2()
    {
        flowchart_LIRIA.SetBooleanVariable("IsTalkingW_NPC2", true); TalkingState_LIRIA = true;
    }
    
    public void TaskOnClick_3()
    {
        flowchart_ENOKI.SetBooleanVariable("IsTalkingW_NPC3", true); TalkingState_ENOKI = true;
    }
    public void TaskOnClick_4()
    {
        flowchart_ANGUS.SetBooleanVariable("IsTalkingW_NPC4", true); TalkingState_ANGUS = true;
    }
    public void TaskOnClick_Prop1()
    {
        ArmarioGetKey.SetBooleanVariable("TalkingToProp", true);
        TalkingState_PROPS = true;
    }
    public void IsDialogueActive()
    {
       
        if (ArmarioGetKey.GetBooleanVariable("TriggerToGetKey") == true)
        {
            PlayerScript.HasKey = true;
        }
        #region //Estado de dialogo de los NPCs
        if (TalkingState_ISA || TalkingState_LIRIA || TalkingState_ENOKI || TalkingState_ANGUS || TalkingState_PROPS)
        {
            PlayerScript.PlayerSpeed = 0;
        }

        else
        {
            PlayerScript.PlayerSpeed = 2;
        }

        #endregion
    }
    public void Pause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MENU");
    }
}


   

