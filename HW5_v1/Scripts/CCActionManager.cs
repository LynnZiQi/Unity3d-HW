using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class CCActionManager : SSActionManager, IActionManager, ISSActionCallback
{



    private DiskFactory factory;



    // Use this for initialization

    void Awake()
    {

        this.sceneController = Singleton<FirstSceneController>.Instance;

       factory = Singleton<DiskFactory>.Instance;

    }


    void Start()
    {

    }

    // Update is called once per frame

    protected new void Update()
    {

        base.Update();

    }



    public CCMoveToAction MoveToAction(GameObject obj, float speed)
    {

        CCMoveToAction action = CCMoveToAction.GetSSAction(speed);

        base.RunAction(obj, action, this);

        return action;

    }

    public void moveDisk()
    {

        GameObject diskObj = factory.getDiskCountObject(factory.getDiskCount(sceneController.round));

        this.MoveToAction(diskObj, sceneController.getSpeed());

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

    public void actionDone(SSAction source)
    {
       
    }


}