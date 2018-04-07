using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class CCActionManager : SSActionManager, ISSActionCallback
{
    public FirstSceneController scene;
    public MoveToAction horizontal, vertical;
    public SequenceAction saction;
    float speed = 30f;


    public void MoveBoat(GameObject boat)
    {
        Debug.Log("MoveBoat");
        horizontal = MoveToAction.getAction((boat.transform.position == new Vector3(4, 0, 0) ? new Vector3(-4, 0, 0) : new Vector3(4, 0, 0)), speed);
        this.Action(boat, horizontal, this);
    }


    public void getOnBoat(GameObject people, int shore, int seat)
    {
        Debug.Log("getOnBoat!!!");
        if (shore == 0 && seat == 0)
        {
            horizontal = MoveToAction.getAction(new Vector3(-5f, 2.7f, 0), speed);//右移
            vertical = MoveToAction.getAction(new Vector3(-5f, 1.2f, 0), speed);//下移
        }
        else if (shore == 0 && seat == 1)
        {
            horizontal = MoveToAction.getAction(new Vector3(-3f, 2.7f, 0), speed); //右移
            vertical = MoveToAction.getAction(new Vector3(-3f, 1.2f, 0), speed); //下移
        }
        else if (shore == 1 && seat == 0)
        {
            horizontal = MoveToAction.getAction(new Vector3(3f, 2.7f, 0), speed);//左移
            vertical = MoveToAction.getAction(new Vector3(3f, 1.2f, 0), speed);//下移
        }
        else if (shore == 1 && seat == 1)
        {

            horizontal = MoveToAction.getAction(new Vector3(5f, 2.7f, 0), speed);//左移
            vertical = MoveToAction.getAction(new Vector3(5f, 1.2f, 0), speed);//下移
        }

        SequenceAction saction = SequenceAction.getAction(0, 0, new List<SSAction> { horizontal, vertical });//将动作组合
        this.Action(people, saction, this);
    }


    public void getOffBoat(GameObject people, int shoreNum)
    {
        horizontal = MoveToAction.getAction(new Vector3(people.transform.position.x, 2.7f, 0), speed);//上移

        if (shoreNum == 0) vertical = MoveToAction.getAction(new Vector3(-16f + 1.5f * Convert.ToInt32(people.name), 2.7f, 0), speed);//左移
        else vertical = MoveToAction.getAction(new Vector3(16f - 1.5f * Convert.ToInt32(people.name), 2.7f, 0), speed);//右移

        SequenceAction saction = SequenceAction.getAction(0, 0, new List<SSAction> { horizontal, vertical });//将动作组合
        this.Action(people, saction, this);
    }

    protected void Start()
    {
        scene = (FirstSceneController)SSDirector.getInstance().currentScenceController;
        scene.actionManager = this;
    }

    protected new void Update()
    {
        base.Update();
    }

    public void actionDone(SSAction source)
    {
        //  Debug.Log("Done");
    }
}