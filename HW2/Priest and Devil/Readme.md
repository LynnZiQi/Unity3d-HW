游戏规则：游戏初始左岸有三个恶魔和三个牧师，鼠标点击牧师或恶魔，可以控制他们上下船。点击船，可以使它到达对岸。开船时船上必须至少有一个恶魔或者牧师。当一边的恶魔数大于牧师数时，游戏失败。把全部恶魔和牧师都送到对岸，游戏成功。只有船靠岸时，才能响应动作。


----------


1.列出游戏中提及的事物（Objects）
游戏中的对象包括3个牧师、3个恶魔、两个河岸、一艘船

2.用表格列出玩家动作表（规则表），注意，动作越少越好

<table>
  <tr>
    <th width=33% >规则</th>
    <th width=33%>条件</th>
    <th width="33%",>动作</th>
  </tr>
  <tr> 
  <td>游戏失败</td>
  <td>船停靠时牧师的数量少于恶魔的数量</td>
  <td>提示Lose</td>
  </tr>
  <tr>
  <td>开船</td>
  <td>船上有人（牧师/恶魔）</td>
 <td>船移动</td>
 
  <tr>
  <td>牧师上船</td>
<td>停船的河岸上有牧师，船有空位</td>
<td>牧师从岸上移动到船上</td>
</tr>

  <tr>
  <td>恶魔上船</td>
<td>停船的河岸上有恶魔，船有空位</td>
<td>恶魔从岸上移动到船上</td>
</tr>

  <tr>
  <td>左侧上岸</td>
<td>船靠岸，船上左边有对象</td>
<td>对象从船上移动到岸上</td>
</tr>

  <tr>
  <td>右侧上岸</td>
<td>船靠岸，船上右边有对象</td>
<td>对象从船上移动到岸上</td>
</tr>

  <tr>
  <td>游戏成功</td>
<td>船靠右岸，所有牧师和恶魔都在一个河岸</td>
<td>提示WIN</td>
</tr>
</table>


----------
关于程序
> 思路和代码有参考[H12590400327的博客](https://blog.csdn.net/H12590400327/article/details/70037805)


 - **脚本有四个，分别是**
 UserGUI.cs（用于提供和用户交互的接口）

 SSDirector.cs（导演类，控制全局）

 FirstSSActionManager.cs（管理游戏对象动作）
 
 FirstSceneController.cs（场记，管理游戏对象）


 - **将游戏对象做成预制**，其中Devil用Sphere表示，Priest用Cube表示。为了统计方便，分别给它们加上"Devil"和"Priest"的Tag.

![这里写图片描述](https://img-blog.csdn.net/2018040111503257?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzMyMzM1MDk1/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)
 
 - **一些重要的函数说明**
 只是大概逻辑，和类之间联系部分，条件语句等基本省略。


```
/*SSDirector.cs*/
public enum State { START, LOSE,WIN }; //定义游戏三个状态

 public ISceneController currentScenceController { get; set; } //当前场景控制器，接受人机交互接口委托
```


```
/*FirstSceneController.cs*/
public interface ISceneController
{
    void LoadResources(); //加载资源（如预制）

    //没来得及实现
    //Pause()
    //Resume()

}

    // Update is called once per frame
    void Update()
    {
        Judge(); //条件判断此时的游戏状态以做出响应
    }
    
//利用鼠标点击来进行游戏
 public void OnClick() {
 MovePeople(gameObj); //移动牧师/恶魔
 MoveBoat();//移动船
 }

 void MovePeople(GameObject people){
 actionManager.getOnBoat(people, shoreNum, seatNum); //利用ActionManager 
 actionManager.getOffBoat(people, shoreNum); //利用ActionManager
/*函数包括动作之后对应的对象列表的增添和删除*/

void MoveBoat()
{
actionManager.moveBoat(boat_obj);
 }
 
public void Judge() {
        if ((left_d > left_p && left_p != 0) || (right_d > right_p && right_p != 0) )
        {
            SSDirector.getInstance().state = State.LOSE; //恶魔数量大于牧师数量，游戏失败
        }
        else if (right_d == right_p && right_d == 3)//全过河，赢了
        {
            //Debug.Log(right_d);
            SSDirector.getInstance().state = State.WIN;
        }
}
```



```
/*UserGUI.cs*/
    void Start()
    {
        action = SSDirector.getInstance().currentScenceController as IUserAction;
    }
    void Update()
    {
        action.OnClick();
    }
    void OnGUI()
    {
  
        if (SSDirector.getInstance().state == State.WIN)//胜利
        {
            StopAllCoroutines();
            GUI.Button(new Rect(300, 50, 50,50), "Win!");
        }
        else if (SSDirector.getInstance().state == State.LOSE)//失败
        {
            StopAllCoroutines();
            GUI.Button(new Rect(300, 50,50,50), "Lose!");

        }
    }
```


```
/*FirstSSActionManager.cs*/
    public override void Start()
    {
        foreach (SSAction action in sequence)
        {
            action.gameObject = this.gameObject;
            action.transform = this.transform;
            action.callback = this;
            action.Start();
        }
    }
    public void moveBoat(GameObject boat)
    {
        horizontal = MoveToAction.getAction((boat.transform.position == new Vector3(4, 0, 0) ? new Vector3(-4, 0, 0) : new Vector3(4, 0, 0)), speed);
        this.Action(boat, horizontal, this);
    }
    public void getOnBoat(GameObject people, int shore, int seat);
    public void getOffBoat(GameObject people, int shoreNum);
    
```

 - **一些bug或困难记录**

1.State枚举时把"WIN"放在了第一位，由于UserGUI是通过State来判断应该做出什么样的响应，所以游戏一开始就会显示“WIN”。调整了一下顺序，把Start放在第一个位置，表示游戏开始就进入“Start”状态，就可以顺利进行。
 
2.不清楚怎么样操作对象，一开始看一篇博客是规定了每一个牧师/恶魔的控制按钮，这样显得画面比较难看。然后发现可以用鼠标Click选中，用Ray是否Hit来判断选中对象，这样比较符合游戏玩家的习惯。
    
3.比较头疼的是位置安排的问题，不是很清楚怎么能够恰好让对象出现在屏幕中间，这里是直接按照博客上的坐标设置。还有上船下船时对象的位置确定也比较头疼，习惯了绝对坐标的写法，然后经常会飞出规定的范围，这个比较头痛。以前写2D的时候还可以用鼠标拖从而确定位置，现在....还需要进一步研究。
 
4.要控制只有船靠岸的时候才能响应动作，需要一个变量类似flag，判断此时能否进行动作，在每一个move或者geton/getoff函数里都要进行状态的判断，否则会出现行驶过程中人物上船/上岸的诡异现象。

5.本来打算写一个restart函数，点击弹出的"WIN"或者"LOSE"按钮可以重新开始游戏。以为可以直接重新手动调用start()就能够实现，然后发现并不是，因为需要重新安排“导演”“场记”然后进行FirstAction的系列设置，并不能把原来的状态全部清空。这个等有时间再进一步完善。

6.统计左右岸的牧师和恶魔数量来判断是否胜利/失败的时候，一开始没有用Tag的方法，然后获取object来判断是Sphere还是Cube，虽然也可以实现，但是感觉不太好，如果以后有更多的对象就不适用这种方法了，所以还是按照这个比较好的思路进行了代码的修改。

7.一些条件判断比如船上没人不能开船，驶离岸边才开始判断恶魔数和牧师数，船靠岸时就判断恶魔数和牧师数等逻辑比较复杂。

8.考虑补充倒计时功能，限制时间完成，否则算是失败。不然玩家一直用一个恶魔/牧师控制然后让船开来开去，失去了游戏的趣味性。



