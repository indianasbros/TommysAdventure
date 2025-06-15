using UnityEngine;

public class ErrorController : MonoBehaviour
{
    private int errorLimit;
    private int errors;

    void Awake()
    {
        errorLimit = 3;
        errors = 0;
    }
    void Update()
    {
        bool lost = CheckErrors();
        if (lost)
        {
            GameplayManager.Instance.GameOver();
        }
    }
    public bool CheckErrors()
    {
        if (errors == errorLimit)
        {
            return true;
        }
        return false;
    }
    public void Fail()
    {
        errors++;
    }
}