using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] private bool isCorrectRope = false;
    [SerializeField] private ScissorsManager scissorsManager;

    private bool used = false;

    void Update()
    {
        if (used && gameObject.activeSelf)
        {
            DeactivateRope();
            return;
        }
        if(!used && Input.GetKeyDown(KeyCode.E))
        {
            OnMouseDown();
        }
    }
    
    private void OnMouseDown()
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
}
