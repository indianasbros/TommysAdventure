using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] private bool isCorrectRope = false;
    [SerializeField] private ScissorsManager scissorsManager;
    private bool canInteract = false;
    private bool used = false;

    void Update()
    {
        if (used && gameObject.activeSelf)
        {
            DeactivateRope();
            return;
        }
        if (Input.GetKeyDown(KeyCode.E) && canInteract && !used)
        {
            // Si la cuerda no ha sido usada, se puede cortar
            Cut();
        }
    }

    private void Cut()
    {
        if (used) return;
        used = true;

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
        used = false;
        gameObject.SetActive(true); // Reactiva la cuerda
        Debug.Log("Cuerda reiniciada: " + gameObject.name);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered trigger for rope: " + gameObject.name);
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Player exited trigger for rope: " + gameObject.name);
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
