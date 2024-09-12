using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideosButtons : MonoBehaviour
{
    [System.Serializable]
    public class VideoData
    {
        [HideInInspector]
        public int arrayIdx = 0; // �ndice que corresponde al tipo de video seleccionado
        [HideInInspector]
        public string[] Tipo = new string[] { "360", "180", "360 3D", "180 3D", "Standar", "Standar 3D" };

        [Header("Miniaturas y URLs")]
        [Tooltip("Miniatura del video que se mostrar� en el bot�n.")]
        public Sprite Miniaturas; // Miniatura que se ver� en el bot�n

        [Tooltip("URL que se aplica al YouTube.")]
        public string videoUrl;  // URL del video

        [Tooltip("Tag personalizado para categorizar el video.")]
        public string videoTag;  // Tag que se puede asignar a cada video
    }

    [Header("Configuraci�n del Bot�n")]
    [Tooltip("Prefab del bot�n que contendr� la miniatura y la URL.")]
    public GameObject buttonPrefab; // Prefab del bot�n que contendr� la miniatura y la URL

    [Tooltip("Contenedor donde se instanciar�n los botones.")]
    public Transform buttonContainer; // Contenedor donde se instanciar�n los botones

    [Header("Iconos Tipo Video")]
    [SerializeField] public Sprite[] Iconos; // Array de iconos para cada tipo de video

    [Space(10)]
    [Header("Lista de Videos")]
    [Tooltip("Lista que contiene las miniaturas y URLs de los videos.")]
    public List<VideoData> videoList = new List<VideoData>(); // Lista de videos (miniaturas, URLs y tipos)

    [Header("Filtros de Tipo de Video")]
    public Toggle toggle360;
    public Toggle toggle180;
    public Toggle toggle3603D;
    public Toggle toggle1803D;
    public Toggle toggleStandar;
    public Toggle toggleStandar3D;

    private string currentTag = "";  // Almacenar el tag actual que est� filtrado

    void Start()
    {
        // Agregar listeners a los toggles para que actualicen los botones al cambiar
        toggle360.onValueChanged.AddListener(delegate { FilterButtons(); });
        toggle180.onValueChanged.AddListener(delegate { FilterButtons(); });
        toggle3603D.onValueChanged.AddListener(delegate { FilterButtons(); });
        toggle1803D.onValueChanged.AddListener(delegate { FilterButtons(); });
        toggleStandar.onValueChanged.AddListener(delegate { FilterButtons(); });
        toggleStandar3D.onValueChanged.AddListener(delegate { FilterButtons(); });

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
            // Instanciar el bot�n
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);

            // Buscar el Image espec�fico del hijo llamado "Miniatura"
            Transform miniaturaTransform = newButton.transform.Find("Miniatura");
            Transform tipoVideoLogoTransform = newButton.transform.Find("Image Icon");

            // Asignar miniatura al bot�n
            if (miniaturaTransform != null)
            {
                Image miniaturaImage = miniaturaTransform.GetComponent<Image>();
                if (miniaturaImage != null)
                {
                    miniaturaImage.sprite = video.Miniaturas;
                }
            }

            // Asignar icono seg�n el tipo de video
            if (tipoVideoLogoTransform != null)
            {
                Image iconImage = tipoVideoLogoTransform.GetComponent<Image>();
                if (iconImage != null)
                {
                    // Acceder al tipo de video basado en arrayIdx y asignar el icono adecuado
                    int tipoIdx = video.arrayIdx; // Usar arrayIdx para obtener el tipo correcto
                    if (tipoIdx >= 0 && tipoIdx < Iconos.Length)
                    {
                        iconImage.sprite = Iconos[tipoIdx];
                    }
                }
            }

            // Configurar el bot�n y su funcionalidad
            Button button = newButton.GetComponent<Button>();
            string url = video.videoUrl; // Capturar la URL en una variable local para evitar problemas de cierre
            string tag = video.videoTag; // Capturar el tag del video

            button.onClick.AddListener(() => OpenVideo(url));

            // Desactivar el bot�n si el tipo de video no coincide con los toggles activos
            if (!IsTypeActive(video.arrayIdx))
            {
                newButton.SetActive(false);
            }
        }
    }

    // Funci�n que chequea si un tipo de video est� activo seg�n los toggles
    bool IsTypeActive(int tipoIdx)
    {
        switch (tipoIdx)
        {
            case 0: return toggle360.isOn;
            case 1: return toggle180.isOn;
            case 2: return toggle3603D.isOn;
            case 3: return toggle1803D.isOn;
            case 4: return toggleStandar.isOn;
            case 5: return toggleStandar3D.isOn;
            default: return false;
        }
    }

    // M�todo para actualizar los botones seg�n los filtros seleccionados
    void FilterButtons()
    {
        foreach (Transform child in buttonContainer)
        {
            // Obtener el �ndice del bot�n en el contenedor
            int index = child.GetSiblingIndex();
            if (index >= 0 && index < videoList.Count)
            {
                VideoData videoData = videoList[index];

                // Mostrar el bot�n solo si coincide el tipo y el tag actual (si hay uno)
                bool isActive = IsTypeActive(videoData.arrayIdx) && (string.IsNullOrEmpty(currentTag) || videoData.videoTag == currentTag);
                child.gameObject.SetActive(isActive);
            }
        }
    }

    // Funci�n para filtrar los botones por el tag del video
    public void FilterButtonsByTag(string tag)
    {
        currentTag = tag;  // Almacenar el tag actual
        FilterButtons();   // Llamar a la funci�n de filtrado general
    }

    void OpenVideo(string url)
    {
        
    }
}
