using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [Header("Imagen Banner")]
    [SerializeField] public RawImage BannerImage; // Imagen a mover
    [SerializeField] public float _x, _y; // Valor y dirección en la que se movera esa imagen

    [Header("Imagen Menu")]
    [SerializeField] private RawImage MenuImage; // Imagen a mover
    [SerializeField] private float x, y; // Valor y dirección en la que se movera esa imagen

    [Header("Imagen Menu")]
    [SerializeField] private RawImage[] ImagenTutoBackground; // Imagen a mover
    [SerializeField] private float MovX, MovY; // Valor y dirección en la que se movera esa imagen

    [Header("Salir Menu")]
    [SerializeField] private RawImage SalirImage; // Imagen a mover
    [SerializeField] private float x_, y_; // Valor y dirección en la que se movera esa imagen

    public TextMeshProUGUI nickname;
    public string EscenaJugar;

    public static GameObject BotonIniciar;

    [Header("UiTween")]
    public EasyTween BlackIn;
    public EasyTween BannerName;
    public EasyTween PanelMenu;
    public EasyTween Iniciar;
    public EasyTween Tutorial;
    public EasyTween Config;
    public EasyTween Salir;

    void Start()
    {
        BlackIn.OpenCloseObjectAnimation(); // Ejecuta el easyTween dentro de este GameObject

        BotonIniciar = GameObject.Find("Iniciar");

        // Obtener el nombre del jugador almacenado en PlayerPrefs
        string playerName = PlayerPrefs.GetString("nick");

        // Asignar el nombre del jugador a la casilla de texto
        nickname.text = playerName;

        StartCoroutine(BotonesStart());
    }

    private void Update()
    {
        MenuImage.uvRect = new Rect(MenuImage.uvRect.position + new Vector2(x, y) * Time.deltaTime, MenuImage.uvRect.size); // Mueve la imagen
        foreach (RawImage rawImage in ImagenTutoBackground)
        {
            Rect currentUVRect = rawImage.uvRect;
            currentUVRect.position += new Vector2(MovX, MovY) * Time.deltaTime;
            rawImage.uvRect = currentUVRect;
        }
        BannerImage.uvRect = new Rect(BannerImage.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, BannerImage.uvRect.size); // Mueve la imagen
        SalirImage.uvRect = new Rect(SalirImage.uvRect.position + new Vector2(x_, y_) * Time.deltaTime, SalirImage.uvRect.size); // Mueve la imagen
    }
    #region//Botones
    public void IniciarJuego()
    {
        StartCoroutine(SeleccionPersonaje());
    }

    public void VolverEscena()
    {
        SceneManager.LoadScene("MenuNombre");
    }

    public void CerrarJuego()
    {
        Application.Quit();
        Debug.Log("El juego se cerró correctamente.");
    }
    #endregion



    IEnumerator SeleccionPersonaje()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(EscenaJugar);
    }

    IEnumerator BotonesStart()
    {
        yield return new WaitForSeconds(1);

        BannerName.OpenCloseObjectAnimation();

        yield return new WaitForSeconds(0.5f);

        PanelMenu.OpenCloseObjectAnimation();
        Iniciar.OpenCloseObjectAnimation();
        Tutorial.OpenCloseObjectAnimation();
        Config.OpenCloseObjectAnimation();
        Salir.OpenCloseObjectAnimation();
    }

}
