using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text ScoreText;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private Text TotalScoreText;

    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreasePoint()
    {
        score += 10;
        ScoreText.text = score.ToString();
    }

    public void DecreasePoint()
    {
        score -= 5;
        ScoreText.text = score.ToString();
    }

    public void PlayAgain()
    {
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void ActivateGameOverUI()
    {
        TotalScoreText.text = "Total Score: " + score.ToString();
        GameOverUI.SetActive(true);
    }
}
