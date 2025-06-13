using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] private bool isCorrectRope = false;
    [SerializeField] private ScissorsManager scissorsManager;

    private bool cutted = false;
    private bool canCut = false;

    void Update()
    {
        if (cutted && gameObject.activeSelf)
        {
            DeactivateRope();
            return;
        }
        if (!cutted && canCut && Input.GetKeyDown(KeyCode.E))
        {
            OnMouseDown();
        }
    }

    private void OnMouseDown()
    {
        if (cutted) return;
        cutted = true;

        // Lógica visual opcional: desaparecer cuerda, animación, etc.
        Debug.Log("Cuerda desatada: " + gameObject.name);

        if (isCorrectRope)
        {
            scissorsManager.ReleaseScissors();
        }
    }
    private void DeactivateRope()
    {
        gameObject.SetActive(false); // Desactiva la cuerda
    }
    public void ResetRope()
    {
        cutted = false;
        gameObject.SetActive(true); // Reactiva la cuerda
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
