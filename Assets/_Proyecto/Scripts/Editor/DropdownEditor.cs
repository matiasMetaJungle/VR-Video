using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VideosButtons))]
public class VideosButtonsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Referencia al script original
        VideosButtons script = (VideosButtons)target;

        // Dibujar el Inspector por defecto para otros campos
        DrawDefaultInspector();

        // Título de la sección personalizada
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Lista de Videos", EditorStyles.boldLabel);

        // Iterar sobre la lista de videos
        for (int i = 0; i < script.videoList.Count; i++)
        {
            VideosButtons.VideoData video = script.videoList[i];

            // Crear una caja plegable para cada elemento
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Video {i + 1}", EditorStyles.boldLabel);

            // Miniatura
            video.Miniaturas = (Sprite)EditorGUILayout.ObjectField("Miniatura", video.Miniaturas, typeof(Sprite), allowSceneObjects: false);

            // URL del video
            video.videoUrl = EditorGUILayout.TextField("URL del Video", video.videoUrl);

            // Dropdown para seleccionar el tipo de video
            video.arrayIdx = EditorGUILayout.Popup("Tipo de Video", video.arrayIdx, video.Tipo);

            EditorGUILayout.EndVertical(); // Terminar la caja
            EditorGUILayout.Space(); // Espacio entre elementos
        }

        // Aplicar los cambios si se han hecho modificaciones
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
