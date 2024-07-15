using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class Data : MonoBehaviour
{
    TMP_InputField Nombre;
    int MonedasUltimaPartida;
    int MonedasTotales;

    PlayerMovement playerContador;

    private void Start()
    {
        playerContador = GetComponent<PlayerMovement>();
    }

    public void send()
    {
        if (Nombre.text == "")
        { 
        
        }
        else
        {
            StartCoroutine(Upload());
        }
    }

    IEnumerator Upload()
    {
        int Suma = playerContador.contador + MonedasTotales;

        WWWForm form = new WWWForm();
        form.AddField("entry.148617324", PlayerPrefs.GetString("nick"));
        form.AddField("entry.229224923", playerContador.contador);
        form.AddField("entry.2129744939", Suma);

        UnityWebRequest www = UnityWebRequest.Post("https://docs.google.com/forms/d/e/1FAIpQLSfYSJVtrF0-6YrXhbhgvTIzmyvtip8qwschg_HYeDEwQwAELg/viewform", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}
