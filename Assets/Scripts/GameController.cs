using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalScore;    
    public int curLevel;
    public int totalLevels;

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
            SceneManager.LoadScene("game_over");
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
