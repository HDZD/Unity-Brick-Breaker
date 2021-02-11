using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {
    //Script that controls the movement of the Paddle
    void Update() {
        //Use mouse input when in unity editor, and touch count when on mobile
        #if UNITY_EDITOR
        if (Input.GetMouseButton(0)){
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(pos.x - transform.position.x,0,0) / 3;
        }
        #else
        if (Input.touchCount > 0){
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            transform.position += new Vector3(pos.x - transform.position.x,0,0) / 3;
        }
        #endif
    }
}
