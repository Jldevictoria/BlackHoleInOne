using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalScore;    
    public int curLevel;
    public int totalLevels;

    private GameObject previousScoreText;
    private GameObject bestScoreText;
    private int previousScore;
    private int bestScore;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        totalScore = 0;
        curLevel = -1;

        //SceneManager.LoadScene("level_" + curLevel);
    }

    // Start is called before the first frame update
    void Start()
    {
        previousScoreText = GameObject.Find("PreviousScore");
        bestScoreText = GameObject.Find("BestScore");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextLevel(int levelScore)
    {
        totalScore += levelScore;
        curLevel += 1;

        if (curLevel < totalLevels)
        {
            print("Loading next level: level_" + curLevel + "!");
            SceneManager.LoadScene("level_" + curLevel);
        }
        else
        {
            previousScore = totalScore;
            if (previousScore < bestScore) {
                bestScore = previousScore;
            }
            totalScore = 0;

            SceneManager.LoadScene("lobby");

            // Update the score text in the lobby to reflect your best game!
            previousScoreText = GameObject.Find("PreviousScore");
            bestScoreText = GameObject.Find("BestScore");
            previousScoreText.GetComponent<UnityEngine.UI.Text>().text = previousScore.ToString();
            bestScoreText.GetComponent<UnityEngine.UI.Text>().text = bestScore.ToString();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("start");
        Destroy(this.gameObject);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
