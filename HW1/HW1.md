
**作业内容**

**1、简答题**

**解释 游戏对象（GameObjects） 和 资源（Assets）的区别与联系。**

Asset是存储在硬盘上的文件，保存在Unity项目的Assets文件夹内。资源一定可以存在磁盘上，对象是运行期的东西。
资源可以被对象使用，资源中包含除了对象之外的如场景、脚本等素材，不一定直接出现在游戏场景中。对象直接出现在游戏中，像玩家、NPC等等。
   Assets和Objects之间存在一对多的关系：也就是说，Asset文件内能够包含一个或多个Objects。

----------


 

**下载几个游戏案例，分别总结资源、对象组织的结构（指资源的目录组织结构与游戏对象树的层次结构）**

该案例是Unity官方Example，Car
由图可知，资源的目录结构包括脚本、声音、材料等等，对象则是具体的参与游戏互动的东西。

![Assets](https://raw.githubusercontent.com/LynnZiQi/Unity3D/master/image/QQ截图20180322235333.jpg)

![对象树](https://raw.githubusercontent.com/LynnZiQi/Unity3D/master/image/QQ截图20180322235411.jpg)

----------


**编写一个代码，使用 debug 语句来验证 [MonoBehaviour](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) 基本行为或事件触发的条件**

- 基本行为包括 Awake() Start() Update() FixedUpdate() LateUpdate()
- 常用事件包括 OnGUI() OnDisable() OnEnable()
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    void Awake()
    {
        Debug.Log("Awake");
        this.enabled = true;
    }
 void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }
    void LateUpdate()
    {
        Debug.Log("LateUpdate");
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Start");
    }
	
	// Update is called once per frame
void Update () {
   Debug.Log("Update");
}
    

    //常用事件
void OnGUI()
    {
        Debug.Log("OnGUI");
    }
    void OnDisable()
    {
       // this.enabled = true;
        Debug.Log("OnDisable");
    }
    void OnEnable()
    {

        Debug.Log("OnEnable");
               this.enabled = true;
    }
}

```
实验结果截图：

![](https://raw.githubusercontent.com/LynnZiQi/Unity3D/master/image/常用.jpg)

![](https://raw.githubusercontent.com/LynnZiQi/Unity3D/master/image/常用2.jpg)

结果分析：
最先执行的方法是Awake，这是生命周期的开始。当前脚本处于可用状态，会顺序执行OnEnable，如果此时Start方法没被执行，则执行一次。然后就是更新的相关操作，先是Update,然后再FixUpdate，最后LateUpdate。“如果后面写了Reset，则会又回到Update，在这4个事件间可以进行循环流动。”再向后，就进入渲染模块。此时会有GUI相关，执行OnGUI，最后，结束脚本，OnDisable.

关于最后的结束阶段，比较完整的应该是：

> 卸载模块（TearDown），这里主要有两个方法OnDisable与OnDestroy。当被禁用(enable=false)时，会执行OnDisable方法，但是这个时候，脚本并不会被销毁，在这个状态下，可以重新回到OnEnable状态（enable=true）。当手动销毁或附属的游戏对象被销毁时，OnDestroy才会被执行，当前脚本的生命周期结束。




----------


**查找脚本手册，了解 [GameObject](https://docs.unity3d.com/ScriptReference/GameObject.html)，Transform，Component 对象**

GameObject:所有实体在Unity场景中的基类。

Transform：控制物体的位置、旋转和缩放。

Component：所有附加到GameObject的基类。
    

 
2.描述下图中 table 对象（实体）的属性、table 的 Transform 的属性、 table 的部件
- 本题目要求是把可视化图形编程界面与 Unity API 对应起来，当你在 Inspector 面板上每一个内容，应该知道对应 API。例如：table 的对象是 GameObject，第一个选择框是 activeSelf 属性。
- 用 UML 图描述 三者的关系（请使用 UMLet 14.1.1 stand-alone版本出图）

![workwork](https://raw.githubusercontent.com/pmlpml/unity3d-learning/gh-pages/images/ch02/ch02-homework.png)
table的Transform属性

 1. 位置（position）：以X、Y、Z坐标系表示变换的位置。 
 2. 旋转（rotation）：表示此变换以X，Y，Z轴为准的旋转程度，以角度为单位。 
 3. 缩放（scale）：沿着X，Y，Z轴缩放此变换。值为”1”时表示原始尺寸（物体最初被导入时的大小）

Component菜单包括

 - Mesh
 - Effects
 - Physics
 - Physics 2d
 - Navigation
 - Audio
 - Rendering
 - Miscellaneous
 - Scripts
 - Airplane



 图中体现了Mesh这个组件，是用来添加网格类型的
 
 - Mesh Filter：网格过滤器
 - Mesh Filter：网格过滤器
 - Mesh Renderer：网格渲染器

 此外，还有Collider（碰撞组件），因为系统默认会给每个对象(GameObject)添加一个碰撞组件(ColliderComponent)


UML图：

![](https://raw.githubusercontent.com/LynnZiQi/Unity3D/master/image/UML.jpg)


----------


**整理相关学习资料，编写简单代码验证以下技术的实现：**

1.查找对象

>     unity中提供了获取对象的五种方法：
>     
> 
>  1. 通过对象名称（Find方法）
>  2. 通过标签获取单个游戏对象（FindWithTag方法）
>  3. 通过标签获取多个游戏对象（FindGameObjectsWithTags方法）
>  4. 通过类型获取单个游戏对象（FindObjectOfType方法）
>  5. 通过类型获取多个游戏对象（FindObjectsOfType方法）
> 

以Find方法为例验证。

```
void Start()
    {

        if (GameObject.Find("Son") != null)
        {
            Debug.Log("Find the Son.");
        } else
        {
            Debug.Log("Can't find the Son.");
        }
        if (GameObject.Find("Father") != null)
        {
            Debug.Log("Find the Father.");
        }
        else
        {
            Debug.Log("Can't find the Father.");
        }
        if (GameObject.Find("noExit") != null)
        {
            Debug.Log("Find the noExit.");
        }
        else
        {
            Debug.Log("Can't find the noExit.");
        }
    }
```
结果截图：

![](https://raw.githubusercontent.com/LynnZiQi/Unity3D/master/image/find.jpg)

2.添加子对象

参考了官方文档[GameObject.CreatePrimitive](https://docs.unity3d.com/ScriptReference/GameObject.CreatePrimitive.html)

```
using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    void Start()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0.5F, 0);
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(0, 1.5F, 0);
        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule.transform.position = new Vector3(2, 1, 0);
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(-2, 1, 0);
    }
}
```
 
3.遍历对象树

```
List lst = new List;
foreach (Transform child in transform)
{
    lst.Add(child); 
    Debug.Log(child.gameObject.name);
}
//清除所有子对象
for(int i = 0;i < lst.Count;i++)
{
    Destroy(lst[i].gameObject);
} 
```

4.清除所有子对象

参见3.遍历对象树。



----------


**资源预设（Prefabs）与 对象克隆 (clone)**
  
- 预设（Prefabs）有什么好处？
  

> 预设 (Prefab) 是一种资源 - 存储在工程视图 (Project View) 中可重复使用的游戏对象 (GameObject)。预设 (Prefabs) 可放入到多个场景中，且每个场景可使用多次。向场景添加一个预设 (Prefab) 时，就会创建它的一个实例。所有预设 (Prefab) 实例都链接到原始预设 (Prefab)，实质上是原始预设的克隆。不管您的工程中有多少个实例，您对预设 (Prefab) 作薄出任何更改时，您会看到这些更改应用于所有实例。 

预设可以理解为一个类模版。使用预设可以方便快捷地创建大量相同属性的对象，还能避免冗杂代码带来的错误。

- 预设与对象克隆 (clone or copy or Instantiate of Unity Object) 关系？
  
  对象克隆也是用来大批量创建对象的，但是这些对象之间并无关联，也不会因为被克隆的对象属性改变而影响克隆的对象。预设不同，修改预设会使得所有实例化的对象属性发生改变。
  
- 制作 table 预制，写一段代码将 table 预制资源实例化成游戏对象
  
  *这里遇到了个坑，Game窗口显示不出实体。因为不在摄像机的镜头范围内.....需要调整一下坐标

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject objPrefab = (GameObject)Resources.Load("Table");
        Instantiate(objPrefab);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

```

结果截图：

![](https://raw.githubusercontent.com/LynnZiQi/Unity3D/master/image/QQ截图20180324154240.jpg)


 **尝试解释组合模式（Composite Pattern / 一种设计模式）。使用** **BroadcastMessage() 方法**
- 向子对象发送消息
```
//子对象
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		


}
    void SonMethod()
    {
        Debug.Log("Hello Father");
    }
}
//父对象
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

	// Use this for initialization
	void Start () {
        BroadcastMessage("SonMethod");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  
}


```
运行结果：

利用预设生成的Table进行实验，因此Table有四个子对象。

![](https://raw.githubusercontent.com/LynnZiQi/Unity3D/master/image/Message.jpg)

----------


**2、 编程实践，小游戏**

* 游戏内容： 井字棋 或 贷款计算器 或 简单计算器 等等
* 技术限制： 仅允许使用 **[IMGUI](https://docs.unity3d.com/Manual/GUIScriptingGuide.html)** 构建 UI
* 作业目的： 
    - 提升 debug 能力
    - 提升阅读 API 文档能力 


游戏截图：

初始界面：

![](https://raw.githubusercontent.com/LynnZiQi/Unity3D/master/image/1.jpg)

Xwin：

![](https://raw.githubusercontent.com/LynnZiQi/Unity3D/master/image/2.jpg)

Owin:

![](https://raw.githubusercontent.com/LynnZiQi/Unity3D/master/image/3.jpg)

平局：

![](https://raw.githubusercontent.com/LynnZiQi/Unity3D/master/image/4.jpg)


源代码参见HW1/NewBehaviourScript.cs,视频文件已压缩，见HW1/Video

----------
参考资料：

 1. [unity3d中脚本生命周期（MonoBehaviour lifecycle）](https://blog.csdn.net/qitian67/article/details/18516503)
 2. [unity中查找对象的五种方法](https://blog.csdn.net/u010145745/article/details/39160141)
