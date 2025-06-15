using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;

public class ScissorsManager : MonoBehaviour
{
    [SerializeField] private Rigidbody scissorsRigidbody;
    [SerializeField] private RopeController[] ropes;
    private void Start()
    {
        scissorsRigidbody.isKinematic = true;
    }

    public void ReleaseScissors()
    {
        scissorsRigidbody.isKinematic = false;
        foreach (var rope in ropes)
        {
            if (!rope.Cutted)
            {
                rope.DeactivateRope();
            }
        }
    }
}
