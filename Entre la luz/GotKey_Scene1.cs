using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

public class GotKey_Scene1 : MonoBehaviour
{
    [Header("PLAYER")]
    public Mov_Player stopMoving;

    [Header("DialogueStart")]
    public Flowchart DialogueStart;

    [Header ("Flowcharts PROPS ESCENA 1")]
    public Flowchart DialogueToPROP1;
    public Flowchart DialogueToPROP2;
    public Flowchart DialogueToPROP3;

    [Header("Botones")]
    public Button ButtonToChat_PROP1;
    public Button ButtonToChat_PROP2;
    

    [Header("Booleanos, Está activo el dialogo?")]
    public bool TalkingState_PROP1;
    public bool TalkingState_PROP2;
    public bool TalkingState_PROP3;
    public bool TalkingState_START;

    public void Start()
    {
        Time.timeScale = 1; 
    }

    public void Update()
    {
        TalkingState_PROP1 = DialogueToPROP1.GetBooleanVariable("IsTalkingWithPROP");
        TalkingState_PROP2 = DialogueToPROP2.GetBooleanVariable("IsTalkingWithPROP");
        TalkingState_PROP3 = DialogueToPROP3.GetBooleanVariable("IsTalkingWithPROP");
        TalkingState_START = DialogueStart.GetBooleanVariable("DidDialogueStart");

        IsDialogueActive();

        ButtonToChat_PROP1.onClick.AddListener(TaskOnClick_1);
        ButtonToChat_PROP2.onClick.AddListener(TaskOnClick_2);
        

    }

    public void TaskOnClick_1()
    {
        DialogueToPROP1.SetBooleanVariable("IsTalkingWithPROP", true);
        TalkingState_PROP1 = true;
    }

    public void TaskOnClick_2()
    {
        DialogueToPROP2.SetBooleanVariable("IsTalkingWithPROP", true);
        TalkingState_PROP2 = true;
    }

    public void IsDialogueActive()
    {
        if (TalkingState_PROP1 || TalkingState_PROP2 || TalkingState_PROP3 || TalkingState_START) //estan verdaderos
        {
            stopMoving.PlayerSpeed = 0; 
        }
        else if (stopMoving.Run == false)
        {
            stopMoving.PlayerSpeed = 2;
        }

       
    }

    //public void TriggerToGetKey()
    //{
    //    //if (DialogueToPROP2.GetBooleanVariable("TriggerToGetKey") == true)
    //    //{
    //    //    GameObject.Find("Body").GetComponent<Mov_Player>().HasKey = true;
    //    //}
    //}

}

