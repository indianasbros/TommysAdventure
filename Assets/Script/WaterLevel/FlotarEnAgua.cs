using UnityEngine;

public class FlotarEnAgua : MonoBehaviour
{
    public float waterLevel = 0.0f;
    public float waterDensity = 0.5f;
    public float waterDrag = 5.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    void FixedUpdate()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            if (transform.position.y < waterLevel)
            {
                rb.drag = waterDrag;
                rb.angularDrag = waterDrag;
                Vector3 waterUp = new Vector3(0.0f, 1.0f, 0.0f);
                float displacementFactor = Mathf.Clamp01((waterLevel - transform.position.y) / GetComponent<Renderer>().bounds.extents.y);
                float upForce = displacementFactor * waterDensity * Mathf.Abs(Physics.gravity.y) * GetComponent<Rigidbody>().mass;
                Vector3 appliedForce = upForce * waterUp;
                rb.AddForce(appliedForce, ForceMode.Force);
            }
            else
            {
                rb.drag = 0.0f;
                rb.angularDrag = 0.05f;
            }
        }
    }
}
