using UnityEngine;

class ObjectUpClose : MonoBehaviour
{
    public Sprite image;
    bool canInteract;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public AudioClip sound;
    public bool CanInteract
    {
        get { return canInteract; }
        private set { canInteract = value; }
    }
    void Start()
    {
        canInteract = false;
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void Interact(bool show)
    {
        if (show)
        {
            audioSource.PlayOneShot(sound);

            HUDManager.Instance.UpdateUpClose(image, show);
            return;
        }
        HUDManager.Instance.UpdateUpClose(null,show);
    }
    void OnTriggerEnter(Collider other)
    {
        canInteract = true;
    }
    void OnTriggerExit(Collider other)
    {
        canInteract = false;
    }
}