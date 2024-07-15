using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class UI_Game : MonoBehaviour
{
    public GameObject BotonVolver;

    [Header("Imagen Banner")]
    [SerializeField] public RawImage BannerImageInferior, BannerImageSuperior, RankFondo; // Imagen a mover
    [SerializeField] public float _x, _y; // Valor y dirección en la que se movera esa imagen

    [Header("Imagen Menu")]
    [SerializeField] public RawImage MenuImage, Cargando; // Imagen a mover
    [SerializeField] public float x, y; // Valor y dirección en la que se movera esa imagen

    [Header("Imagen Menu")]
    [SerializeField] public RawImage ConfigImagen; // Imagen a mover
    [SerializeField] public float x_, y_; // Valor y dirección en la que se movera esa imagen

    public bool Pausado = false;
    public bool CanMove = true;
    public bool AsegurandoMonedas = false;
    public TextMeshProUGUI nickname;

    [Header("EasyTween")]
    public EasyTween Pausa_Banner_Superior;
    public EasyTween Pausa_Banner_Inferior;
    public EasyTween Pausa_Botones;
    public EasyTween Pausa_Background;

    [Header("PowerUps")]
    public EasyTween Velocidad;
    public EasyTween X2;
    public EasyTween Invisibilidad;
    public EasyTween Iman;
    public EasyTween Lentitud;
    public EasyTween Desmayo;
    public EasyTween Borracho;

    [Header("Sonidos")]
    public AudioSource Boton;

    private void Start()
    {
        // Obtener el nombre del jugador almacenado en PlayerPrefs
        string playerName = PlayerPrefs.GetString("nick");

        // Asignar el nombre del jugador a la casilla de texto
        nickname.text = playerName;

        StartCoroutine(VolverMenu());
    }

    IEnumerator VolverMenu()
    {
        yield return new WaitForSeconds(15);
        BotonVolver.SetActive(true);
    }

    private void Update()
    {
        MenuImage.uvRect = new Rect(MenuImage.uvRect.position + new Vector2(x, y) * Time.deltaTime, MenuImage.uvRect.size); // Mueve la imagen
        Cargando.uvRect = new Rect(Cargando.uvRect.position + new Vector2(x, y) * Time.deltaTime, Cargando.uvRect.size); // Mueve la imagen
        ConfigImagen.uvRect = new Rect(ConfigImagen.uvRect.position + new Vector2(x_, y_) * Time.deltaTime, ConfigImagen.uvRect.size); // Mueve la imagen
        BannerImageInferior.uvRect = new Rect(BannerImageInferior.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, BannerImageInferior.uvRect.size); // Mueve la imagen
        BannerImageSuperior.uvRect = new Rect(BannerImageSuperior.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, BannerImageSuperior.uvRect.size); // Mueve la imagen
        RankFondo.uvRect = new Rect(RankFondo.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, RankFondo.uvRect.size); // Mueve la imagen

        if (AsegurandoMonedas == true)
        {
            if (Pausado == false)
            {
                CanMove = false;
            }
        }
        else if (AsegurandoMonedas == false)
        {
            if (Pausado == false)
            {
                CanMove = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && Pausado == false)
        {
            Pausa_Banner_Superior.OpenCloseObjectAnimation();
            Pausa_Banner_Inferior.OpenCloseObjectAnimation();
            Pausa_Botones.OpenCloseObjectAnimation();
            Pausa_Background.OpenCloseObjectAnimation();
            Boton.Play();
            CanMove = false;
            Pausado = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Pausado == true)
        {
            Pausa_Banner_Superior.OpenCloseObjectAnimation();
            Pausa_Banner_Inferior.OpenCloseObjectAnimation();
            Pausa_Botones.OpenCloseObjectAnimation();
            Pausa_Background.OpenCloseObjectAnimation();
            Boton.Play();
            CanMove = true;
            Pausado = false;
        }
    }

    public void QuitarPausa()
    {
        Pausado = false;
        CanMove = true;
    }
}
