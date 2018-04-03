using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public Transform origin;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 axis = new Vector3(0, 1, 1);
        this.transform.RotateAround(origin.position, axis, 30 * Time.deltaTime);
    }
}
