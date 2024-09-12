using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VideosButtons))]
public class VideosButtonsEditor : Editor
{
    SerializedProperty videoListProp;
    SerializedProperty buttonPrefabProp;
    SerializedProperty buttonContainerProp;
    SerializedProperty iconosProp;

    // Añadir los toggles de tipo de video
    SerializedProperty toggle360Prop;
    SerializedProperty toggle180Prop;
    SerializedProperty toggle3603DProp;
    SerializedProperty toggle1803DProp;
    SerializedProperty toggleStandarProp;
    SerializedProperty toggleStandar3DProp;

    // Definir el array de tipos de video como constante
    private static readonly string[] videoTypes = { "360", "180", "360 3D", "180 3D", "Standar", "Standar 3D" };

    private void OnEnable()
    {
        // Asignamos las propiedades serializadas
        videoListProp = serializedObject.FindProperty("videoList");
        buttonPrefabProp = serializedObject.FindProperty("buttonPrefab");
        buttonContainerProp = serializedObject.FindProperty("buttonContainer");
        iconosProp = serializedObject.FindProperty("Iconos");

        // Asignar los toggles de tipo de video
        toggle360Prop = serializedObject.FindProperty("toggle360");
        toggle180Prop = serializedObject.FindProperty("toggle180");
        toggle3603DProp = serializedObject.FindProperty("toggle3603D");
        toggle1803DProp = serializedObject.FindProperty("toggle1803D");
        toggleStandarProp = serializedObject.FindProperty("toggleStandar");
        toggleStandar3DProp = serializedObject.FindProperty("toggleStandar3D");
    }

    public override void OnInspectorGUI()
    {
        // Actualizar el objeto serializado
        serializedObject.Update();

        // Dibujar las propiedades del botón
        EditorGUILayout.PropertyField(buttonPrefabProp, new GUIContent("Button Prefab"));
        EditorGUILayout.PropertyField(buttonContainerProp, new GUIContent("Button Container"));
        EditorGUILayout.PropertyField(iconosProp, new GUIContent("Iconos Tipo Video"));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Filtros de Tipo de Video", EditorStyles.boldLabel);

        // Dibujar los toggles para cada tipo de video
        EditorGUILayout.PropertyField(toggle360Prop, new GUIContent("360 Toggle"));
        EditorGUILayout.PropertyField(toggle180Prop, new GUIContent("180 Toggle"));
        EditorGUILayout.PropertyField(toggle3603DProp, new GUIContent("360 3D Toggle"));
        EditorGUILayout.PropertyField(toggle1803DProp, new GUIContent("180 3D Toggle"));
        EditorGUILayout.PropertyField(toggleStandarProp, new GUIContent("Standar Toggle"));
        EditorGUILayout.PropertyField(toggleStandar3DProp, new GUIContent("Standar 3D Toggle"));

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
            SerializedProperty videoTag = video.FindPropertyRelative("videoTag");
            SerializedProperty arrayIdx = video.FindPropertyRelative("arrayIdx");

            // Crear una caja plegable para cada elemento
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Video {i + 1}", EditorStyles.boldLabel);

            // Miniatura
            EditorGUILayout.PropertyField(miniaturas, new GUIContent("Miniatura"));

            // URL del video
            EditorGUILayout.PropertyField(videoUrl, new GUIContent("URL del Video"));

            // Tag del video
            EditorGUILayout.PropertyField(videoTag, new GUIContent("Tag del Video"));

            // Dropdown para seleccionar el tipo de video
            arrayIdx.intValue = EditorGUILayout.Popup("Tipo de Video", arrayIdx.intValue, videoTypes);

            // Botón para eliminar el video
            if (GUILayout.Button("Eliminar Video"))
            {
                videoListProp.DeleteArrayElementAtIndex(i);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        // Aplicar los cambios si se han hecho modificaciones
        serializedObject.ApplyModifiedProperties();
    }
}
