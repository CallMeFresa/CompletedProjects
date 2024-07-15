using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPause_sce1ENABLED : MonoBehaviour
{

    [Header("Flowcharts donde saca las variables")]
    public Flowchart START;
    public Flowchart PROP1;
    public Flowchart PROP2;
    public Flowchart PROP3;


    public bool start, prop1, prop2, prop3;

    [Header("Boton que inhabilita cada vez que hablan")]
    public GameObject Button_Pause;
    public void Update()
    {
        start = START.GetBooleanVariable("DidDialogueStart");
        prop1 = PROP1.GetBooleanVariable("IsTalkingWithPROP");
        prop2 = PROP2.GetBooleanVariable("IsTalkingWithPROP");
        prop3 = PROP3.GetBooleanVariable("IsTalkingWithPROP");
       

        VerifyDialogueState();
    }
    public void VerifyDialogueState()
    {
        if (start || prop1 || prop2 || prop3)
        {
            Button_Pause.SetActive(false);
        }
        else
        {
            Button_Pause.SetActive(true);
        }
    }
}
