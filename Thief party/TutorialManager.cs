using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    int tuto = 0;

    public int Ultimotuto = 0;

    public GameObject Marcador;

    public Transform[] Pos;

    private void Start()
    {
        Dictionary<Transform, int> posicionesNumeradas = new Dictionary<Transform, int>();
        for (int i = 0; i < Pos.Length; i++)
        {
            posicionesNumeradas.Add(Pos[i], i);
        }
    }

    public void DerechaTutos()
    {
        GameObject.Find("Canvas/Menu/PanelTutorial/Tutoriales").transform.GetChild(tuto).gameObject.SetActive(false);
        tuto += 1;
        GameObject.Find("Canvas/Menu/PanelTutorial/Tutoriales").transform.GetChild(tuto).gameObject.SetActive(true);
    }

    public void IzquierdaTutos()
    {
        GameObject.Find("Canvas/Menu/PanelTutorial/Tutoriales").transform.GetChild(tuto).gameObject.SetActive(false);
        tuto -= 1;
        GameObject.Find("Canvas/Menu/PanelTutorial/Tutoriales").transform.GetChild(tuto).gameObject.SetActive(true);
    }


    private void Update()
    {
        if (tuto == Ultimotuto)
        {
            GameObject.Find("Canvas/Menu/PanelTutorial").transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("Canvas/Menu/PanelTutorial").transform.GetChild(1).gameObject.SetActive(true);
        }

        if (tuto == 0)
        {
            GameObject.Find("Canvas/Menu/PanelTutorial").transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("Canvas/Menu/PanelTutorial").transform.GetChild(2).gameObject.SetActive(true);
        }

        if (tuto >= 0 && tuto < Pos.Length)
        {
            Marcador.transform.position = Pos[tuto].position;
        }
    }
}
