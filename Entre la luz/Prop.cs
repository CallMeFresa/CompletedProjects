using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Prop : MonoBehaviour
{

    public Mov_Player KeyConfirmation;
    public float speed;
    public GameObject KeyProp;
    public Flowchart ConfirmationToAppear;


    public void Update()
    {
        gameObject.transform.Rotate(0, speed * Time.deltaTime, 0);
        //if (ConfirmationToAppear.GetBooleanVariable("KeyIsEnabled")== true)
        //{
        //    KeyProp.SetActive(true);
        //}
       
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            KeyConfirmation.GetComponent<Mov_Player>().HasKey = true;
            Debug.Log("Recogiste la llave");
          
        }
        
    }
}
