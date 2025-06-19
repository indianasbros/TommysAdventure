using UnityEngine;

public class CandleController : MonoBehaviour
{
    [SerializeField] private Light candleLight;
    [SerializeField] private KeyCode grabKey = KeyCode.E;
    [SerializeField] private KeyCode dropKey = KeyCode.Q;
    [SerializeField] private Transform playerHoldPoint;

    private bool isLit = true; // Por defecto la vela está encendida
    private bool isCarried = false;
    private bool isPlayerInRange = false;
    private Rigidbody rb;
    Transform originalParent;

    void Start()
    {
        originalParent = transform.parent;
        if (candleLight != null)
            candleLight.enabled = isLit;

        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>(); // Seguridad por si falta

        //Control Setting for Interact
        if (PlayerPrefs.HasKey("Key_0"))
        {
            if (System.Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_0"), true, out var parsedKey))
            {
                grabKey = parsedKey;
            }
        }
        //Control Setting for Interact
        if (PlayerPrefs.HasKey("Key_1"))
        {
            if (System.Enum.TryParse<KeyCode>(PlayerPrefs.GetString("Key_1"), true, out var parsedKey))
            {
                dropKey = parsedKey;
            }
        }
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            if (!isCarried && Input.GetKeyDown(grabKey))
            {
                PickUpCandle();
            }
            else if (isCarried && Input.GetKeyDown(dropKey))
            {
                // Si ya la estás llevando, E sirve para soltar
                DropCandle();
            }
        }

        // Encender/apagar solo cuando la estás llevando
        if (isCarried && Input.GetKeyDown(KeyCode.F))
        {
            ToggleLight();
        }
    }

    private void PickUpCandle()
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
            Debug.Log("Candle picked up");
        }
    }

    private void DropCandle()
    {
        transform.SetParent(originalParent);
        isCarried = false;

        rb.isKinematic = false;
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = false; // Para evitar colisiones mientras se lleva
        transform.position = playerHoldPoint.position + playerHoldPoint.forward * 0.5f + Vector3.down * 0.2f;
        Debug.Log("Candle dropped");
    }

    private void ToggleLight()
    {
        isLit = !isLit;
        if (candleLight != null)
            candleLight.enabled = isLit;
        Debug.Log("Candle light: " + isLit);
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