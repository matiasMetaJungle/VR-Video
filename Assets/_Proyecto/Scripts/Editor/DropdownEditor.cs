using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

    private Dictionary<string, bool> foldoutStates = new Dictionary<string, bool>();

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
            int index = videoListProp.arraySize;
            videoListProp.InsertArrayElementAtIndex(index);
            SerializedProperty newVideo = videoListProp.GetArrayElementAtIndex(index);

            // Inicializar videoTag a vacío
            SerializedProperty videoTag = newVideo.FindPropertyRelative("videoTag");
            videoTag.stringValue = string.Empty;

            // Inicializar Miniaturas a null
            SerializedProperty miniaturas = newVideo.FindPropertyRelative("Miniaturas");
            miniaturas.objectReferenceValue = null;
        }

        // Agrupar y visualizar videos por etiqueta
        var groupedVideos = GroupVideosByTag();

        foreach (var group in groupedVideos)
        {
            if (!foldoutStates.ContainsKey(group.Key))
            {
                foldoutStates[group.Key] = true; // Estado inicial del foldout
            }

            foldoutStates[group.Key] = EditorGUILayout.Foldout(foldoutStates[group.Key], group.Key);

            if (foldoutStates[group.Key])
            {
                // Crear una lista temporal de elementos para evitar problemas al eliminar elementos mientras se itera
                List<int> indicesToRemove = new List<int>();

                for (int i = 0; i < group.Value.Count; i++)
                {
                    var video = group.Value[i];
                    SerializedProperty videoProp = videoListProp.GetArrayElementAtIndex(video.Index);
                    if (videoProp == null) continue; // Verificar que el elemento no sea nulo

                    SerializedProperty miniaturas = videoProp.FindPropertyRelative("Miniaturas");
                    SerializedProperty videoUrl = videoProp.FindPropertyRelative("videoUrl");
                    SerializedProperty videoTag = videoProp.FindPropertyRelative("videoTag");
                    SerializedProperty arrayIdx = videoProp.FindPropertyRelative("arrayIdx");

                    EditorGUILayout.BeginVertical("box");
                    EditorGUILayout.LabelField($"Video {video.Index + 1}", EditorStyles.boldLabel);

                    // Miniatura
                    Sprite miniaturaSprite = miniaturas.objectReferenceValue as Sprite;
                    if (miniaturaSprite != null)
                    {
                        // Mostrar una vista previa de la miniatura
                        Rect rect = EditorGUILayout.GetControlRect(GUILayout.Height(100));
                        EditorGUI.DrawPreviewTexture(rect, miniaturaSprite.texture, null, ScaleMode.ScaleToFit);

                        // Permitir reemplazo de la miniatura
                        EditorGUILayout.PropertyField(miniaturas, new GUIContent("Miniatura (Opcional)"));
                    }
                    else
                    {
                        // Permitir asignar una nueva miniatura
                        EditorGUILayout.PropertyField(miniaturas, new GUIContent("Miniatura (Opcional)"));
                    }

                    // URL del video
                    EditorGUILayout.PropertyField(videoUrl, new GUIContent("URL del Video"));

                    // Tag del video
                    EditorGUILayout.PropertyField(videoTag, new GUIContent("Tag del Video"));

                    // Dropdown para seleccionar el tipo de video
                    arrayIdx.intValue = EditorGUILayout.Popup("Tipo de Video", arrayIdx.intValue, videoTypes);

                    // Botón para eliminar el video
                    if (GUILayout.Button("Eliminar Video"))
                    {
                        indicesToRemove.Add(video.Index);
                    }

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space();
                }

                // Eliminar los elementos en orden descendente para evitar problemas de índice
                foreach (var index in indicesToRemove.OrderByDescending(i => i))
                {
                    videoListProp.DeleteArrayElementAtIndex(index);
                }
            }
        }

        // Aplicar los cambios si se han hecho modificaciones
        serializedObject.ApplyModifiedProperties();
    }



    private Dictionary<string, List<VideoItem>> GroupVideosByTag()
    {
        var groups = new Dictionary<string, List<VideoItem>>();

        for (int i = 0; i < videoListProp.arraySize; i++)
        {
            SerializedProperty videoProp = videoListProp.GetArrayElementAtIndex(i);
            SerializedProperty videoTag = videoProp.FindPropertyRelative("videoTag");
            string tag = videoTag.stringValue;

            if (!groups.ContainsKey(tag))
            {
                groups[tag] = new List<VideoItem>();
            }

            groups[tag].Add(new VideoItem { Index = i });
        }

        return groups;
    }

    private class VideoItem
    {
        public int Index { get; set; }
    }
}
