using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalScore;    
    public int curLevel;
    public int totalLevels;

    public GameObject previousScoreText;
    public GameObject bestScoreText;
    private int previousScore;
    private int bestScore;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (GameObject.FindGameObjectsWithTag("GameController").Length > 1)
        {
            GameObject curController = GameObject.FindGameObjectsWithTag("GameController")[0];
            bestScore = curController.GetComponent<GameController>().bestScore;
            previousScore = curController.GetComponent<GameController>().previousScore;
            Destroy(curController);
        }
        else
        {
            bestScore = 9999;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;
        curLevel = -1;

        previousScoreText = GameObject.Find("PreviousScore");
        bestScoreText = GameObject.Find("BestScore");

        // Update the score text in the lobby to reflect your best game!
        previousScoreText.GetComponent<UnityEngine.UI.Text>().text = previousScore.ToString();
        bestScoreText.GetComponent<UnityEngine.UI.Text>().text = bestScore.ToString();

        //SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextLevel(int levelScore)
    {
        totalScore += levelScore;
        curLevel += 1;

        print(bestScore);
        print(previousScore);
        print(totalScore);
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

            print(bestScore);
            print(previousScore);
            print(totalScore);
            SceneManager.LoadScene("lobby");
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

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (next.name == "lobby")
        {
            print(bestScore);
            print(previousScore);
            print(totalScore);
            Start();
            print(bestScore);
            print(previousScore);
            print(totalScore);
        }
    }
}

