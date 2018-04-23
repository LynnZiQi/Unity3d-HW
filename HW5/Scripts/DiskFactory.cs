using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{

    public GameObject diskObject; //飞碟的预制
    public List<DiskData> inUsed;
    public List<DiskData> inFree;

    public int usedCount
    {

        get { return inUsed.Count; }
        set { }

    }
    private int diskCount = 0;
    public Camera camera;


    void Awake()
    {
        inUsed = new List<DiskData>();
        inFree = new List<DiskData>();


    }

    // Use this for initialization
    void Start()
    {
        diskCount = 0;
        inUsed.Clear();
        inFree.Clear();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
          //  Debug.Log("Fire Pressed");

            Vector3 mPosition = Input.mousePosition;
            Camera ca = camera.GetComponent<Camera>();
            Ray ray = ca.ScreenPointToRay(mPosition);


            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit))
            {
                DiskData disk = rayHit.collider.gameObject.GetComponent<DiskData>();
                //点中了要free
               disk.reStart();
                this.FreeDisk(disk);

                Singleton<ScoreRecorder>.Instance.Record(disk);
            }
        }
    }

    public int getDiskCount(int rules)

    {
        int round = 0;
        DiskData tempDisk;

        if (inFree.Count <= 0)
        {
            diskCount++;
            GameObject newDisk = Instantiate(Resources.Load("Disk")) as GameObject;
            tempDisk = newDisk.GetComponent<DiskData>();

            tempDisk.innerDiskCount = diskCount;
            tempDisk.prepareFunc(rules, round);
            inUsed.Add(tempDisk);
        }
        else
        {
            tempDisk = inFree.ToArray()[inFree.Count - 1];
            inFree.RemoveAt(inFree.Count - 1);
            tempDisk.prepareFunc(rules, round);
            inUsed.Add(tempDisk);
        }

        round++;
        return 0;
    }

    public void FreeDisk(DiskData disk)
    {
        DiskData tempDisk = null;

        foreach (DiskData d in inUsed)
        {
            if (d.innerDiskCount == disk.innerDiskCount)
            {
                tempDisk = d;
            }
        }

        if (tempDisk == null)
        {
         //   Debug.Log("系统异常");

        }
        else
        {
            tempDisk.reStart();
            inFree.Add(tempDisk);
            inUsed.Remove(tempDisk);
        }
    }
    public void FreeDisk(int totalDisk)
    {
        DiskData tempDisk = inUsed.ToArray()[totalDisk];
        if (tempDisk == null)
        {
    //        Debug.Log("系统异常");
        }
        else
        {

            inFree.Add(tempDisk);
            inUsed.Remove(tempDisk);
        }
    }

    public void FreeDisk() //freeall
    {
        for (int i = 0; i < inUsed.Count; i++)
        {
            DiskData temp = inUsed[i];
            FreeDisk(temp);
        }
    }
}

