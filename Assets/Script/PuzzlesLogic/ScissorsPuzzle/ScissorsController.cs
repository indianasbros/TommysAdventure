using UnityEngine;

public class ScissorsController : MonoBehaviour
{
    //[SerializeField] private Light siccorsLight;
    [SerializeField] private KeyCode grabKey = KeyCode.E;
    [SerializeField] private KeyCode dropKey = KeyCode.Q;
    [SerializeField] private Transform playerHoldPoint;

    //private bool isLit = true; // Por defecto la vela está encendida
    private bool isCarried = false;
    private bool isPlayerInRange = false;
    private Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>(); // Seguridad por si falta
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            if (!isCarried && Input.GetKeyDown(grabKey))
            {
                PickUp();
            }
            else if( isCarried && Input.GetKeyDown(dropKey))
            {
                // Si ya la estás llevando, E sirve para soltar
                Drop();
            }
            
        }

        // Encender/apagar solo cuando la estás llevando
        if (isCarried && Input.GetKeyDown(KeyCode.F))
        {
            Use();
        }
    }

    private void PickUp()
    {
        if (playerHoldPoint == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerHoldPoint = player.transform.Find("CandleHoldPoint");
        }

        if (playerHoldPoint != null)
        {
            transform.SetParent(playerHoldPoint);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            isCarried = true;
            rb.isKinematic = true;
            
            Collider collider = GetComponent<Collider>();
            collider.isTrigger = true; // Para evitar colisiones mientras se lleva
            Debug.Log("Scissors picked up");
        }
    }

    private void Drop()
    {
        transform.SetParent(null);
        isCarried = false;

        rb.isKinematic = false;
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = false; // Para evitar colisiones mientras se lleva
        // Deja la vela un poco más abajo para que caiga
        transform.position = playerHoldPoint.position + playerHoldPoint.forward * 0.5f + Vector3.down * 0.2f;
        Debug.Log("Scissors dropped");
    }

    private void Use()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isCarried)
            isPlayerInRange = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && !isCarried)
            isPlayerInRange = false;
    }
}
