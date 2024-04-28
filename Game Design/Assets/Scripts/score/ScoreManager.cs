using UnityEngine;
using UnityEngine.UI;
namespace score
{
    public class ScoreManager: MonoBehaviour
    {

        public Text scoreText;
        public int score;

        /*private void Start()
        {
            score = 0;
            scoreText.text = "Score: " + score;
        }*/

        //For tutorial. can be deleted for other levels.
        public void StartScore()
        {
            score = 0;
            scoreText.text = "Score: " + score;
        }

        public void IncreaseScore(int increment)
        {
            score += increment;
            UpdateScore();
        }

        public void DecreaseScore(int decrement)
        {
            score -= decrement;
            UpdateScore();
        }

        private void UpdateScore()
        {
            PlayerPrefs.SetInt("PlayerScore", score);
            scoreText.text = "Score: " + score;
        }
    }
}
