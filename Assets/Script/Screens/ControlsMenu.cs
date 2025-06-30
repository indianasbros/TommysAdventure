using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField] private List<TMP_InputField> keyInputs;
    [SerializeField] private List<Slider> audioSliders;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private List<TMP_Text> volumeValueTexts;

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
            float value = PlayerPrefs.HasKey("Volume_" + i) ? PlayerPrefs.GetFloat("Volume_" + i) : 100f;

            audioSliders[i].value = value;
            SetMixerVolume(i, value);
            UpdateVolumeText(i, value);
        }

        //Listen Sliders Changes
        for (int i = 0; i < audioSliders.Count; i++)
        {
            int index = i;
            audioSliders[i].onValueChanged.AddListener((value) =>
            {
                SetMixerVolume(index, value);
                UpdateVolumeText(index, value);
            });
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

        StartCoroutine(ClearMessageAfterDelay(3));
    }

    private IEnumerator ClearMessageAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        messageText.text = "";
    }

    private void SetMixerVolume(int index, float sliderValue)
    {
        float normalizedValue = Mathf.Clamp01(sliderValue / 100f);
        float volumeInDb = Mathf.Lerp(-80f, 0f, Mathf.Pow(normalizedValue, 0.5f));


        switch (index)
        {
            case 0:
                audioMixer.SetFloat("Volume_Master", volumeInDb);
                break;
            case 1:
                audioMixer.SetFloat("Volume_Music", volumeInDb);
                break;
            case 2:
                audioMixer.SetFloat("Volume_Sfx", volumeInDb);
                break;
            case 3:
                audioMixer.SetFloat("Volume_Dialogues", volumeInDb);
                break;
        }
    }

    private void UpdateVolumeText(int index, float value)
    {
        volumeValueTexts[index].text = Mathf.RoundToInt(value) + "%";
    }
}