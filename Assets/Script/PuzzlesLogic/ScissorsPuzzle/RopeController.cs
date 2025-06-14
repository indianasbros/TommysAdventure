using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] private bool isCorrectRope = false;
    [SerializeField] private ScissorsManager scissorsManager;
    [SerializeField] private GameObject rope;

    private bool cutted = false;
    public bool Cutted { get { return cutted; } }
    private bool canCut = false;
    public ErrorController errorController;
    void Update()
    {
        if (cutted && gameObject.activeSelf)
        {
            DeactivateRope();
            return;
        }
        if (!cutted && canCut && Input.GetKeyDown(KeyCode.E))
        {
            Untie();
        }
    }

    private void Untie()
    {
        if (cutted) return;
        cutted = true;

        Debug.Log("Cuerda desatada: " + gameObject.name);

        if (isCorrectRope)
        {
            scissorsManager.ReleaseScissors();
        }
        else
        {
            errorController.Fail();
        }
    }
    public void DeactivateRope()
    {
        rope.SetActive(false); // Desactiva la cuerda
    }
    public void ResetRope()
    {
        cutted = false;
        rope.SetActive(true); // Reactiva la cuerda
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCut = true;
            Debug.Log("Player can cut the rope: " + gameObject.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCut = false;
            Debug.Log("Player cannot cut the rope: " + gameObject.name);
        }
    }
}
