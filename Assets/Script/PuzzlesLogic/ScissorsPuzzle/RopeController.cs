using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] private bool isCorrectRope = false;
    [SerializeField] private ScissorsManager scissorsManager;

    private bool used = false;

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
}
