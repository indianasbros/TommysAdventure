using Assets.Script.Difficulty;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    [SerializeField] private Toggle toggleEasy;
    [SerializeField] private Toggle toggleMedium;
    [SerializeField] private Toggle toggleHard;

    public Difficulty Selected { get; private set; }

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
        {
            Selected = Difficulty.Easy;
        }
        else if (toggleMedium.isOn)
        {
            Selected = Difficulty.Normal;
        }
        else if (toggleHard.isOn)
        {
            Selected = Difficulty.Hard;
        }
        DifficultyController.Instance.SetGameDifficulty(Selected);
    }
}
