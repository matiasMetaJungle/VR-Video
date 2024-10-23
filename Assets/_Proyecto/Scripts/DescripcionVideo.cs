using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescripcionVideo : MonoBehaviour
{
    [Header("Referenciamos el prefab de UI Descripcion")]
    public GameObject prefabUI;

    [Header("Referencia la posicion de UI Descripcion")]
    public GameObject Uiposicion;

    public string Descripcion;
    public Sprite Miniatura;

    private void Start()
    {
        Uiposicion = GameObject.Find("UI Play Video_Descripcion");
    }

    public void IntanciarUI()
    {
        
        // Borra todos los hijos de Uiposicion
        foreach (Transform child in Uiposicion.transform)
        {
            Destroy(child.gameObject);
        }

        // Instanciar el Prefab en la posición y rotación de Uiposicion y lo hace hijo de Uiposicion
        GameObject UIDescripcion = Instantiate(prefabUI, Uiposicion.transform.position, Uiposicion.transform.rotation, Uiposicion.transform);


        // Buscar todos los TextMeshProUGUI en los hijos
        TextMeshProUGUI[] textos = UIDescripcion.GetComponentsInChildren<TextMeshProUGUI>();

        // Buscar el TextMeshProUGUI con el nombre "TextoDescriptivo" y asignar la descripción
        foreach (TextMeshProUGUI texto in textos)
        {
            if (texto.name == "TextoDescripcion")
            {
                texto.text = Descripcion;
                break;
            }
        }


        // Buscar todos los TextMeshProUGUI en los hijos
        Image[] images = UIDescripcion.GetComponentsInChildren<Image>();

        // Buscar el TextMeshProUGUI con el nombre "TextoDescriptivo" y asignar la descripción
        foreach (Image image in images)
        {
            if (image.name == "Miniatura")
            {
                image.sprite = Miniatura;
                break;
            }
        }
    }


}
