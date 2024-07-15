using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_right : MonoBehaviour
{

    public Animator anim;
    public Mov_Player PlayerHasKey;
    public AudioSource open, close;

    private void Update()
    {
        Key();
    }

    void Key()
    {

        if (PlayerHasKey.HasKey == false )
        {
            anim.enabled = false;
        }

        else
        {
            anim.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {


        Debug.Log("Player entró");
        if (other.transform.CompareTag("Player"))
        {
            anim.SetBool("IsPlayerNear", true);
            anim.SetBool("IsPlayerOut", false);
            //open.Play();
        }
    }
    private void OnTriggerStay(Collider other)
    {

        Debug.Log("Player se quedó");
        if (other.transform.CompareTag("Player"))
        {
            anim.SetBool("IsPlayerNear", true);
            anim.SetBool("IsPlayerOut", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        Debug.Log("Player se fue");
        if (other.transform.CompareTag("Player"))
        {
            anim.SetBool("IsPlayerNear", false);
            anim.SetBool("IsPlayerOut", true);
            //close.Play();
        }
    }
}
