using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        source.destroy = false;
        this.currentAction++;
        if (this.currentAction >= sequence.Count)
        {
            this.currentAction = 0;
            if (repeat > 0) repeat--;
            if (repeat == 0)
            {
                this.destroy = true;
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