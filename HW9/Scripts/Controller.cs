using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    float speed = 200;

    //表示屏幕是否被点击，点击之后才会有随重力移动的效果
    static bool isTouched = false;

	// Use this for initialization
	void Start () {
        isTouched = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount >= 1)
        {
            isTouched = true;
        }
        if (isTouched)
        {
            Vector3 mMovement = new Vector3(
                                            Input.acceleration.x * speed * Time.deltaTime,
                                            Input.acceleration.y * speed * Time.deltaTime);
            transform.Translate(mMovement);
        }

	}
}
