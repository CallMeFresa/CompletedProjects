using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public EasyTween logo;
    void Start()
    {
        logo.OpenCloseObjectAnimation();
        StartCoroutine(EsperarLogo());
    }
    IEnumerator EsperarLogo()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MENU");
    }
}
