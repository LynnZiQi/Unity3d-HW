using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFactory : MonoBehaviour {

    public GameObject PatrolObject;
    private static List<GameObject> inUsed;
    // 正在使用的对象链表
    private static List<GameObject> inFree;
    // 正在空闲的对象链表
    void Awake()
    {
        inUsed = new List<GameObject>();
        inFree = new List<GameObject>();
    }
    void Start()
    {
        inUsed.Clear();
        inFree.Clear();
    }

    public GameObject getPatrolObject(Vector3 targetposition, Quaternion faceposition)
    {
        if (inFree.Count <= .0)
        {
            GameObject newPatrol = Instantiate(Resources.Load("prefabs/Patrol"), targetposition, faceposition) as GameObject;
            inUsed.Add(newPatrol);
        }
        else
        {
            inUsed.Add(inFree[0]);
            inFree.RemoveAt(0);
            inUsed[inUsed.Count - 1].SetActive(true);
            inUsed[inUsed.Count - 1].transform.position = targetposition;
            inUsed[inUsed.Count - 1].transform.localRotation = faceposition;
        }
        return inUsed[inUsed.Count - 1];
    }

    public void FreeObject(GameObject obj)
    {
        obj.SetActive(false);
        inUsed.Remove(obj);
        inFree.Add(obj);
    }
}
