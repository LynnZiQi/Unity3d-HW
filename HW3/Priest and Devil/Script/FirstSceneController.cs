using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface ISceneController
{
    void LoadResources();

    //没来得及实现
    //Pause()
    //Resume()

}

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{

    public CCActionManager actionManager;

    List<GameObject> LeftObject = new List<GameObject>();
    List<GameObject> RightObject= new List<GameObject>();
    GameObject[] boat = new GameObject[2]; //只有两个空位

    GameObject boat_obj, leftShore_obj, rightShore_obj;

    Vector3 LeftShorePos = new Vector3(-12, 0, 0);
    Vector3 RightShorePos = new Vector3(12, 0, 0);
    Vector3 BoatLeftPos = new Vector3(-4, 0, 0);
    Vector3 BoatRightPos = new Vector3(4, 0, 0);

    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentScenceController = this;
        director.currentScenceController.LoadResources();


    }
    // Use this for initialization
    void Start()
    {

        Debug.Log("start");
        actionManager = GetComponent<CCActionManager>() as CCActionManager;
        Debug.Log(actionManager.GetType());
    }

    // Update is called once per frame
    void Update()
    {
        Judge();
    }

    public void LoadResources()
    {
        GameObject priest_obj, devil_obj;
        Camera.main.transform.position = new Vector3(0, 0, -20);


        leftShore_obj = Instantiate(Resources.Load("Prefabs/Shore"), LeftShorePos, Quaternion.identity) as GameObject;
        rightShore_obj = Instantiate(Resources.Load("Prefabs/Shore"), RightShorePos, Quaternion.identity) as GameObject;

        leftShore_obj.name = "left_shore";
        rightShore_obj.name = "right_shore";

        boat_obj = Instantiate(Resources.Load("Prefabs/Boat"), BoatLeftPos, Quaternion.identity) as GameObject;
        boat_obj.name = "boat";


        boat_obj.transform.parent = leftShore_obj.transform;

        for (int i = 0; i < 3; ++i)
        {
            priest_obj = Instantiate(Resources.Load("Prefabs/Priest")) as GameObject;
            priest_obj.name = i.ToString();//0~2号是牧师
            priest_obj.transform.position = new Vector3(-16f + 1.5f * Convert.ToInt32(priest_obj.name), 2.7f, 0);
            priest_obj.transform.parent = leftShore_obj.transform;
            LeftObject.Add(priest_obj);

            devil_obj = Instantiate(Resources.Load("Prefabs/Devil")) as GameObject;
            devil_obj.name = (i + 3).ToString();//3~5号是恶魔
            devil_obj.transform.position = new Vector3(-16f + 1.5f * Convert.ToInt32(devil_obj.name), 2.7f, 0);
            devil_obj.transform.parent = leftShore_obj.transform;
            LeftObject.Add(devil_obj);
        }
    }


    public void OnClick()
    {
        GameObject gameObj = null;

        if (Input.GetMouseButtonDown(0) &&
            (SSDirector.getInstance().state == State.START ))  //开始游戏之后点击才有效
        {
          //  Debug.Log(SSDirector.getInstance().state);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
               

                gameObj = hit.transform.gameObject;
                Debug.Log(gameObj.name);
            }
        }

        if (gameObj == null) return;
        else if (gameObj.name == "0" || gameObj.name == "1" || gameObj.name == "2"
            || gameObj.name == "3" || gameObj.name == "4" || gameObj.name == "5")
        {
            Debug.Log(gameObj.name);
            MovePeople(gameObj);  //按照对应编号确定操作游戏对象
        }
        else if (gameObj.name == "boat")
        {
            Debug.Log("name==boat");
            MoveBoat();
        }
    }


    void MovePeople(GameObject people)
    {
        Debug.Log(people.GetType());
        Debug.Log("MovePeople");
        int shoreNum, seatNum;//0为左，1为右

        if (people.transform.parent == boat_obj.transform.parent && (boat[0] == null || boat[1] == null))//物体和船都在同一个岸且船有空位才能上船
        {
            seatNum = boat[0] == null ? 0 : 1;
            Debug.Log("SeatNum=="+seatNum);
            if (people.transform.parent == leftShore_obj.transform)
            {
                shoreNum = 0;
                for (int i = 0; i < LeftObject.Count; i++)
                {
                    if (people.name == LeftObject[i].name)  //左边上船
                    {
                        Debug.Log("左边上船");
                        Debug.Log(people.GetType());
                        Debug.Log(actionManager.GetType());
                        actionManager.getOnBoat(people, shoreNum, seatNum); //利用ActionManager
                        LeftObject.Remove(LeftObject[i]); //上船之后游戏对象减少
                    }
                }
            }
            else
            {
                shoreNum = 1;
                for (int i = 0; i <RightObject.Count; i++)
                {
                    if (people.name ==RightObject[i].name) //右边上船
                    {
                        Debug.Log("右边上船");
                        actionManager.getOnBoat(people, shoreNum, seatNum);
                       RightObject.Remove(RightObject[i]);  //上船之后游戏对象减少
                    }
                }
            }
            Debug.Log("赋值Boat");
            boat[seatNum] = people;
            Debug.Log(boat[seatNum]);
            Debug.Log(boat[seatNum].name);
            people.transform.parent = boat_obj.transform;
        }
        else if (people.transform.parent == boat_obj.transform)//点击时物体在船上，选择上岸动作
        {
            shoreNum = boat_obj.transform.parent == leftShore_obj.transform ? 0 : 1;
            seatNum = (boat[0] != null && boat[0].name == people.name) ? 0 : 1;

            actionManager.getOffBoat(people, shoreNum); //利用ActionManager

            boat[seatNum] = null;
            if (shoreNum == 0)
            {
                people.transform.parent = leftShore_obj.transform;
                LeftObject.Add(people);   //左岸对象数目增加
            }
            else
            {
                people.transform.parent = rightShore_obj.transform;
               RightObject.Add(people); //右岸对象数增加
            }
        }
    }


    void MoveBoat()
    {
        Debug.Log("MoveBoatFunc");
        Debug.Log(boat[0]);
        if (boat[0] != null || boat[1] != null)
        {
            Debug.Log("boat!=null");
            actionManager.MoveBoat(boat_obj);

            boat_obj.transform.parent = boat_obj.transform.parent == leftShore_obj.transform ?
                rightShore_obj.transform : leftShore_obj.transform; //确定在哪一边
        }
    }


    public void Judge()
    {
        int left_d = 0, left_p = 0, right_d = 0, right_p = 0;
       // Debug.Log("Judge");
        foreach (GameObject element in LeftObject)
        {
            if (element.tag == "Priest") left_p++;
            if (element.tag == "Devil") left_d++;
        }

        foreach (GameObject element in RightObject)
        {
            if (element.tag == "Priest") right_p++;
            if (element.tag == "Devil") right_d++;
        }

        for (int i = 0; i < 2; i++)
        {
            if (boat[i] != null && boat_obj.transform.parent == leftShore_obj.transform)//船在左岸
            {
                if (boat[i].tag == "Priest") left_p++;
                else left_d++;
            }
            if (boat[i] != null && boat_obj.transform.parent == rightShore_obj.transform)//船在右岸
            {
                if (boat[i].tag == "Priest") right_p++;
                else right_d++;
            }
        }

        if ((left_d > left_p && left_p != 0) || (right_d > right_p && right_p != 0) )
        {
            SSDirector.getInstance().state = State.LOSE;//恶魔数量大于牧师数量，游戏失败

        }
        else if (right_d == right_p && right_d == 3)//全过河，赢了
        {
            //Debug.Log(right_d);
            SSDirector.getInstance().state = State.WIN;
        }
    }


}