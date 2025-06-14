using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Toggle toggleEasy;
    [SerializeField] private Toggle toggleMedium;
    [SerializeField] private Toggle toggleHard;

    public string LevelSelected { get; private set; }

    void Start()
    {
        toggleEasy.onValueChanged.AddListener(delegate { UpdateLevel(); });
        toggleMedium.onValueChanged.AddListener(delegate { UpdateLevel(); });
        toggleHard.onValueChanged.AddListener(delegate { UpdateLevel(); });

        UpdateLevel(); 
    }

    void UpdateLevel()
    {
        if (toggleEasy.isOn)
            LevelSelected = "FACIL";
        else if (toggleMedium.isOn)
            LevelSelected = "MEDIO";
        else if (toggleHard.isOn)
            LevelSelected = "DIFICIL";
    }
}
