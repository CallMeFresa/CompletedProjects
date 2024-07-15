using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashScreem : MonoBehaviour
{
    //public VideoClip clip;
    void Start()
    {
        StartCoroutine(EsperarLogo());
    }
    IEnumerator EsperarLogo()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Loby");
    }
}
