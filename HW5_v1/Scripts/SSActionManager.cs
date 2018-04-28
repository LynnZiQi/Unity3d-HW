using System.Collections;

using System.Collections.Generic;

using UnityEngine;





public class SSActionManager : MonoBehaviour

{

    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();

    private List<SSAction> waitingAdd = new List<SSAction>();

    private List<int> waitingDelete = new List<int>();



    protected FirstSceneController sceneController;



    void Awake()
    {

        sceneController = Singleton<FirstSceneController>.Instance;

    }



    protected void Update()

    {

        if (sceneController.moveMode == FirstSceneController.MoveMode.CCMove)
        {

            if (sceneController.isPaused == false && sceneController.isStarted == true)
            {

                foreach (SSAction ac in waitingAdd)

                    actions[ac.GetInstanceID()] = ac;

                waitingAdd.Clear();



                foreach (KeyValuePair<int, SSAction> kv in actions)
                {

                    SSAction ac = kv.Value;

                    if (ac.destroy)
                    {

                        waitingDelete.Add(ac.GetInstanceID());

                    }
                    else if (ac.enable)
                    {

                        ac.Update();

                    }

                }



                foreach (int key in waitingDelete)
                {

                    SSAction ac = actions[key];

                    actions.Remove(key);

                    DestroyObject(ac);

                    sceneController.diskComp++;

                }

                waitingDelete.Clear();

            }

        }

    }



    protected void FixedUpdate()
    {

        if (sceneController.moveMode == FirstSceneController.MoveMode.PhysicsMove)
        {

            if (sceneController.isPaused == false && sceneController.isStarted == true)
            {

                foreach (SSAction ac in waitingAdd)

                    actions[ac.GetInstanceID()] = ac;

                waitingAdd.Clear();



                foreach (KeyValuePair<int, SSAction> kv in actions)
                {

                    SSAction ac = kv.Value;

                    if (ac.destroy)
                    {

                        waitingDelete.Add(ac.GetInstanceID());

                    }
                    else if (ac.enable)
                    {

                        ac.FixedUpdate();

                    }

                }



                foreach (int key in waitingDelete)
                {

                    SSAction ac = actions[key];

                    actions.Remove(key);

                    DestroyObject(ac);

                    sceneController.diskComp++;

                }

                waitingDelete.Clear();

            }

        }

    }



    public void ClearAction()
    {

        foreach (int key in waitingDelete)
        {

            SSAction ac = actions[key];

            actions.Remove(key);

            DestroyObject(ac);

        }

        waitingAdd.Clear();

        waitingDelete.Clear();

        actions.Clear();

    }



    public void RunAction(GameObject gameObject, SSAction action, ISSActionCallback manager)

    {

        action.gameObject = gameObject;

        action.callback = manager;

        gameObject.GetComponent<DiskData>().currentSSAction = action;

        waitingAdd.Add(action);

        action.Start();

    }



}