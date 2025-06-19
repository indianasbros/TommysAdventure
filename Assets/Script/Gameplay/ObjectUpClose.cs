using UnityEngine;

class ObjectUpClose : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(audioClip);
            return;
        }
        HUDManager.Instance.UpdateUpClose(null, show);
        audioSource.PlayOneShot(audioClip);
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