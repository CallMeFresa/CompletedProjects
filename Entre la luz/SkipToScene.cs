using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipToScene : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(CinematicToScene());
    }
    public void SkipCinematic()
    {
        SceneManager.LoadScene("Scene1_StartAndIntroduction");
    }

    IEnumerator CinematicToScene()
    {
        yield return new WaitForSeconds(32);
        SceneManager.LoadScene("Scene1_StartAndIntroduction");
    }
}
