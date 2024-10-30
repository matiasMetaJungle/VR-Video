using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    public CanvasGroup fadeCanvasGroup; // Para el efecto de fundido
    public float fadeDuration = 2f; // Duración del fundido

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener el manager entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneWithLoadingScreen(string sceneName, string videoUrl)
    {
        StartCoroutine(LoadSceneAsync(sceneName, videoUrl));
    }

    private IEnumerator LoadSceneAsync(string sceneName, string videoUrl)
    {
        // Iniciar el fundido de entrada
        yield return StartCoroutine(Fade(1f));

        // Cargar la escena de carga primero
        AsyncOperation loadLoadingScene = SceneManager.LoadSceneAsync("LoadingScene");
        yield return loadLoadingScene;

        // Iniciar la carga de la siguiente escena de manera asíncrona
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneName);
        loadScene.allowSceneActivation = false; // No activar la escena todavía

        // Esperar hasta que la escena esté casi cargada
        while (loadScene.progress < 0.9f)
        {
            yield return null; // Esperar hasta que esté lista
        }

        // Precargar el video en la nueva escena
        VideoPlayer videoPlayer = FindObjectOfType<VideoPlayer>();
        if (videoPlayer != null)
        {
            videoPlayer.url = videoUrl;
            videoPlayer.Prepare(); // Precargar el video
            while (!videoPlayer.isPrepared)
            {
                yield return null; // Esperar hasta que el video esté listo
            }
        }
        // Fundido de salida antes de activar la nueva escena
        yield return StartCoroutine(Fade(0f));

        // Activar la siguiente escena
        loadScene.allowSceneActivation = true;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}
