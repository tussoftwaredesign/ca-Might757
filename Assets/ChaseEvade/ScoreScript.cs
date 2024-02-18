using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    private TextMeshProUGUI textScore;
    private float score;

    public PlayerTimePlaying gameTime;

    // Start is called before the first frame update
    void Start()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the elapsed time of the game is smaller than a minute, multiply score by time by .01
        if (gameTime.elapsedTime < 60f) 
        {
            score += gameTime.elapsedTime * 0.01f;
        }
        else // if the elapsed time of the game is smaller than a 3 minutes and bigger than 1 minute, multiply score by time by .03
        if (gameTime.elapsedTime > 60f && gameTime.elapsedTime < 180f)
        {
            score += gameTime.elapsedTime * 0.03f;
        }
        textScore.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }
}
