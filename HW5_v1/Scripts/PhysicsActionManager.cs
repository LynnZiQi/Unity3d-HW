using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class PhysicsActionManager : SSActionManager, IActionManager, ISSActionCallback
{





    private DiskFactory factory;



    // Use this for initialization

    void Awake()
    {

        this.sceneController = Singleton<FirstSceneController>.Instance;

       factory = Singleton<DiskFactory>.Instance;

    }



    protected new void FixedUpdate()
    {

        if (sceneController.isPaused == true || sceneController.isStarted == false)

            return;

        base.FixedUpdate();

    }



    public PhysicsMoveToAction ApplyMoveToAction(GameObject obj, float speed)
    {

        PhysicsMoveToAction action = PhysicsMoveToAction.GetSSAction(speed);

        base.RunAction(obj, action, this);

        return action;

    }



    public void moveDisk()
    {

        GameObject diskObj = factory.getDiskCountObject(factory.getDiskCount(sceneController.round));

        this.ApplyMoveToAction(diskObj, sceneController.getSpeed());

    }

    public void clearActions()
    {

        base.ClearAction();

    }



    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Compeleted,

        int intParam = 0,

        string strParam = null,

        Object objectParam = null)
    {



        source.enable = false;

        source.destroy = true;

        source.gameObject.transform.position = source.originPosition;

    }

    public void actionDone(SSAction  source)
    {

    }


}