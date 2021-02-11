using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPooler : MonoBehaviour {
    public static GameObject ballPool;
    static GameObject [] availableBalls;
    public static int ballCount;

    public static void Initialize (int poolSize){
        //Only allow initialization to happen once
        if (ballPool != null) return;
        
        ballPool = GameObject.Find("BallPool");
        availableBalls = new GameObject[poolSize];
        GameObject ball = Resources.Load<GameObject>("Prefabs/Ball");

        //Create a number of balls (poolSize amount) and disable all of them
        for (int i = 0;i < poolSize;i++){
            availableBalls[i] = Instantiate(ball,Vector3.zero,Quaternion.identity,ballPool.transform);
            availableBalls[i].SetActive(false);
        }
    }

    public static void CreateBall() {
        //Find the first available ball in the pool and enable it
        for (int i = 0;i < availableBalls.Length;i++)
            if (!availableBalls[i].activeSelf){
                ballCount++;
                availableBalls[i].SetActive(true);
                return ;
            }
        
        //Nothing will be done if the max number of balls (poolSize) is already active
    }
}
