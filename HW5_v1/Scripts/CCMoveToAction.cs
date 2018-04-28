using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class CCMoveToAction : SSAction

{

   // public Vector3 target;

    public float Speed;



    private float disappeareTime;
    private float currentTime;



    private float shotSpeed;


    private Vector3 startPoint;
    private Vector3 endPoint;

    private Vector3 Gravity = Vector3.zero;
    private const float g = -10f;  //重力加速度


    private Vector3 speedDirection;
    private Vector3 currentAngle;

    private float time;
    private float dTime;



    public bool isGameEnded
    {
        get
        {
            return this.currentTime >= disappeareTime;
        }
    }



    public override void Start()

    {

        disappeareTime = (int)(2f / Time.deltaTime);

        currentTime = 0;
        dTime = 0;
        this.enable = true;



        /*飞碟出现和消失的位置*/

        startPoint = getRandomStartPoint();
        endPoint = getRandomEndPoint();
        time = Vector3.Distance(startPoint, endPoint) / Speed;

        this.gameObject.transform.position = startPoint;

        speedDirection = new Vector3((endPoint.x - startPoint.x) / time, (endPoint.y - startPoint.y) / disappeareTime - 0.5f * g * time, (endPoint.z - startPoint.z) / time);

        Gravity = Vector3.zero;

    }



    public static CCMoveToAction GetSSAction(float speed)

    {

        CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction>();

        action.Speed = speed;

        return action;

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public override void Update()

    {


            if (enable)
            {
                if (this.gameObject.transform.position != endPoint)
                {
                    currentTime++;
                    Gravity.y = g * (dTime += Time.fixedDeltaTime);  //重力作用
                    currentAngle.x = -Mathf.Atan((speedDirection.y + Gravity.y) / speedDirection.z) * Mathf.Rad2Deg; //一个三角形，求反切
                    this.gameObject.transform.eulerAngles = currentAngle;
                    this.gameObject.transform.position += (speedDirection + Gravity) * Time.fixedDeltaTime;
                }
            }
       

        if (this.isGameEnded)
        {

            reStart();

            this.callback.SSActionEvent(this);

        }

    }



    public void reStart()
    {

        this.gameObject.transform.position = startPoint;
        speedDirection = Vector3.zero;
        Gravity = Vector3.zero;
        currentAngle = Vector3.zero;
        this.gameObject.transform.eulerAngles = currentAngle;
        currentTime = 0;
        dTime = 0;
        this.enable = false;

    }



}