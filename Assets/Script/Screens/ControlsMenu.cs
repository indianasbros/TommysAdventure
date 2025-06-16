using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField] private List<TMP_InputField> keyInputs;
    [SerializeField] private List<Slider> audioSliders;
    [SerializeField] private TMP_Text messageText;

    public static KeyCode InteractKey { get; private set; } = KeyCode.E;

    void Start()
    {
        for (int i = 0; i < keyInputs.Count; i++)
        {
            if (PlayerPrefs.HasKey("Key_" + i))
            {
                keyInputs[i].text = PlayerPrefs.GetString("Key_" + i);
            }
        }
        for (int i = 0; i < audioSliders.Count; i++)
        {
            if (PlayerPrefs.HasKey("Volume_" + i))
            {
                audioSliders[i].value = PlayerPrefs.GetFloat("Volume_" + i);
            }
        }
    }

    public void OnSave()
    {
        for (int i = 0; i < keyInputs.Count; i++)
        {
            PlayerPrefs.SetString("Key_" + i, keyInputs[i].text);
        }
        for (int i = 0; i < audioSliders.Count; i++)
        {
            PlayerPrefs.SetFloat("Volume_" + i, audioSliders[i].value);
        }
        PlayerPrefs.Save();

        messageText.text = "** Configuraciones Guardadas **";

        if (System.Enum.TryParse<KeyCode>(keyInputs[4].text, true, out var parsedKey))
        {
            InteractKey = parsedKey;
        }
        else
        {
            InteractKey = KeyCode.E;
        }

        StartCoroutine(ClearMessageAfterDelay(3));
    }

    private IEnumerator ClearMessageAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        messageText.text = "";
    }
}