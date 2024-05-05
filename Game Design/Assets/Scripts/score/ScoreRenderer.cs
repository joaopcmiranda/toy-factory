using score;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRenderer : MonoBehaviour
{

    public SpriteRenderer StarOne, StarTwo, StarThree;
    public Sprite starFull, starEmpty;
    public TextMeshProUGUI scoreText; 

    void Start()
    {
        // Retrieve score
        int score = PlayerPrefs.GetInt("PlayerScore");
        //Reset score
        PlayerPrefs.SetInt("PlayerScore", 0);

        StarOne = GameObject.Find("StarOne").GetComponent<SpriteRenderer>();
        StarTwo = GameObject.Find("StarTwo").GetComponent<SpriteRenderer>();
        StarThree = GameObject.Find("StarThree").GetComponent<SpriteRenderer>();

        // Render stars based on score
        RenderStars(score);
    }

    void RenderStars(int score)
    {
        scoreText.text = "Score: " + score; 
        if (score < 100)
        {
            StarOne.sprite = starEmpty;
            StarTwo.sprite = starEmpty;
            StarThree.sprite = starEmpty;
            //render no stars 
        }
        else if (score >= 100 && score < 300)
        {
            StarOne.sprite = starFull;
            StarTwo.sprite = starEmpty;
            StarThree.sprite = starEmpty;
            //render one star
        } 
        else if (score >= 300 && score < 500)
        {
            StarOne.sprite = starFull;
            StarTwo.sprite = starFull;
            StarThree.sprite = starEmpty;
            //render two stars
        } 
        else if (score >= 500)
        {
            StarOne.sprite = starFull;
            StarTwo.sprite = starFull;
            StarThree.sprite = starFull;
            //render three stars
        }
        Debug.Log(score);
        // Logic to render stars based on the score
        // For example, instantiate star GameObjects based on the score
    }
}
