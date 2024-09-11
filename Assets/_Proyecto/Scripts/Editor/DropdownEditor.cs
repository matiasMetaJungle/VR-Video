using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VideosButtons))]
public class VideosButtonsEditor : Editor
{
    SerializedProperty videoListProp;
    SerializedProperty buttonPrefabProp;
    SerializedProperty buttonContainerProp;

    // Definir el array de tipos de video como constante
    private static readonly string[] videoTypes = { "360", "180", "360 3D", "180 3D", "Standar", "Standar 3D" };

    private void OnEnable()
    {
        // Asignamos las propiedades serializadas
        videoListProp = serializedObject.FindProperty("videoList");
        buttonPrefabProp = serializedObject.FindProperty("buttonPrefab");
        buttonContainerProp = serializedObject.FindProperty("buttonContainer");
    }

    public override void OnInspectorGUI()
    {
        // Actualizar el objeto serializado
        serializedObject.Update();

        // Dibujar las propiedades del botón
        EditorGUILayout.PropertyField(buttonPrefabProp, new GUIContent("Button Prefab"));
        EditorGUILayout.PropertyField(buttonContainerProp, new GUIContent("Button Container"));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Lista de Videos", EditorStyles.boldLabel);

        // Botón para añadir nuevos elementos a la lista
        if (GUILayout.Button("Añadir Video"))
        {
            videoListProp.InsertArrayElementAtIndex(videoListProp.arraySize);
        }

        // Iterar sobre la lista de videos
        for (int i = 0; i < videoListProp.arraySize; i++)
        {
            SerializedProperty video = videoListProp.GetArrayElementAtIndex(i);

            SerializedProperty miniaturas = video.FindPropertyRelative("Miniaturas");
            SerializedProperty videoUrl = video.FindPropertyRelative("videoUrl");
            SerializedProperty arrayIdx = video.FindPropertyRelative("arrayIdx");

            // Crear una caja plegable para cada elemento
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Video {i + 1}", EditorStyles.boldLabel);

            // Miniatura
            EditorGUILayout.PropertyField(miniaturas, new GUIContent("Miniatura"));

            // URL del video
            EditorGUILayout.PropertyField(videoUrl, new GUIContent("URL del Video"));

            // Dropdown para seleccionar el tipo de video
            arrayIdx.intValue = EditorGUILayout.Popup("Tipo de Video", arrayIdx.intValue, videoTypes);

            // Botón para eliminar el video
            if (GUILayout.Button("Eliminar Video"))
            {
                videoListProp.DeleteArrayElementAtIndex(i);
            }

            EditorGUILayout.EndVertical(); // Terminar la caja
            EditorGUILayout.Space(); // Espacio entre elementos
        }

        // Aplicar los cambios si se han hecho modificaciones
        serializedObject.ApplyModifiedProperties();
    }
}
