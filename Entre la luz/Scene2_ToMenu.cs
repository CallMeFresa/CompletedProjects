using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2_ToMenu : MonoBehaviour
{

    public void Start()
    {
        StartCoroutine(CinematicToScene());
    }
    IEnumerator CinematicToScene()
    {
        yield return new WaitForSeconds(57);
        SceneManager.LoadScene("MENU");
    }
}

