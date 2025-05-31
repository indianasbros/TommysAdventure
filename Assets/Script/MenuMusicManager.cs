using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicManager : MonoBehaviour
{
    private static MenuMusicManager instance;

    // Keep menu music in settings
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            AudioSource audio = GetComponent<AudioSource>();
            if (audio != null && !audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Menu" && scene.name != "Settings")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}