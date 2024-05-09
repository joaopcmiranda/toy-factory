using UnityEngine;
using UnityEngine.UI;
namespace score
{
    public class ScoreManager: MonoBehaviour
    {

        public Text scoreText;
        public int score;
        public bool tutorial;

        private AudioManager audioManager;

        private void Start()
        {
            if (!tutorial)
            {
                score = 0;
                scoreText.text = "Score: " + score;
            }
            audioManager = FindObjectOfType<AudioManager>();
        }

        //For tutorial
        public void StartScore()
        {
            score = 0;
            scoreText.text = "Score: " + score;
        }

        public void IncreaseScore(int increment)
        {
            score += increment;
            audioManager.PlayOrder();
            UpdateScore();
        }

        public void DecreaseScore(int decrement)
        {
            score -= decrement;
            audioManager.PlayNegativeScore();
            UpdateScore();
        }

        private void UpdateScore()
        {
            PlayerPrefs.SetInt("PlayerScore", score);
            scoreText.text = "Score: " + score;
        }
    }
}
