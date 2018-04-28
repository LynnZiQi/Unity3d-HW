
using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class DiskData : MonoBehaviour

{
    public int UsedIndex { get; set; }

    public int score { get; set; }

    public bool isEnabled { get; set; }

    public int innerDiskCount { get; set; }



    public float size { get; set; }



    public Color color { get; set; }



    public float speed { get; set; }







    public SSAction currentSSAction; 


    public DiskData(Color tcolor, float tspeed, int score)
    {



        this.color = tcolor;

        this.speed = tspeed;

        this.score = score;

    }



    public void set(DiskData newData)
    {


        this.color = newData.color;

        this.speed = newData.speed;

        this.score = newData.score;




        Renderer render = this.transform.GetComponent<Renderer>();

        render.material.shader = Shader.Find("Transparent/Diffuse");

        render.material.color = this.color;

    }


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
        this.transform.localScale = new Vector3(2 + rules, 0.3f + rules, 2);

    }


}