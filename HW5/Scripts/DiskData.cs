using UnityEngine;


public interface OnReachEndCallback
{

    void ReachEndCallback(DiskData disk);

}
public class DiskData : MonoBehaviour, OnReachEndCallback
{
    public int UsedIndex { get; set; }

    public int score { get; set; }

    public bool isEnabled { get; set; }

    public int innerDiskCount { get; set; }

    private float x, y, z;
    public Camera ca;
    //字段
    private Transform mmTransform;
    private Rigidbody mmRigidbody;

    Vector3 startPoint;
    //属性
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
      // this.transform.localScale = new Vector3(2 + rules, 0.3f, 2);

    }
    //开始事件 Awake(),Start()
    public void prepareFunc(int rules, int round) //setStart
    {
        this.isEnabled = true;
        setColor(rules);
        setScale(rules);
        //setShape(rules);

       startPoint = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(0,1.5f), -6f);
      //  Debug.Log(startPoint);
      //  Debug.Log(startPoint);
        this.score = 10 * rules; //不同round分数不同

        x = rules * 3;
        y = rules *3;
       z =  rules * 3;
        //   mmRigidbody.AddForce(new Vector3(x, y, z));

        /*飞碟出现和消失的位置*/
        // startPoint = Vector3.zero;

        // endPoint = getRandomEndPoint();
        //time = Vector3.Distance(startPoint, endPoint) / Speed;

        transform.position = startPoint;

        //speedDirection = new Vector3((endPoint.x - startPoint.x) / time, (endPoint.y - startPoint.y) / disappeareTime - 0.5f * g * time, (endPoint.z - startPoint.z) / time);

        //   Gravity = Vector3.zero;

    }
    void FixedUpdate()
    {

        //mmRigidbody.AddForce(new Vector3(0.2f, 0.3f, 0.3f));
        //   Debug.Log(mmRigidbody.position);
        if (isEnabled)
        {
            mmRigidbody.AddForce(new Vector3(x, y, z));
          
            //mmRigidbody.AddForce(new Vector3(5, 0, -5));
        }
        if (this.isGameEnded)
        {
           // Debug.Log("isGameEnd");
            reStart();
            ReachEndCallback(this);

        }
    
    }
    void Start()
    {
        //获取自身 Transform组件和Rigidbody组件的引用
        mmTransform = gameObject.GetComponent<Transform>();
        mmRigidbody = gameObject.GetComponent<Rigidbody>();
        //    Debug.Log(mmRigidbody);
        //mmRigidbody.position = new Vector3(5, 5, 5);
        mmRigidbody.velocity = new Vector3(0, 0, 5);
    }
    //更新事件，Update(),FixUpdate
    void Update()
    {

    }
    //方法

    private void PlayerMove()
    {
        //使用系统预设的w,a,s,d 控制Cube移动
        //    float h = Input.GetAxis("Horizontal");
        //    float v = Input.GetAxis("Vertical");
        //   Vector3 dir = new Vector3(h, 0, v);
        //刚体移动的特点：物体的位置+方向，太快就方向*一个小数，使之慢一点
        //  mmRigidbody.MovePosition(mmTransform.position + dir * 0.2f);

    }

    public static Vector3 getRandomStartPoint()

    {

        //   return new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 50f);
        return new Vector3(Random.Range(-1.5f, 1.5f), 1.5f, 12f);
    }
    public void reStart()
    {
  //  this.transform.position = new Vector3(1000, 1000, 1000);
        //speedDirection = Vector3.zero;

        //  Gravity = Vector3.zero;
        //    currentAngle = Vector3.zero;
    //    this.transform.eulerAngles = Vector3.zero;
        //     dtime = 0;
        isEnabled = false;

    }
 public bool IsAPointInACamera(Camera cam, Vector3 wordPos)
    {
        // 是否在视野内
        bool result1 = false;
        Vector3 posViewport = cam.WorldToViewportPoint(wordPos);
    //    Debug.Log("posViewport:" + posViewport.ToString());
        Rect rect = new Rect(0, 0, 1, 1);
        result1 = rect.Contains(posViewport);
     //   Debug.Log("result1:" + result1.ToString());
        // 是否在远近平面内
        bool result2 = false;
        if (posViewport.z >= cam.nearClipPlane && posViewport.z <= cam.farClipPlane)
        {
            result2 = true;
        }
    //    Debug.Log("result2:" + result2.ToString());
        // 综合判断
        bool result = result1 && result2;
        //    Debug.Log("result:" + result.ToString());
      //  Debug.Log(result);
        return result;
    }
    public bool isGameEnded
    {
        get
        {
            float a = mmRigidbody.position.x;
            float b = mmRigidbody.position.y;
            float c = mmRigidbody.position.z;
           // Debug.Log(mmRigidbody.position.y);
            //    return this.IsAPointInACamera(ca, this.mmRigidbody.position);
            if (a < -15 || a > 15)
                return false;
            if (b < -10 || b > 10)
                return false;
            if (c > 80)
                return false;
            return true;
        }
    }

    public void ReachEndCallback(DiskData disk)
    {
        Singleton<DiskFactory>.Instance.FreeDisk(disk);
    }
}