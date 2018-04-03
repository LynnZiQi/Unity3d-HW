using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour
{
    public Transform origin;
    public float speed = 15;
    float ry, rz;
    // Use this for initialization
    void Start()
    {
        ry = Random.Range(1, 360);
        rz = Random.Range(1, 360);
        Debug.Log(ry);
        Debug.Log(rz);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 axis = new Vector3(0, ry, rz);
        this.transform.RotateAround(origin.position, axis, speed * Time.deltaTime);
    }
}
