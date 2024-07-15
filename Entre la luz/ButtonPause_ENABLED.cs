using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPause_ENABLED : MonoBehaviour
{
    [Header ("Flowcharts donde saca las variables")]
    public Flowchart ISA;
    public Flowchart LIRIA;
    public Flowchart ENOKI;
    public Flowchart ANGUS;
    public Flowchart PROPS;

    public bool NPC1, NPC2, NPC3, NPC4, PROP;

    [Header ("Boton que inhabilita cada vez que hablan")]
    public GameObject Button_Pause;
    public void Update()
    {
        NPC1 = ISA.GetBooleanVariable("IsTalkingW_NPC1");
        NPC2 = LIRIA.GetBooleanVariable("IsTalkingW_NPC2");
        NPC3 = ENOKI.GetBooleanVariable("IsTalkingW_NPC3");
        NPC4 = ANGUS.GetBooleanVariable("IsTalkingW_NPC4");
        PROP = PROPS.GetBooleanVariable("TalkingToProp");
    
        VerifyDialogueState();
    }
    public void VerifyDialogueState()
    {
        if (NPC1 || NPC2 || NPC3 || NPC4 || PROP)
        {
            Button_Pause.SetActive(false);
        }
        else
        {
            Button_Pause.SetActive(true);
        }
    }
}
