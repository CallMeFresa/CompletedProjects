using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtomAppear : MonoBehaviour
{
    [SerializeField] public GameObject CustomButton;


    public void Start()
    {

        CustomButton.SetActive(false);


        //CustomButton.SetActive = false;
        //CustomImage.enabled = false;
        //CustomUIText.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CustomButton.SetActive(true);
            

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CustomButton.SetActive(false);
          
        }
    }
}
