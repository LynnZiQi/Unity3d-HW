using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceAction : SSAction, ISSActionCallback
{
    public List<SSAction> sequence;
    public int repeat = -1;
    public int start = 0;

    public static SequenceAction GetSSAction(List<SSAction> _sequence, int _start = 0, int _repead = 1)
    {
        SequenceAction actions = ScriptableObject.CreateInstance<SequenceAction>();
        actions.sequence = _sequence;
        actions.start = _start;
        actions.repeat = _repead;
        return actions;
    }

    public override void Start()
    {
        foreach (SSAction ac in sequence)
        {
            ac.gameObject = this.gameObject;
            ac.transform = this.transform;
            ac.callback = this;
            ac.Start();
        }
    }

    public override void Update()
    {
        if (sequence.Count == 0) return;
        if (start < sequence.Count) sequence[start].Update();
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Compeleted,
        int intParam = 0, string strParam = null, Object objParam = null) //通过对callback函数的调用执行下个动作
    {
        source.destroy = false;
        this.start++;
        if (this.start >= this.sequence.Count)
        {
            this.start = 0;
            if (this.repeat > 0) this.repeat--;
            if (this.repeat == 0)
            {
                this.destroy = true;
                this.callback.SSActionEvent(this);
            }
        }
    }

    private void OnDestroy()
    {
        this.destroy = true;
    }
}