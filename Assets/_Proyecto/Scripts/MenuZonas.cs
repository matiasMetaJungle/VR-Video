using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization.Components;

[AddComponentMenu("MenuZonas (NO TOCAR ESTE SCRIPT SE CONTROLA DESDE EL BOTON)")]
public class MenuZonas : MonoBehaviour
{
    [Header("Referencia el image donde se cambiara la imagen")]
    public Image ImagenRegion;

    [Header("Referencia el Textmeshpro donde cambiara el nombre")]
    public TextMeshProUGUI NombreRegion;

    [Header("Prefab de la descripcion del texto")]
    public GameObject PrefabText;

    [Header("Referencia donde deberia instanciar el texto")]
    public Transform TextPosition;

    [Header("Referencia donde deberia instanciar los botones")]
    public Transform BotonesPosition;

    [Header("Referencia del localization")]
    public LocalizeStringEvent Localizestringevent;

    private GameObject textoInstanciado;

    public void CambiarNombre(string nombre)
    {
        // Cambia el nombre 
        NombreRegion.text = nombre;
    }

    public void CambiarImagen(Sprite imagen)
    {
        ImagenRegion.sprite = imagen;
    }

    public void cambiarDescripcion(GameObject prefab)
    {
        PrefabText = prefab;

        // Borra todos los hijos de TextPosition
        foreach (Transform child in TextPosition)
        {
            Destroy(child.gameObject);
        }

        // Instanciar el Prefab en la posición y rotación de TextPosition y lo hace hijo de TextPosition
        GameObject nuevoTexto = Instantiate(PrefabText, TextPosition.position, TextPosition.rotation, TextPosition);
        textoInstanciado = nuevoTexto;
    }

    public void CambiarBotonesVideos(ContenedorVideos contenedorVideos)
    {
        // Borra todos los hijos de BotonesPosition antes de instanciar nuevos botones
        foreach (Transform child in BotonesPosition)
        {
            Destroy(child.gameObject);
        }

        // Recorre el array de botones en el contenedorVideos y los instancia con la imagen y descripción correspondientes
        for (int i = 0; i < contenedorVideos.botonesPrefabs.Length; i++)
        {
            // Instancia el prefab del botón en la posición de BotonesPosition y lo hace hijo de BotonesPosition
            GameObject nuevoBoton = Instantiate(contenedorVideos.botonesPrefabs[i], BotonesPosition.position, BotonesPosition.rotation, BotonesPosition);

            // Asigna la imagen correspondiente al botón si existe un componente Image
            Image botonImagen = nuevoBoton.GetComponent<Image>();
            if (botonImagen != null && i < contenedorVideos.imagenesBotones.Length)
            {
                botonImagen.sprite = contenedorVideos.imagenesBotones[i];
            }

            // Asigna la descripción correspondiente si existe un componente TextMeshProUGUI
            TextMeshProUGUI botonTexto = nuevoBoton.GetComponentInChildren<TextMeshProUGUI>();
            if (botonTexto != null && i < contenedorVideos.DescripcionesBotones.Length)
            {
                botonTexto.text = contenedorVideos.DescripcionesBotones[i];
            }

            // Asigna la descripción y miniatura al script DescripcionVideo del botón
            DescripcionVideo descripcionVideo = nuevoBoton.GetComponent<DescripcionVideo>();
            if (descripcionVideo != null && i < contenedorVideos.DescripcionesBotones.Length && i < contenedorVideos.imagenesBotones.Length)
            {
                descripcionVideo.Descripcion = contenedorVideos.DescripcionesBotones[i];
                descripcionVideo.Miniatura = contenedorVideos.imagenesBotones[i];
            }
        }
    }


    public void CambiarLocalization(string nuevaClave)
    {
        // Cambiar la clave de LocalizeStringEvent en el nuevo texto
        LocalizeStringEvent nuevoLocalizeStringEvent = textoInstanciado.GetComponent<LocalizeStringEvent>();
        if (nuevoLocalizeStringEvent != null)
        {
            // Cambia la clave de entrada de localización
            nuevoLocalizeStringEvent.StringReference.SetReference("Tabla1", nuevaClave);
            // Fuerza la actualización del texto localizado
            nuevoLocalizeStringEvent.RefreshString();
        }
    }
}
