using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMP_Text distanceText;
    public TMP_InputField nameInputField;
    public Button saveRecordButton;
    public Button leaderBoarButton;
    //public Button StarGameButton;

    private float finalDistance;

    void Start()
    {
        gameOverPanel.SetActive(false);
        saveRecordButton.onClick.AddListener(SubmitScore);
        leaderBoarButton.onClick.AddListener(LeaderBoardScene);
    }

    public void ShowGameOverScreen(float distance)
    {
        finalDistance = distance;
        distanceText.text = (distance / 100f).ToString("F2") + "KM";
        gameOverPanel.SetActive(true);

    }

    private void SubmitScore()
    {
        string player = nameInputField.text;
        LeaderboardManager.Instance.AddScore(player, finalDistance);
        gameOverPanel.SetActive(false);
        RestartGame();
    }

    private void RestartGame()
    {
        //Carga la misma escena que se encuentra
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LeaderBoardScene()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
