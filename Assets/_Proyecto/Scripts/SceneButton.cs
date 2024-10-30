using UnityEngine;

public class SceneButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        // Reemplaza "SceneWithVideo" con el nombre de la escena con el video y proporciona la URL del video
        SceneTransitionManager.Instance.LoadSceneWithLoadingScreen("EscenaVideos", "https://storage.googleapis.com/rocktruck/pwc/Brazos_Automaticos.mp4");
    }
}
