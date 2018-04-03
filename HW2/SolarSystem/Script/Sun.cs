using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject.Find("Sun").transform.Rotate(Vector3.up * Time.deltaTime * 10);
        GameObject.Find("Earth").transform.Rotate(Vector3.up * Time.deltaTime * 10);
        Debug.Log("hello");
    }
}
