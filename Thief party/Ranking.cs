using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using TMPro;
using System.Linq;

public class Ranking : MonoBehaviour
{
    public GameObject TarRank;
    public GameObject TarRank2;
    public GameObject ContenedorRank;
    public GameObject ContenedorRank2;

    public void GetUserScore()
    {
        foreach (Transform child in ContenedorRank.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            GameObject newRank = Instantiate(TarRank);
            newRank.transform.SetParent(ContenedorRank.transform, false);

            newRank.transform.name = "00";
            newRank.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[i].GetComponent<PhotonView>().Owner.NickName;
        }

        foreach (Transform child in ContenedorRank2.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            GameObject newRank = Instantiate(TarRank2);
            newRank.transform.SetParent(ContenedorRank2.transform, false);

            newRank.transform.name = "00";
            newRank.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = players[i].GetComponent<PhotonView>().Owner.NickName;
        }
    }

    public void UpdateUserScore(string nickname, int Puntos)
    {
        for (int i = 0; i < ContenedorRank.transform.childCount; i++)
        {
            if (ContenedorRank.transform.GetChild(i).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text == nickname)
            {
                ContenedorRank.transform.GetChild(i).transform.name = Puntos.ToString("00");
                ContenedorRank.transform.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Puntos.ToString();
            }
        }
        ContenedorRank.GetComponent<SortChildren>().SortChildrenByName();

        for (int i = 0; i < ContenedorRank2.transform.childCount; i++)
        {
            if (ContenedorRank2.transform.GetChild(i).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text == nickname)
            {
                ContenedorRank2.transform.GetChild(i).transform.name = Puntos.ToString("00");
                ContenedorRank2.transform.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = Puntos.ToString();
            }
        }
        ContenedorRank2.GetComponent<SortChildren>().SortChildrenByName();

        //for (int i = 0; i < ContenedorRank.transform.childCount; i++)
        //{
        //    ContenedorRank.transform.GetChild(i).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
        //}
    }
}
