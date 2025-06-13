
using System;
using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    public GameObject player;
    private bool isInteracting = false;
    public bool IsInteracting
    {
        get { return isInteracting; }
        set
        {
            isInteracting = value;
        }
    }
    private ItemPickup interactableItem = null;
    private Interactable interactableObject = null;
    public Interactable InteractableObject
    {
        get { return interactableObject; }
        set
        {
            interactableObject = value;
        }
    }
    [Header("Observers")]
    public TriggerDetector targetDetector;
    private static InteractSystem _instance;
    public static InteractSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("InteractSystem");
                _instance = obj.AddComponent<InteractSystem>();
            }
            return _instance;
        }
    }
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
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        targetDetector.OnTriggerEntered += TriggerEnter;
        targetDetector.OnTriggerExited += TriggerExit;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactableItem != null)
            {
                InteractWithItem();
            }
            if (interactableObject != null)
            {
                InteractWithObject();
            }
        }
        
    }
    public void InteractWithItem()
    {
        if (interactableItem != null && interactableItem.TryGetComponent(out ItemPickup pickup))
        {
            Debug.Log("agarro item: " + interactableObject.name);
            pickup.PickUp();
            return;
        }
    }
    public void InteractWithObject()
    {

        if (InteractableObject == null)
        {
            Debug.LogError("Interactable component not found on the object: " + InteractableObject.name);
            return;
        }
        Debug.Log("Attempting to interact with object: " + InteractableObject.name);

        
        if (isInteracting)
        {
            // If the player is already interacting with an object, switch back to the main camera
            InteractableObject.ChangeToMainCamera();
            isInteracting = false;
            player.SetActive(true);

            return;
        }
        else
        {
            Debug.Log("Player is not interacting with any object, checking if interaction is possible.");
            if (InteractableObject.canInteract)
            {
                Debug.Log("Player is interacting with: " + InteractableObject.name);
                InteractableObject.ChangeToCamera();
                isInteracting = true;
                player.SetActive(false);
                return;
            }
            else
            {
                Debug.Log("Interaction with " + InteractableObject.name + " is not possible at the moment");
            }
        }
    }
    void TriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Interactable interactable))
        {
            interactable.canInteract = true;
            OnCanInteract?.Invoke(true);
            interactableObject = interactable;
            Debug.Log("Entered interactable object: " + interactable.name);
            return;
        }
        if (collider.TryGetComponent(out ItemPickup item))
        {
            OnCanInteract?.Invoke(true);
            interactableItem = item;
            Debug.Log("Entered interactable item : " + item.name);
            return;
        }
    }
    void TriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Interactable interactable))
        {
            interactable.canInteract = false;
            OnCanInteract?.Invoke(false);
            interactableObject = null;
            Debug.Log("Exited interactable trigger, cleared interactable object.");
        }
        if (collider.TryGetComponent(out ItemPickup item))
        {
            OnCanInteract?.Invoke(false);
            interactableItem = null;
            Debug.Log("Exited interactable item : " + item.name);
            return;
        }
    }
}