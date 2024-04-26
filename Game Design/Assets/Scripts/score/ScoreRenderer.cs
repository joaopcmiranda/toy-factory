using score;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRenderer : MonoBehaviour
{

    SpriteRenderer StarOne, StarTwo, StarThree;

    void Start()
    {
        // Retrieve score
        int score = PlayerPrefs.GetInt("PlayerScore");
        PlayerPrefs.SetInt("PlayerScore", 0);

        StarOne = GameObject.Find("StarOne").GetComponent<SpriteRenderer>();
        StarTwo = GameObject.Find("StarTwo").GetComponent<SpriteRenderer>();
        StarThree = GameObject.Find("StarThree").GetComponent<SpriteRenderer>();

        StarOne.enabled = false;
        StarTwo.enabled = false;
        StarThree.enabled = false;

        // Render stars based on score
        RenderStars(score);
    }

    void RenderStars(int score)
    {
        if (score < 100)
        {
            StarOne.enabled = true;
            StarTwo.enabled = false;
            StarThree.enabled = false;
            //render one star
        } 
        else if (score >= 100 && score < 300)
        {
            StarOne.enabled = true;
            StarTwo.enabled = true;
            StarThree.enabled = false;
            //render two stars
        } 
        else if (score >= 300)
        {
            StarOne.enabled = true;
            StarTwo.enabled = true;
            StarThree.enabled = true;
            //render three stars
        }
        Debug.Log(score);
        // Logic to render stars based on the score
        // For example, instantiate star GameObjects based on the score
    }
}
