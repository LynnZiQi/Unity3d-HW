
using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public enum SSActionEventType : int

{

    Started,

    Compeleted

}







public class SSAction : ScriptableObject

{


    public bool enable = true;

    public bool destroy = false;

    public GameObject gameObject { get; set; }
    public Transform transform { get; set; }
    public ISSActionCallback callback { get; set; }





    public Vector3 originPosition = new Vector3(0, 0, -20);







    protected SSAction()

    {

    }



    // Use this for initialization

    public virtual void Start()

    {

        throw new System.NotImplementedException();

    }



    // Update is called once per frame

    public virtual void Update()

    {

        throw new System.NotImplementedException();

    }



    public virtual void FixedUpdate()

    {

        throw new System.NotImplementedException();

    }



    public static Vector3 getRandomStartPoint()

    {

        //   return new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 50f);
        return new Vector3(Random.Range(-1.5f, 1.5f), 1.5f, -12f);
    }



    public static Vector3 getRandomEndPoint()

    {

        //return new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        return new Vector3(Random.Range(-5f, 5f), Random.Range(3f, 8f), Random.Range(-5f, 5f));

    }

}