using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
   
    [SerializeField]private GameObject menu;
    bool isMenuOpen;
    public static PauseMenu Instance { get; private set; }
    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        isMenuOpen = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuOpen = !isMenuOpen;
            PauseGame(true);
        }
        menu.SetActive(isMenuOpen);
    }
    public void PauseGame(bool pause)
    {
        if (pause)
        {
            GameplayManager.Instance.PauseGame(true);
            CameraManager.Instance.LockCursor(false);
            RoomManager.Instance.PauseMusic();
            DialogueManager.Instance.PauseAudio();
            isMenuOpen = true;
            return;
        }
        GameplayManager.Instance.PauseGame(false);
        CameraManager.Instance.LockCursor(true);
        RoomManager.Instance.ResumeMusic();
        DialogueManager.Instance.ResumeAudio();

        isMenuOpen = false;

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameplayManager.Instance.PauseGame(false);

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        GameplayManager.Instance.PauseGame(false);
    }
}