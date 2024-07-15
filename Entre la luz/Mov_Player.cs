using Fungus;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mov_Player : MonoBehaviour
{
    [Header("Movimiento")]
    private Rigidbody RigidBodyPlayer;
   
    public bool Run;
    public float HMov, VMov;

    public float PlayerSpeed;
    [Header ("Animacion")]
    public Animator animator;
    public SpriteRenderer sprite;

    //[Header("Flowcharts de los NPCS")]
    //public Flowchart flowchart_1;
    //public Flowchart flowchart_2;
    //public Flowchart flowchart_3;

    //[Header ("Componente Botón de los NPCS")]
    //public Button ButtonToChat_NPC1;
    //public Button ButtonToChat_NPC2;
    //public Button ButtonToChat_NPC3;

    //[Header ("Está activo el dialogo?")]
    //public bool FungusButtonState;
    //public bool FungusButtonState_2;
    //public bool FungusButtonState_3;
    //public bool FungusButtonState_4;

    [Header ("Consiguio desbloqueables")]
    public bool HasKey;
    public Flowchart GotKey;

    [Header("Está hablando con algo?")]
    public bool DialogueState;

    private void Update()
    {
        CharacterMovement();
        CharacterAnimation();
        PlayerGotKey();
        running();

        #region invocables fungus
        //FungusButtonState = flowchart_1.GetBooleanVariable("IsTalkingW_NPC1");
        //FungusButtonState_2 = flowchart_2.GetBooleanVariable("IsTalkingW_NPC2");
        //FungusButtonState_3 = flowchart_3.GetBooleanVariable("IsTalkingW_NPC3");

        //IsDialogueActive();

        //ButtonToChat_NPC1.onClick.AddListener(TaskOnClick_1);
        //ButtonToChat_NPC2.onClick.AddListener(TaskOnClick_2);
        //ButtonToChat_NPC3.onClick.AddListener(TaskOnClick_3);
        #endregion

    }

    private void CharacterMovement()
    {
        
        RigidBodyPlayer = GetComponent<Rigidbody>();

        HMov = Input.GetAxis("Horizontal") * PlayerSpeed * Time.deltaTime;
        VMov = Input.GetAxis("Vertical") * PlayerSpeed * Time.deltaTime;

        transform.Translate(HMov, 0, VMov);

    }

    public void running()
    { 
       

        if (Run == false)
        {
            PlayerSpeed = 2f;
        
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Run = true;
            PlayerSpeed = 6f;
            //float Running = PlayerSpeed * multiplier;
        }

        else
        {
            Run = false;
        }
    }

    private void CharacterAnimation()
    {
        if (HMov > 0)//Derecha
        {
            animator.SetBool("Eyn_SideWalk", true);
            animator.SetBool("Eyn_Idle", false);
            animator.SetBool("Eyn_BackWalk", false);
            animator.SetBool("Eyn_FrontWalk", false);
            sprite.flipX = true;
        }
        if (HMov < 0)//Izquierda
        {
            animator.SetBool("Eyn_SideWalk", true);
            animator.SetBool("Eyn_Idle", false);
            animator.SetBool("Eyn_BackWalk", false);
            animator.SetBool("Eyn_FrontWalk", false);
            sprite.flipX = false;
        }
        if (HMov == 0)//Idle
        {
            animator.SetBool("Eyn_SideWalk", false);
            animator.SetBool("Eyn_Idle", true);
            animator.SetBool("Eyn_BackWalk", false);
            animator.SetBool("Eyn_FrontWalk", false);
            sprite.flipX = false;
        }
        if (VMov > 0) //Caminando adelante
        {
            animator.SetBool("Eyn_SideWalk", false);
            animator.SetBool("Eyn_Idle", false);
            animator.SetBool("Eyn_BackWalk", true);
            animator.SetBool("Eyn_FrontWalk", false);
            sprite.flipX = false;
        } 
        if ( VMov < 0) // caminando atras
        {
            animator.SetBool("Eyn_SideWalk", false);
            animator.SetBool("Eyn_Idle", false);
            animator.SetBool("Eyn_BackWalk", false);
            animator.SetBool("Eyn_FrontWalk", true);
            sprite.flipX = false;
        }
    }

    public void PlayerGotKey()
    {
            if (HasKey == true)
            {
                GotKey.SetBooleanVariable("GotKey", true);
            }
    }



    //public void TaskOnClick_1()
    //{
    //    flowchart_1.SetBooleanVariable("IsTalkingW_NPC1", true);
    //    FungusButtonState = true;
    //}

    //public void TaskOnClick_2()
    //{
    //    flowchart_2.SetBooleanVariable("IsTalkingW_NPC2", true);
    //    FungusButtonState = true;
    //}

    //public void TaskOnClick_3()
    //{
    //    flowchart_3.SetBooleanVariable("IsTalkingW_NPC3", true);
    //    FungusButtonState = true;
    //}
    //public void IsDialogueActive()
    //{
    //    if (FungusButtonState == true || FungusButtonState_2 == true || FungusButtonState_3 == true)
    //    {
    //        PlayerSpeed = 0;
    //    }

    //    else if (flowchart_1.GetBooleanVariable("IsTalkingW_NPC1") == false)
    //    {
    //        FungusButtonState = false;
    //        PlayerSpeed = 2;
    //    }

    //    else if (flowchart_2.GetBooleanVariable("IsTalkingW_NPC2") == false)
    //    {
    //        FungusButtonState_2 = false;
    //        PlayerSpeed = 2;
    //    }

    //    else if (flowchart_3.GetBooleanVariable("IsTalkingW_NPC3")== false)
    //    {
    //        FungusButtonState_3 = false;
    //        PlayerSpeed = 2;
    //    }
    //}

    //public void IsDialogueActive_NPC1()
    //{

    //    if (FungusButtonState == true)
    //    {
    //        PlayerSpeed = 0;
    //    }
    //    else
    //    {

    //        flowchart_1.SetBooleanVariable("IsTalkingW_NPC1", false);  
    //        FungusButtonState = false;

    //    }
    //}

    //public void IsDialogueActive_NPC2()
    //{
    //    if (FungusButtonState_2 == true)
    //    {
    //        PlayerSpeed = 0;
    //    }
    //    else
    //    {
    //        FungusButtonState_2 = flowchart_2.GetBooleanVariable("IsTalkingW_NPC2");
    //        flowchart_2.SetBooleanVariable("IsTalkingW_NPC2", false);
    //        FungusButtonState_2 = false;

    //    }
    //}




}
