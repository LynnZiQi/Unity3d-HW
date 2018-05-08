
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour
{
    private Dictionary<int, SSAction> dictionary;
    private List<SSAction> watingAddAction; 
    private List<int> watingDelete;

    void Awake()
    {
        dictionary = new Dictionary<int, SSAction>();
        watingAddAction = new List<SSAction>();
        watingDelete = new List<int>();
    }
    protected void Start()
    {

    }

    protected void Update()
    {
        foreach (SSAction ac in watingAddAction) dictionary[ac.GetInstanceID()] = ac;
        watingAddAction.Clear();
        // 将待加入动作加入dictionary执行

        foreach (KeyValuePair<int, SSAction> dic in dictionary)
        {
            SSAction ac = dic.Value;
            if (ac.destroy) watingDelete.Add(ac.GetInstanceID());
            else if (ac.enable) ac.Update();
        }
        // 如果要删除，加入要删除的list，否则更新

        foreach (int id in watingDelete)
        {
            SSAction ac = dictionary[id];
            dictionary.Remove(id);
            DestroyObject(ac);
        }
        watingDelete.Clear();
        // 将deletelist中的动作删除
    }

    public void runAction(GameObject gameObject, SSAction action, ISSActionCallback callback)
    {
        action.gameObject = gameObject;
        action.transform = gameObject.transform;
        action.callback = callback;
        watingAddAction.Add(action);
        action.Start();
    }
}

