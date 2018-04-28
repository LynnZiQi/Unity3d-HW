using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class DiskFactory : MonoBehaviour

{


    public GameObject diskObject;

    private List<DiskData> inUsed;

    private List<DiskData> inFree;

    private int diskCount = 0;



    private FirstSceneController sceneController;



    void Awake()

    {

        inUsed = new List<DiskData>();

        inFree = new List<DiskData>();

        sceneController = Singleton<FirstSceneController>.Instance;

    }

    void Start()
    {
        diskCount = 0;
        inUsed.Clear();
        inFree.Clear();
    }

    public int getDiskCount(int rules)
    {


        DiskData tempDisk;
        if (inFree.Count <= .0)
        {
            diskCount++;
            GameObject newDisk = Instantiate(Resources.Load("Disk")) as GameObject;
            Rigidbody mmRigibody= newDisk.GetComponent<Rigidbody>();
            if (sceneController.moveMode == FirstSceneController.MoveMode.CCMove)
            {
                mmRigibody.isKinematic = true;
            } else
            {
                mmRigibody.isKinematic = false;
            }
            newDisk.name = "Disk" + diskCount.ToString();
            tempDisk = newDisk.GetComponent<DiskData>();
            tempDisk.innerDiskCount = diskCount;

        } else
        {
            tempDisk = inFree.ToArray()[inFree.Count - 1];
            Rigidbody mmRigibody = tempDisk.gameObject.GetComponent<Rigidbody>();
            if(sceneController.moveMode == FirstSceneController.MoveMode.CCMove)
            {
                mmRigibody.isKinematic = true;

            } else
            {
                mmRigibody.isKinematic = false;
            }
            inFree.RemoveAt(inFree.Count - 1);
        }
        tempDisk.set(getDiskCountDataByRound(rules));

        inUsed.Add(tempDisk);



        return tempDisk.UsedIndex = inUsed.Count - 1;

    }


    public GameObject getDiskCountObject(int id)
    {
        return inUsed[id].gameObject;
    }



    public void Free(int id)
    {

        DiskData tempDisk = inUsed[id];

        if (tempDisk == null)
        {

            Debug.Log("系统异常");

        }
        else
        {

            inFree.Add(tempDisk);

            inUsed.Remove(tempDisk);

        }

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
            Debug.Log("系统异常");

        }
        else
        {

            inFree.Add(tempDisk);
           inUsed.Remove(tempDisk);
        }
    }
    public void FreeDisk(int totalDisk)
    {
        DiskData tempDisk = inUsed.ToArray()[totalDisk];
        if (tempDisk == null)
        {
            Debug.Log("系统异常");
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
    public void FreeAllDisks()

    {

        for (int i = inUsed.Count - 1; i >= 0; i--)
        {

            DiskData disk = inUsed[i];

            inFree.Add(disk);

            inUsed.Remove(disk);

        }



        for (int i = 0; i < inFree.Count; i++)
        {

            inFree[i].transform.position = new Vector3(3f * i, 3f * i, -20);

            inFree[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        }

    }





    private static DiskData getDiskCountDataByRound(int rules)
    {





        Color tcolor = getRandomColor();
        float speed = 10f + 5f * rules;
        int score = 10 * rules;

        

        return new DiskData(tcolor, speed, score);

    }
    /*随机生成一个颜色覆盖飞碟*/
    public static Color getRandomColor()

    {

        float R = Random.Range(0f, 1f);

        float G = Random.Range(0f, 1f);

        float B = Random.Range(0f, 1f);

        Color color = new Color(R, G, B);

        return color;

    }

}

