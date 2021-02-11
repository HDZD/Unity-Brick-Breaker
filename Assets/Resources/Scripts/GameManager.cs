using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    public int poolSize;
    public int brickNumberX,brickNumberY;
    GameObject [,]bricks;
    public static int activeBricks,currentScore;
    static TMP_Text scoreText;
    GameObject LoseScreen;
    void Start() {
        //Scale the orthographic perspective of the camera so the objects have
        //consistent sizes across multiple resolutions
        Camera.main.orthographicSize = 2.9f * Screen.height / Screen.width;

        LoseScreen = GameObject.Find("LoseScreen");
        scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        bricks = new GameObject[brickNumberX,brickNumberY];
        BallPooler.Initialize(poolSize);
        LoseScreen.SetActive(false);

        InitializeBoundaries();
        InstantiateBricks();
    }

    //Initializing boundaries like this allows the game to be true full screen regardless of
    //device aspect ratio or resolution
    void InitializeBoundaries() {
        //Get screen width/height in world unit scale
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));

        //Create Upper boundary
        Instantiate(Resources.Load<GameObject>("Prefabs/Boundary"),new Vector3(0,stageDimensions.y,0),Quaternion.identity).transform.localScale = new Vector3(2*stageDimensions.x,0.1f,0);
        //Create Right boundary
        Instantiate(Resources.Load<GameObject>("Prefabs/Boundary"),new Vector3(stageDimensions.x,0,0),Quaternion.identity).transform.localScale = new Vector3(0.1f,2*stageDimensions.y,0);
        //Create Left boundary
        Instantiate(Resources.Load<GameObject>("Prefabs/Boundary"),new Vector3(-stageDimensions.x,0,0),Quaternion.identity).transform.localScale = new Vector3(0.1f,2*stageDimensions.y,0);
        //Create Lower boundary
        GameObject lb = Instantiate(Resources.Load<GameObject>("Prefabs/Boundary"),new Vector3(0,-stageDimensions.y,0),Quaternion.identity);
        lb.transform.localScale = new Vector3(2*stageDimensions.x,0.1f,0);
        lb.gameObject.name = "LowerBound";
    }

    void Update (){
        //If you clear the bricks on screen, you get new bricks
        if (activeBricks == 0)
            NewRound();

        //If you lose all balls, game over
        if (BallPooler.ballCount == 0){
            LoseScreen.SetActive(true);
            PlayerPrefs.SetInt("HighScore",currentScore);
            PlayerPrefs.Save();
            GameObject.Find("FinalScoreText").GetComponent<TMP_Text>().text = "Score: " + currentScore;
        } else
            LoseScreen.SetActive(false);
    }

    public void GoMainMenu() {
        SceneManager.LoadScene("Menu");
    }

    void InstantiateBricks() {
        activeBricks = 0;
        GameObject brick = Resources.Load<GameObject>("Prefabs/Brick");
        GameObject brickParent = GameObject.Find("BrickParent");
        float brickWidth = brick.transform.localScale.x;
        float brickHeight = brick.transform.localScale.y;
        //Instantiate a grid of bricks
        for (int i = 0;i < brickNumberX;i++)
            for (int j = 0;j < brickNumberY;j++){
                bricks[i,j] = Instantiate(brick,new Vector3(i*brickWidth - brickWidth*(float)brickNumberX/2 + brickWidth/2,j*brickHeight-0.5f,0),Quaternion.identity,brickParent.transform);
                bricks[i,j].SetActive(false);
            }
    }

    void NewRound () {
        //Give the player a new Ball
        BallPooler.CreateBall();

        //Turn on a random selection of bricks
        for (int i = 0;i < brickNumberX;i++)
            for (int j = 0;j < brickNumberY;j++)
                if (Random.Range(0,2) == 0){
                    bricks[i,j].SetActive(true);
                    activeBricks++;
                }
    }

    public static void KillBrick(){
        GameManager.activeBricks--;
        GameManager.currentScore++;
        scoreText.text = "Score: "+ currentScore;
    }
}