using UnityEngine;

public class ScissorsManager : MonoBehaviour
{
    [SerializeField] private Rigidbody scissorsRigidbody;

    private void Start()
    {
        // Asegúrate que las tijeras empiecen suspendidas
        scissorsRigidbody.isKinematic = true;
    }

    public void ReleaseScissors()
    {
        Debug.Log("Tijeras liberadas");
        scissorsRigidbody.isKinematic = false;
    }
}
