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

        [Header("Miniaturas, URLs y Tipo de video")]
        [Tooltip("Miniatura del video que se mostrar� en el bot�n.")]
        public Sprite Miniaturas; // Miniatura que se ver� en el bot�n

        [Tooltip("URL que se aplica al youtube.")]
        public string videoUrl;  // URL del video
    }

    [Header("Configuraci�n del Bot�n")]
    [Tooltip("Prefab del bot�n que contendr� la miniatura y la URL.")]
    public GameObject buttonPrefab; // Prefab del bot�n que contendr� la miniatura y la URL

    [Tooltip("Contenedor donde se instanciar�n los botones.")]
    public Transform buttonContainer; // Contenedor donde se instanciar�n los botones

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
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            newButton.GetComponentInChildren<Image>().sprite = video.Miniaturas;

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
