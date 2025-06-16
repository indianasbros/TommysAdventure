using UnityEngine;

class ObjectUpClose : MonoBehaviour
{
    public Sprite image;
    bool canInteract;
    public bool CanInteract
    {
        get { return canInteract; }
        private set { canInteract = value; }
    }
    void Start()
    {
        canInteract = false;
    }
    public void Interact(bool show)
    {
        if (show)
        {
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