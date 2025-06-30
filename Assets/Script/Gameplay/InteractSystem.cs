using System;
using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    public GameObject player;

    private bool isInteracting = false;
    public bool IsInteracting
    {
        get => isInteracting;
        private set
        {
            isInteracting = value;
            OnCanInteract?.Invoke(!isInteracting);
        }
    }

    private GameObject interactableTarget;

    [Header("Observers")]
    public TriggerDetector targetDetector;

    private static InteractSystem _instance;
    public static InteractSystem Instance => _instance ??= new GameObject("InteractSystem").AddComponent<InteractSystem>();

    public event Action<bool> OnCanInteract;
    public event Action<InteractableInventory> OnCanInteractWithInventory;
    public event Action OnInteractWithInventory;

    private KeyCode interactKey = KeyCode.E;


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

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        targetDetector.OnTriggerEntered += TriggerEnter;
        targetDetector.OnTriggerExited += TriggerExit;

        //Control Setting for Interact
        if (PlayerPrefs.HasKey("Key_0"))
        {
            if (Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_0"), true, out var parsedKey))
            {
                interactKey = parsedKey;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey) && interactableTarget != null)
        {
            InteractWithTarget();
        }
    }

    public void InteractWithTarget()
    {
        GameObject obj = interactableTarget;

  
        if (obj.TryGetComponent<IPickable>(out var pickup))
        {
            player.GetComponent<Animator>()?.SetTrigger("doPickUp");
            pickup.PickUp();
            interactableTarget = null;
            OnCanInteract?.Invoke(false);
            return;
        }

        
        if (obj.TryGetComponent<IInventoryReceiver>(out var receiver))
        {
            if (receiver.CanReceive)
            {
                OnInteractWithInventory?.Invoke();
                isInteracting = true;
                ObjectInventorySystem.Instance.OpenInventory();
            }
            else
            {
                Debug.Log("No se puede interactuar con el inventario");
            }
        }

        
        if (obj.TryGetComponent<ICameraInteractable>(out var cameraObj))
        {
            if (obj.TryGetComponent<InteractableInventory>(out var inventory) && inventory.CanReceive)
            {
                OnInteractWithInventory?.Invoke();
                isInteracting = true;
                ObjectInventorySystem.Instance.OpenInventory();
            }
            if (isInteracting)
            {
                cameraObj.ChangeToMainCamera();
                isInteracting = false;
                player.SetActive(true);
            }
            else if (cameraObj.CanInteract)
            {
                cameraObj.ChangeToCamera();
                isInteracting = true;
                player.SetActive(false);
            }
        }
        //4. Objeto de cerca
        if (obj.TryGetComponent<ObjectUpClose>(out var upClose))
        {
            if (isInteracting)
            {
                upClose.Interact(false);
                isInteracting = false;
            }
            else if (upClose.CanInteract)
            {
                upClose.Interact(true);
                isInteracting = true;
            }
            
        }
    }

    void TriggerEnter(Collider collider)
    {
        GameObject obj = collider.gameObject;
        if (collider.CompareTag("Interactable"))
        {
            OnCanInteract?.Invoke(true);
        }
        bool hasInteraction = obj.TryGetComponent<IPickable>(out _) ||
                            obj.TryGetComponent<IInventoryReceiver>(out _) ||
                            obj.TryGetComponent<ICameraInteractable>(out _) ||
                            obj.TryGetComponent<ObjectUpClose>(out _);

        if (hasInteraction)
        {
            interactableTarget = obj;
            OnCanInteract?.Invoke(true);
            if (obj.TryGetComponent<InteractableInventory>(out var inventory))
            {
                OnCanInteractWithInventory?.Invoke(inventory);
            }
        }
    }

    void TriggerExit(Collider collider)
    {
        if (collider.CompareTag("Interactable"))
        {
            OnCanInteract?.Invoke(false);
        }
        if (collider.gameObject == interactableTarget)
        {
            interactableTarget = null;
            OnCanInteract?.Invoke(false);
            Debug.Log($"Exited interactable object: {collider.name}");
        }
    }
}
