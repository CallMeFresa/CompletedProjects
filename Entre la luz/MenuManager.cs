using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class MenuManager : MonoBehaviour
{
    //public GameObject StartButton;
    //public GameObject SettingsButton;
    //public GameObject CreditsButton;
    //public GameObject ExitButton;
    //public GameObject ExitButton2;

    
    public CanvasGroup canvasgroup;
   

    [Header("Volumen settings")]
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;
    public AudioMixer Mixer;
    public AudioSource click;


    public void SetVolumeMusic()
    {
        Mixer.SetFloat("BackgroundMusic", MusicSlider.value);
        PlayerPrefs.SetFloat("BackgroundMusic", MusicSlider.value);
        PlayerPrefs.Save();
        //guarda dato en player ref
    }

    public void SetMasterVolume()
    {

        Mixer.SetFloat("General", MasterSlider.value);
        PlayerPrefs.SetFloat("General", MasterSlider.value);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume()
    {

        Mixer.SetFloat("SFX", SFXSlider.value);
        PlayerPrefs.SetFloat("SFX", SFXSlider.value);
        PlayerPrefs.Save();
    }
    IEnumerator Start()
    {
        
        Time.timeScale = 1;
        
        yield return new WaitForSeconds(1);


        while (canvasgroup.alpha <= 1)
        {
            canvasgroup.alpha += Time.deltaTime /2 ;
            yield return null;
        }

        if (PlayerPrefs.HasKey("BackgroundMusic"))
        {
            MusicSlider.value = PlayerPrefs.GetFloat("BackgroundMusic");
            Mixer.SetFloat("BackgroundMusic", PlayerPrefs.GetFloat("BackgroundMusic"));
        }

        if (PlayerPrefs.HasKey("General"))
        {
            MasterSlider.value = PlayerPrefs.GetFloat("General");
            Mixer.SetFloat("General", PlayerPrefs.GetFloat("General"));
        }

        if (PlayerPrefs.HasKey("SFX"))
        {
            SFXSlider.value = PlayerPrefs.GetFloat("SFX");
            Mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX"));
        }
    }

    public void StartToCinematic()
    {
        SceneManager.LoadScene("Scene0_Cinematic1");
    }

    public void Pause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MENU");
    }

    public void GoScene2()
    {
        SceneManager.LoadScene("Scene2_Orfanato");
    }

    public void ClickSound()
    {
        click.Play();
    }
    public void DoExit()
    {
        Application.Quit();
    }
}


