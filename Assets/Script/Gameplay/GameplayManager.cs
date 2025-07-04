using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public GameObject player;
    [SerializeField] AudioMixer audioMixer;
    private static GameplayManager _instance;
    public static GameplayManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("GameplayManager");
                _instance = obj.AddComponent<GameplayManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned in GameplayManager.");
            player = GameObject.FindWithTag("Player");

            if (player == null)
            {
                Debug.LogError("No GameObject with tag 'Player' found in the scene.");
                return;
            }
        }
        else
        {
            Debug.Log("GameplayManager initialized with player: " + player.name);
        }
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            CameraManager.Instance.LockCursor(false);
            RoomManager.Instance.PauseMusic();
            DialogueManager.Instance.PauseAudio();
            AudioListener.pause = true;
            Time.timeScale = 0;
            return;
        }
        if (!CameraManager.Instance.IsCursorLocked() && InventorySystem.Instance != null && InventorySystem.Instance.IsInventoryOpen)
        {
            // If the inventory is open, we don't lock the cursor
            CameraManager.Instance.LockCursor(false);
        }
        else if (ObjectInventorySystem.Instance != null && ObjectInventorySystem.Instance.IsInventoryOpen)
        {
            // If the object inventory is open, we don't lock the cursor
            CameraManager.Instance.LockCursor(false);
        }
        else
        {
            CameraManager.Instance.LockCursor(true);
        }
        Time.timeScale = 1;
        RoomManager.Instance.ResumeMusic();
        DialogueManager.Instance.ResumeAudio();
        AudioListener.pause = false;
    }
    public void GameOver()
    {
        CameraManager.Instance.LockCursor(false);
        SceneManager.LoadScene("Defeat");

    }
    public void Victory()
    {
        CameraManager.Instance.LockCursor(false);
        SceneManager.LoadScene("Victory");
    }
    public void ChangeScene(string sceneName)
    {
        CameraManager.Instance.LockCursor(false);
        SceneManager.LoadScene(sceneName);
    }
}
