using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface OnReachEndCallback
{

    void ReachEndCallback(DiskData disk);

}

public class DiskData : MonoBehaviour, OnReachEndCallback {
    /*定义变量*/
    public int UsedIndex { get; set; }

    public int score { get; set; }

    public bool isEnabled{ get;set;}

    public int innerDiskCount { get; set; }

    private float disappeareTime;
    private float currentTime;

    private float Speed;

    private Vector3 startPoint;
    private Vector3 endPoint;

    private Vector3 Gravity = Vector3.zero;

    private const float g = -10f;  //重力加速度
                                   

    private Vector3 speedDirection;

    private Vector3 currentAngle;

    private float time;
    private float dtime;


    /*飞碟的一些样式设计*/


    /*随机生成一个颜色覆盖飞碟*/
    public static Color getRandomColor()

    {

        float R = Random.Range(0f, 1f);

        float G = Random.Range(0f, 1f);

        float B = Random.Range(0f, 1f);

        Color color = new Color(R, G, B);

        return color;

    }
    private void setColor(int rules)
    {
        Renderer render = this.transform.GetComponent<Renderer>();
        render.material.color = getRandomColor();
    }


    /*漫反射*/
    private void setShape(int rules)
    {
        Renderer render = this.transform.GetComponent<Renderer>();

        render.material.shader = Shader.Find("Transparent/Diffuse");
    }
    /*不同关卡飞碟形状，利用scale实现*/
    private void setScale(int rules)
    {
        
    }


    public void prepareFunc(int rules, int round) //setStart
    {
        this.isEnabled = true;
        setColor(rules);
        setScale(rules);
        //setShape(rules);

        disappeareTime = (int)(2f / Time.deltaTime);
        currentTime = 0;

        this.score = 10 * rules; //不同round分数不同
        this.Speed = 15f + 15f * rules;

        /*飞碟出现和消失的位置*/

        startPoint = getRandomStartPoint();
        endPoint = getRandomEndPoint();
        time = Vector3.Distance(startPoint, endPoint) / Speed;

        transform.position = startPoint;

        speedDirection = new Vector3((endPoint.x - startPoint.x) /time, (endPoint.y - startPoint.y)/disappeareTime - 0.5f*g*time, (endPoint.z - startPoint.z)/time);

        Gravity = Vector3.zero;

    }



    public void reStart()
    {
        this.transform.position = startPoint;
        speedDirection = Vector3.zero;
        currentTime = 0;
        Gravity = Vector3.zero;
        currentAngle = Vector3.zero;
        this.transform.eulerAngles = Vector3.zero;
        dtime = 0;
        isEnabled = false;

    }


    void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {

        if (isEnabled)
        {
            if (this.transform.position != endPoint)
            {
                currentTime++;
                Gravity.y = g * (dtime += Time.fixedDeltaTime);  //重力作用
                currentAngle.x = -Mathf.Atan((speedDirection.y + Gravity.y) / speedDirection.z) * Mathf.Rad2Deg; //一个三角形，求反切
                transform.eulerAngles = currentAngle;
                transform.position += (speedDirection + Gravity) * Time.fixedDeltaTime;

            }
        } 
        if (this.isGameEnded)
        {
            reStart();
            ReachEndCallback(this);
        }

	}








    public static Vector3 getRandomStartPoint()

    {

        //   return new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 50f);
        return new Vector3(Random.Range(-1.5f, 1.5f), 1.5f, -12f);
    }



    public static Vector3 getRandomEndPoint()

    {

        //return new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        return new Vector3(Random.Range(-5f, 5f), Random.Range(3f, 8f), Random.Range(-5f, 5f));

    }
    public bool isGameEnded
    {
        get
        {
            return this.currentTime >= disappeareTime;
        }
    }

    public void ReachEndCallback(DiskData disk)
    {
        Singleton<DiskFactory>.Instance.FreeDisk(disk);
    }
}
