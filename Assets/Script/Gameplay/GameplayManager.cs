using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject player;
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
            DontDestroyOnLoad(gameObject);
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

    // Update is called once per frame
    void Update()
    {

    }
    
}
