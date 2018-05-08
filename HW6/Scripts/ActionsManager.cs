using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsManager : MonoBehaviour
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

    protected void FixedUpdate()
    {
        foreach (SSAction ac in watingAddAction) dictionary[ac.GetInstanceID()] = ac;
        watingAddAction.Clear();


        foreach (KeyValuePair<int, SSAction> dic in dictionary)
        {
            SSAction ac = dic.Value;
            if (ac.destroy) watingDelete.Add(ac.GetInstanceID());
            else if (ac.enable) ac.FixedUpdate();
        }


        foreach (int id in watingDelete)
        {
            SSAction ac = dictionary[id];
            dictionary.Remove(id);
            DestroyObject(ac);
        }
        watingDelete.Clear();

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