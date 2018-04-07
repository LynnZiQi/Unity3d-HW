本次游戏实现参照课件的框架，将动作管理与游戏场景分离。
![这里写图片描述](https://img-blog.csdn.net/20180407151040560?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzMyMzM1MDk1/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)
完全按照课件的思路实现

 1. 动作基类SSAction
 2. 简单动作MoveToAction
 3. 组合动作SequenceAction
 4. 动作管理基类SSActionManager
 5. 实战动作管理CCActionManager
 6. ......
 

其实上一次实现的时候参考的[这篇博客](https://blog.csdn.net/H12590400327/article/details/70037805)已经符合本次要求，只是把控制类、动作管理写在一个脚本，显得代码比较冗长，所以我只是略微修改了一下上周的代码，把每一个类分别用一个脚本来实现，便于修改。UserGUI没有什么变化，优化了一下让提示框固定在屏幕中心，没有用绝对代码。

项目代码结构如下：
![这里写图片描述](https://img-blog.csdn.net/20180407151709824?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzMyMzM1MDk1/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)


遇到的bug及问题：

 1. 一开始只知道按照课件把各个类分写成不同的脚本，然后按照上一次挂脚本的方式——Camera挂UserGUI，Main挂SenceController和SSActionManager。运行老是提示“Object reference not set to an instance of an object”
![Object reference not set to an instance of an object](https://img-blog.csdn.net/20180407152110942?watermark/2/text/aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3FxXzMyMzM1MDk1/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70)
老老实实用debug.log的方法看是哪里出了问题，发现是actionManager没有实例化，在actionManager = GetComponent<\CCActionManager>() as CCActionManager;出现了问题。搜索了一下GetComponent相关，需要把CCActionManager也挂在Main上。因为上次这些Manager都在一个脚本上，就没有发现问题。挂上去之后成功实例化actionManager。

 2. 用一个数组存boat的对象（船上的对象），但是统计的时候不管有没有船上有没有物体，都显示NULL。一开始以为是赋值没有成功，但是在boat[seatNum]=people;语句前后加上debug输出信息发现其实是成功了的。继续加Debug.Log，发现很诡异地一次点击onClick函数调用了两次，而且第二次调用的时候点击对象为空，所以覆盖了上一次赋值的结果。查了一下资料确定onclick函数放在Update中而不是onGUI里应该是正确的，快走投无路的时候发现UserGUI脚本被挂了两次.......所以onclick被调用了两次。修改之后终于能够成功运行了。

