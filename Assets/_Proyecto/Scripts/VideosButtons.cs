using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideosButtons : MonoBehaviour
{
    [System.Serializable]
    public class VideoData
    {
        [HideInInspector]
        public int arrayIdx = 0;
        [HideInInspector]
        public string[] Tipo = new string[] { "360", "180", "360 3D", "180 3D", "Standar", "Standar 3D" };

        [Header("Miniaturas y URLs")]
        [Tooltip("Miniatura del video que se mostrará en el botón.")]
        public Sprite Miniaturas; // Miniatura que se verá en el botón

        [Tooltip("URL que se aplica al youtube.")]
        public string videoUrl;  // URL del video
    }

    [Header("Configuración del Botón")]
    [Tooltip("Prefab del botón que contendrá la miniatura y la URL.")]
    public GameObject buttonPrefab; // Prefab del botón que contendrá la miniatura y la URL

    [Tooltip("Contenedor donde se instanciarán los botones.")]
    public Transform buttonContainer; // Contenedor donde se instanciarán los botones

    [Header("Iconos Tipo Video")]
    [SerializeField] public Sprite[] Iconos;

    [Space(10)]
    [Header("Lista de Videos")]
    [Tooltip("Lista que contiene las miniaturas y URLs de los videos.")]
    public List<VideoData> videoList = new List<VideoData>(); // Lista de videos (miniaturas y URLs)

    void Start()
    {
        GenerateButtons();
    }

    void GenerateButtons()
    {
        // Limpiar el contenedor de botones existentes, si los hubiera
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // Instanciar los botones de acuerdo a la cantidad de videos en la lista
        foreach (var video in videoList)
        {
            // Instanciar el botón
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);

            // Buscar el Image específico del hijo llamado "Miniatura"
            Transform miniaturaTransform = newButton.transform.Find("Miniatura");
            Transform TipoVideoLogo = newButton.transform.Find("Image Icon");

            if (miniaturaTransform != null)
            {
                Image miniaturaImage = miniaturaTransform.GetComponent<Image>();
                if (miniaturaImage != null)
                {
                    miniaturaImage.sprite = video.Miniaturas;
                }
            }
            else
            {
                Debug.LogWarning("El hijo 'Miniatura' no se encontró en el botón.");
            }

            if (TipoVideoLogo != null)
            {
                Image iconImage = TipoVideoLogo.GetComponent<Image>();
                if (iconImage != null)
                {
                    // Asegúrate de que Tipo tenga al menos un elemento
                    if (video.Tipo.Length > 0)
                    {
                        string tipoVideo = video.Tipo[0]; // Usar el primer elemento del arreglo Tipo
                        switch (tipoVideo)
                        {
                            case "360":
                                iconImage.sprite = Iconos[0];
                                break;
                            case "180":
                                iconImage.sprite = Iconos[1];
                                break;
                            case "360 3D":
                                iconImage.sprite = Iconos[2];
                                break;
                            case "180 3D":
                                iconImage.sprite = Iconos[3];
                                break;
                            case "Standar":
                                iconImage.sprite = Iconos[4];
                                break;
                            case "Standar 3D":
                                iconImage.sprite = Iconos[5];
                                break;
                            default:
                                Debug.LogWarning($"Tipo de video desconocido: {tipoVideo}");
                                break;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("El arreglo Tipo está vacío en el video.");
                    }
                }
                else
                {
                    Debug.LogWarning("El componente Image no se encontró en 'Image Icon'.");
                }
            }

            // Configurar el botón y su funcionalidad
            Button button = newButton.GetComponent<Button>();
            string url = video.videoUrl; // Capturar la URL en una variable local para evitar problemas de cierre

            button.onClick.AddListener(() => OpenVideo(url));
        }
    }





    void OpenVideo(string url)
    {
        //Aqui le pasamos la URL al Youtube
    }
}
