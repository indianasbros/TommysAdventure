using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Script.Difficulty
{
    
    public enum Difficulty { Easy, Normal, Hard }

    public class DifficultyController: MonoBehaviour
    {
        public static DifficultyController Instance { get; private set; }

        [Header("Configuración")]
        [SerializeField] private Difficulty gameDifficulty = Difficulty.Normal;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        public Difficulty GameDifficulty
        {
            get => gameDifficulty;
            set
            {
                gameDifficulty = value;
            }
        }
        public void SetGameDifficulty(Difficulty newDifficulty)
        {
            gameDifficulty = newDifficulty;
        }
        public Difficulty GetGameDifficulty()
        {
            return gameDifficulty;
        }

    }
}

