using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    void Start()
    {
        // Go to the menu after 20 seconds
        StartCoroutine(RetunToMenu());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    System.Collections.IEnumerator RetunToMenu()
    {
        yield return new WaitForSeconds(20f);
        SceneManager.LoadScene("Menu");
    }
}
