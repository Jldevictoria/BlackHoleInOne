using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int levelScore;
    public int levelPar;

    public GameObject startPlanet;
    public GameObject endPlanet;
    public GameObject moonBall;
    public GameObject scoreText;
    public GameObject curPlanet;
    public GameObject boundary;

    public Dictionary<string, Vector3> curState;

    private GameController gameController;
    private OrbitObject moonBallOrbit;
    private float curDirection;
    private Vector3 curPosition;
    private GameObject curBody;
    private float curRadius;

    // Start is called before the first frame update
    void Start()
    {
        moonBallOrbit = moonBall.GetComponent<OrbitObject>();
        levelScore = 0;
        startPlanet = moonBallOrbit.targetBody;
        curPlanet = startPlanet;
        curState = new Dictionary<string, Vector3>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        scoreText = GameObject.Find("GolfScore");
        scoreText.GetComponent<UnityEngine.UI.Text>().text = levelScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText != null) {
            levelScore = System.Convert.ToInt32(scoreText.GetComponent<UnityEngine.UI.Text>().text);
        }
        curPlanet = moonBall.GetComponent<OrbitObject>().targetBody;

        if (curPlanet == endPlanet)
        {
            print("You delivered the moon in " + levelScore + " shots!");
            if (levelScore < levelPar)
            {
                print("You were under par (" + levelPar + ")!");
            }
            else if (levelScore > levelPar)
            {
                print("You were over par (" + levelPar + ")!");
            }
            else if (levelScore == levelPar)
            {
                print("You were at par (" + levelPar + ")!");
            }
            else
            {
                // do nothing
            }
            gameController.NextLevel(levelScore);
        }
    }

    public void UpdateLevelScore()
    {
        levelScore += 1;
    }

    public void SaveState()
    {
        // Handle all planets
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("planet");
        foreach (GameObject astralBody in allObjects)
        {
            //print("Object list:");
            //print(astralBody.name);
            if (curState.ContainsKey(astralBody.name))
            {
                curState[astralBody.name] = astralBody.transform.position;
            }
            else
            {
                curState.Add(astralBody.name, astralBody.transform.position);
            }
            //print(curState[astralBody.name]);
        }

        // Handle Moon
        curBody = moonBallOrbit.targetBody;
        curDirection = moonBallOrbit.direction;
        curPosition = moonBall.transform.position;
        curRadius = moonBallOrbit.radius;
    }

    public void LoadState()
    {
        // Handle all planets        
        foreach (KeyValuePair<string, Vector3> entry in curState)
        {
            GameObject astralBody = GameObject.Find(entry.Key);
            //print(astralBody.transform.position);
            //print(astralBody.transform.localPosition);
            astralBody.transform.localPosition = entry.Value;
            //print("Load List:");
            //print(astralBody.name);
            //print(curState[astralBody.name]);
            //print(astralBody.transform.position);
        }
        // Clear dict
        curState = new Dictionary<string, Vector3>();
    }

    public void OutOfBounds()
    {
        UpdateLevelScore();
        // Handle Moon
        moonBallOrbit.changeTargetBody(curBody, curDirection, curRadius);
        moonBall.transform.position = new Vector3(curBody.transform.position.x + 10, curBody.transform.position.y + 10);

        // Orbit Ring
        moonBallOrbit.changeTargetBody(curBody, 1.0f, 5);
    }
}
