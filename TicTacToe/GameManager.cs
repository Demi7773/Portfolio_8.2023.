using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Text[] textFields;     // text fields for X / O display
    [SerializeField] Text turnTxt;          // turn count display
    public InputField pOneInputField;
    public InputField pTwoInputField;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Button playAgainButton;

    [SerializeField] Text scorePlayerOne;   // player1 score display
    [SerializeField] Text scorePlayerTwo;   // player2 score display

    string playerOneName;
    string playerTwoName;

    [SerializeField] public string playerSide;     // X or O for diff players

    [SerializeField] public int turnCount;         // number of turns
    [SerializeField] int turnCountMax = 10;  // number of max possible turns



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerSide = "X";
        turnCount = 1;
        turnTxt.text = $"Turn {turnCount}";

        for (int i = 0; i < textFields.Length; i++)
        {
            textFields[i].text = null;
            textFields[i].GetComponentInParent<Button>().interactable = true;
        }

        scorePlayerOne.text = $"{playerOneName}: {PlayerPrefs.GetInt("ScoreP1")}";
        scorePlayerTwo.text = $"{playerTwoName}: {PlayerPrefs.GetInt("ScoreP2")}";
    }

    void ChangeSide()
    {
        if(playerSide == "X")
        {
            playerSide = "O";
        }
        else
        {
            playerSide = "X";
        }

        turnTxt.text = $"Turn {turnCount}";
    }

    public void WinCheck()
    {
        // horizontal checks
        if ((textFields[0].text == playerSide) && (textFields[1].text == playerSide) && (textFields[2].text == playerSide))
        {
            GameOver(playerSide);
        }
        else if ((textFields[3].text == playerSide) && (textFields[4].text == playerSide) && (textFields[5].text == playerSide))
        {
            GameOver(playerSide);
        }
        else if ((textFields[6].text == playerSide) && (textFields[7].text == playerSide) && (textFields[8].text == playerSide))
        {
            GameOver(playerSide);
        }

        // vertical checks
        else if ((textFields[0].text == playerSide) && (textFields[3].text == playerSide) && (textFields[6].text == playerSide))
        {
            GameOver(playerSide);
        }
        else if ((textFields[1].text == playerSide) && (textFields[4].text == playerSide) && (textFields[7].text == playerSide))
        {
            GameOver(playerSide);
        }
        else if ((textFields[2].text == playerSide) && (textFields[5].text == playerSide) && (textFields[8].text == playerSide))
        {
            GameOver(playerSide);
        }

        // diagonal checks
        else if ((textFields[0].text == playerSide) && (textFields[4].text == playerSide) && (textFields[8].text == playerSide))
        {
            GameOver(playerSide);
        }
        else if ((textFields[2].text == playerSide) && (textFields[4].text == playerSide) && (textFields[6].text == playerSide))
        {
            GameOver(playerSide);
        }

        // tie check
        else if(turnCount > turnCountMax)
        {
            GameOver("tie");
        }

        // not game over
        else
        {
            ChangeSide();
        }
    }

    void GameOver(string whoWins)
    {
        if (whoWins == "X")
        {
            gameOverPanel.GetComponentInChildren<Text>().text = $"{playerOneName} Wins!";
            gameOverPanel.SetActive(true);
            PlayerPrefs.SetInt("ScoreP1", PlayerPrefs.GetInt("ScoreP1") + 1);
            scorePlayerOne.text = $"{playerOneName}: {PlayerPrefs.GetInt("ScoreP1")}";
        }
        else if (whoWins == "O")
        {
            gameOverPanel.GetComponentInChildren<Text>().text = $"{playerTwoName} Wins!";
            gameOverPanel.SetActive(true);
            PlayerPrefs.SetInt("ScoreP2", PlayerPrefs.GetInt("ScoreP2") + 1);
            scorePlayerTwo.text = $"{playerTwoName}: {PlayerPrefs.GetInt("ScoreP2")}";
        }
        else
        {
            gameOverPanel.GetComponentInChildren<Text>().text = "TIE!";
            gameOverPanel.SetActive(true);
        }
    }

    public void PlayAgain()
    {
        ResetGame();
    }

    void ResetGame()
    {
        gameOverPanel.SetActive(false);
        Start();
    }

    public void ChangePlayerName()
    {
        if (pOneInputField.text != "" && pTwoInputField.text != "")
        {
            PlayerPrefs.SetString("NameP1", pOneInputField.text);
            PlayerPrefs.SetString("NameP2", pTwoInputField.text);

            playerOneName = PlayerPrefs.GetString("NameP1");
            playerTwoName = PlayerPrefs.GetString("NameP2");

            scorePlayerOne.text = $"{playerOneName}: {PlayerPrefs.GetInt("ScoreP1")}";
            scorePlayerTwo.text = $"{playerTwoName}: {PlayerPrefs.GetInt("ScoreP2")}";
        }

        else
        {
            PlayerPrefs.SetString("NameP1", "Player 1");
            PlayerPrefs.SetString("NameP2", "Player 2");

            playerOneName = PlayerPrefs.GetString("NameP1");
            playerTwoName = PlayerPrefs.GetString("NameP2");

            scorePlayerOne.text = $"{playerOneName}: {PlayerPrefs.GetInt("ScoreP1")}";
            scorePlayerTwo.text = $"{playerTwoName}: {PlayerPrefs.GetInt("ScoreP2")}";
        }
    }

    public void BackToMainMenu()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("ScoreP1", 0);
        scorePlayerOne.text = PlayerPrefs.GetInt("ScoreP1").ToString();
        PlayerPrefs.SetInt("ScoreP2", 0);
        scorePlayerTwo.text = PlayerPrefs.GetInt("ScoreP2").ToString();
    }

    // COPY OF START METHOD IN CASE CALLING START CAUSES PROBLEMS

    //    void StartAlt()
    //    {
    //        playerSide = "X";
    //        turnCount = 1;
    //        turnTxt.text = $"Turn {turnCount}";

    //        for (int i = 0; i < textFields.Length; i++)
    //        {
    //            textFields[i].text = null;
    //            textFields[i].GetComponentInParent<Button>().interactable = true;
    //        }

    //        scorePlayerOne.text = PlayerPrefs.GetInt("ScoreP1").ToString();
    //        scorePlayerTwo.text = PlayerPrefs.GetInt("ScoreP2").ToString();
    //    }
}
