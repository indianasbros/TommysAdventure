using System;
using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    public GameObject player;

    private bool isInteracting = false;
    public bool IsInteracting => isInteracting;

    private GameObject interactableTarget;

    [Header("Observers")]
    public TriggerDetector targetDetector;

    private static InteractSystem _instance;
    public static InteractSystem Instance => _instance ??= new GameObject("InteractSystem").AddComponent<InteractSystem>();

    public event Action<bool> OnCanInteract;

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

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        targetDetector.OnTriggerEntered += TriggerEnter;
        targetDetector.OnTriggerExited += TriggerExit;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactableTarget != null)
        {
            InteractWithTarget();
        }
    }

    public void InteractWithTarget()
    {
        GameObject obj = interactableTarget;

        // 1. Pickup
        if (obj.TryGetComponent<IPickable>(out var pickup))
        {
            player.GetComponent<Animator>()?.SetTrigger("doPickUp");
            pickup.PickUp();
            interactableTarget = null;
            OnCanInteract?.Invoke(false);
            return;
        }

        // 2. Inventario (puzzle, etc.)
        if (obj.TryGetComponent<IInventoryReceiver>(out var receiver))
        {
            receiver.TryReceiveItems();
        }

        // 3. Cámara
        if (obj.TryGetComponent<ICameraInteractable>(out var cameraObj))
        {
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
            Debug.Log($"Entered interactable object: {obj.name}");
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
