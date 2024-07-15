using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_left : MonoBehaviour
{
    public Animator anim;
    public AudioSource open, close;
    

    private void OnTriggerEnter (Collider other)
    {
       

        Debug.Log("Player entró");
        if (other.transform.CompareTag("Player"))
        {
            anim.SetBool("IsPlayerNear", true);
            anim.SetBool("IsPlayerOut", false);
            
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
            
        }

        
    }
}
