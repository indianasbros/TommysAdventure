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
    public void InteractWithObject(GameObject obj)
    {
        player.TryGetComponent(out PlayerInventory inventory);
        obj.TryGetComponent(out Interactable interactable);
        if (inventory == null)
        {
            Debug.LogError("PlayerInventory component not found on player GameObject.");
            return;
        }
        if (interactable == null)
        {
            Debug.LogError("Interactable component not found on the object: " + obj.name);
            return;
        }
        Debug.Log("Attempting to interact with object: " + obj.name);
        if (inventory.IsInteracting)
        {
            Debug.Log("Player is already interacting with an object, switching back to main camera.");
            // If the player is already interacting with an object, switch back to the main camera
            interactable.ChangeToMainCamera();
            inventory.IsInteracting = false;
            interactable.isInteracting = false;
            interactable.canInteract = true;
            Debug.Log(player.name + " is now active again.");
            player.SetActive(true);

            return;
        }
        else
        {
            Debug.Log("Player is not interacting with any object, checking if interaction is possible.");
        }
        if (interactable.canInteract)
        {
            Debug.Log("Player is interacting with: " + obj.name);
            interactable.ChangeToCamera();
            inventory.IsInteracting = true;
            interactable.isInteracting = true;
            interactable.canInteract = false;
            player.SetActive(false);
            return;
        }
        else
        {
            Debug.Log("Interaction with " + obj.name + " is not possible at the moment?"+obj.name);
        }
    }
}
