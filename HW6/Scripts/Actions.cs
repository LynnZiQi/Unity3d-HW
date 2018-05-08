using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Actions : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
public class IdleAction : SSAction
{
    private float time;
    private Animator ani;

    public override void Start()
    {
        ani.SetFloat("Speed", 0);

    }

    public static IdleAction GetIdleAction(float time, Animator ani)
    {
        IdleAction currentAction = ScriptableObject.CreateInstance<IdleAction>();
        currentAction.time = time;
        currentAction.ani = ani;
        return currentAction;
    }



    public override void Update()
    {
        if (time == -1) return;

        time -= Time.deltaTime;

        if (time < 0)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }
}

public class WalkAction : SSAction
{
    private float speed;
    private Vector3 target;
    private Animator ani;

    public override void Start()
    {
        ani.SetFloat("Speed", 0.5f);

    }

    public static WalkAction GetWalkAction(Vector3 target, float speed, Animator ani)
    {
        WalkAction currentAction = ScriptableObject.CreateInstance<WalkAction>();
        currentAction.speed = speed;
        currentAction.target = target;
        currentAction.ani = ani;
        return currentAction;
    }


    public override void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(target - transform.position);
        if (transform.rotation != rotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed * 5);


        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        if (this.transform.position == target)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }
}

public class RunAction : SSAction
{
    private float speed;
    private Transform target;
    private Animator ani;


    public static RunAction GetRunAction(Transform target, float speed, Animator ani)
    {
        RunAction currentAction = ScriptableObject.CreateInstance<RunAction>();
        currentAction.speed = speed;
        currentAction.target = target;
        currentAction.ani = ani;
        return currentAction;
    }

    public override void Start()
    {
        ani.SetFloat("Speed", 1f);
    }

    public override void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        if (transform.rotation != rotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed * 5);


        this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
        if (Vector3.Distance(this.transform.position, target.position) < 0.5)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }
}
