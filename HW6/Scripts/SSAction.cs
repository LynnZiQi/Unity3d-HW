
using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public enum SSActionEventType : int

{

    Started,

    Compeleted

}


public class SSAction : ScriptableObject // 动作的基类
{
    public bool enable = true;
    public bool destroy = false;

    public GameObject gameObject { get; set; }
    public Transform transform { get; set; }
    public ISSActionCallback callback { get; set; }

    public virtual void Start()
    {
        throw new System.NotImplementedException("Action Start Error!");
    }

    public virtual void FixedUpdate()
    {
        throw new System.NotImplementedException("Physics Action Start Error!");
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException("Action Update Error!");
    }
}

