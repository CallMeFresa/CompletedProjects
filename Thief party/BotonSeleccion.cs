using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BotonSeleccion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject DiamanteSeleccion;

    public void OnPointerEnter(PointerEventData eventData)
    {
        DiamanteSeleccion.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DiamanteSeleccion.SetActive(false);
    }
}
