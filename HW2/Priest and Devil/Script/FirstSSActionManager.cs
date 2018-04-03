using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface ISSActionCallback
{
    void actionDone(SSAction source);
}


public class SSAction : ScriptableObject
{

    public bool Enable = true;
    public bool Destroy = false;

    public GameObject gameObject { get; set; }
    public Transform transform { get; set; }
    public ISSActionCallback callback { get; set; }

    public virtual void Start()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
}


public class MoveToAction : SSAction
{
    public Vector3 target;
    public float speed;

    private MoveToAction() { }
    public static MoveToAction getAction(Vector3 target, float speed)
    {
        MoveToAction action = ScriptableObject.CreateInstance<MoveToAction>();
        action.target = target;
        action.speed = speed;
        return action;
    }

    public override void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        if (this.transform.position == target)
        {
            this.Destroy = true;
            this.callback.actionDone(this);
        }
    }

    public override void Start() { }

}


public class SequenceAction : SSAction, ISSActionCallback
{
    public List<SSAction> sequence;
    public int repeat = -1; //-1表示无限循环
    public int currentAction = 0;

    public static SequenceAction getAction(int repeat, int currentActionIndex, List<SSAction> sequence)
    {
        SequenceAction action = ScriptableObject.CreateInstance<SequenceAction>();
        action.sequence = sequence;
        action.repeat = repeat;
        action.currentAction = currentActionIndex;
        return action;
    }

    public override void Update()
    {
        if (sequence.Count == 0) return;
        if (currentAction < sequence.Count)
        {
            sequence[currentAction].Update();
        }
    }

    public void actionDone(SSAction source)
    {
        source.Destroy = false;
        this.currentAction++;
        if (this.currentAction >= sequence.Count)
        {
            this.currentAction = 0;
            if (repeat > 0) repeat--;
            if (repeat == 0)
            {
                this.Destroy = true;
                this.callback.actionDone(this);
            }
        }
    }

    public override void Start()
    {
        foreach (SSAction action in sequence)
        {
            action.gameObject = this.gameObject;
            action.transform = this.transform;
            action.callback = this;
            action.Start();
        }
    }

    void OnDestroy()
    {
        foreach (SSAction action in sequence)
        {
            DestroyObject(action);
        }
    }
}


public class SSActionManager : MonoBehaviour
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> waitingToAdd = new List<SSAction>();
    private List<int> watingToDelete = new List<int>();

    protected void Update()
    {
        foreach (SSAction ac in waitingToAdd)
        {
            actions[ac.GetInstanceID()] = ac;
        }
        waitingToAdd.Clear();

        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            if (ac.Destroy)
            {
                watingToDelete.Add(ac.GetInstanceID());
            }
            else if (ac.Enable)
            {
                ac.Update();
            }
        }

        foreach (int key in watingToDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            DestroyObject(ac);
        }
        watingToDelete.Clear();
    }

    public void Action(GameObject gameObject, SSAction action, ISSActionCallback whoToNotify)
    {
        action.gameObject = gameObject;
        action.transform = gameObject.transform;
        action.callback = whoToNotify;
        waitingToAdd.Add(action);
        action.Start();
    }

}


public class FirstSSActionManager : SSActionManager, ISSActionCallback
{
    public FirstSceneController scene;
    public MoveToAction horizontal, vertical;
    public SequenceAction saction;
    float speed = 30f;


    public void moveBoat(GameObject boat)
    {
        horizontal = MoveToAction.getAction((boat.transform.position == new Vector3(4, 0, 0) ? new Vector3(-4, 0, 0) : new Vector3(4, 0, 0)), speed);
        this.Action(boat, horizontal, this);
    }


    public void getOnBoat(GameObject people, int shore, int seat)
    {
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