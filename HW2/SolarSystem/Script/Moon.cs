using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = this.transform.parent.position;
        Debug.Log(position);
        GameObject.Find("Moon").transform.RotateAround(position, Vector3.up, 360 * Time.deltaTime);
       GameObject.Find("Moon").transform.Rotate(Vector3.up * Time.deltaTime * 10);
    }
}
