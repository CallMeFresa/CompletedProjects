using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Select_PJ : MonoBehaviour
{
    public GameObject[] PJs;

    public string NombrePJ;

    [Header("EasyTween")]
    public EasyTween Panel__Info;

    [Header("iTween")]
    public GameObject Cam;
    public Transform Final_Pos;
    public Transform Select_Pos;

    [Header("Materiales")]
    public Material Shader_Select; // Material de selección
    public Material Original_Mat; //Material Original
    public GameObject MeshMaterial;

    [Header("Sonido")]
    public AudioSource SeleccionPj;

    private void Start()
    {
        StartCoroutine(ActivarColliders());
    }

    

    private void OnMouseDown() //Si el clic se hace al gameObject.
    {
        iTween.MoveTo(Cam, iTween.Hash("position", Final_Pos, "Time", 2));
        iTween.RotateTo(Cam, iTween.Hash("rotation", Final_Pos, "Time", 2));
        Panel__Info.OpenCloseObjectAnimation();
        SeleccionPj.Play();

        foreach (GameObject Person in PJs) // Convierte la matris de personajes en un GameObject que se puede usar como GameObject normal
        {
            Person.GetComponent<CapsuleCollider>().enabled = false; // Deshabilita los colliders de los personajes.
        }

    }

    private void OnMouseEnter()
    {
        MeshMaterial.GetComponent<SkinnedMeshRenderer>().material = Shader_Select; // Cambia al material de seleccion cuando el mouse se pone por encima del personaje.
    }

    private void OnMouseExit()
    {
        MeshMaterial.GetComponent<SkinnedMeshRenderer>().material = Original_Mat; // Cambia al material original cuando el mouse ya no esta por encima del perosnaje.
    }

    public void Deseleccionar_PJ()
    {
        MeshMaterial.GetComponent<SkinnedMeshRenderer>().material = Original_Mat;
        iTween.MoveTo(Cam, iTween.Hash("position", Select_Pos, "Time", 2));
        iTween.RotateTo(Cam, iTween.Hash("rotation", Select_Pos, "Time", 2));
        Panel__Info.OpenCloseObjectAnimation();

        foreach (GameObject Person in PJs) // Convierte la matris de personajes en un GameObject que se puede usar como GameObject normal
        {
            Person.GetComponent<CapsuleCollider>().enabled = true; // Activa los colliders de los personajes para poder seleccionar nuevamente.
        }
    }

    public void Jugar()
    {
        PlayerPrefs.SetString("Personaje", NombrePJ);
        PlayerPrefs.Save();
        PhotonNetwork.LoadLevel("Game");
    }

    IEnumerator ActivarColliders()
    {
        yield return new WaitForSeconds(14);

        foreach (GameObject Person in PJs) // Convierte la matris de personajes en un GameObject que se puede usar como GameObject normal
        {
            Person.GetComponent<CapsuleCollider>().enabled = true;
        }
    }

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0)) // Detecta clic izquierdo del mouse
    //    {
    //        // Lanza un rayo desde la posición del mouse
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        // Si el rayo no golpea el collider de este objeto, significa que el clic está fuera del gameObject
    //        if (Physics.Raycast(ray, out hit) || hit.collider.gameObject == gameObject)
    //        {
    //            GetComponent<Renderer>().material = Shader_Select;
    //            iTween.MoveTo(Cam, iTween.Hash("position", Final_Pos, "Time", 2));
    //            iTween.RotateTo(Cam, iTween.Hash("rotation", Final_Pos, "Time", 2));
    //            Panel__Info.OpenCloseObjectAnimation();
    //            gameObject.GetComponent<Collider>().enabled = false;
    //        }
    //    }
    //}
}
