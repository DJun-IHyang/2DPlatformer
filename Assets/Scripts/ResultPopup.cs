using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultPopup : MonoBehaviour
{
    public TextMeshProUGUI titleLabel;
    public TextMeshProUGUI scoreLabel;


    private void OnEnable()
    {
        Time.timeScale = 0;

        if(GameManager.Instance.isCleared)
        {
            titleLabel.text = "Cleared!";
            scoreLabel.text = GameManager.Instance.timeLimit.ToString("#.##");
        }
        else
        {
            titleLabel.text = "GameOver";
            scoreLabel.text = "";
        }
    }

    public void PlayAgainPressed()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("GameScene");
    }
}
