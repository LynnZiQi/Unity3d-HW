using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public interface IUserAction

{
    void GameOver();
}
//定义了事件处理接口，所有事件管理者都必须实现这个接口，来实现 事件调度。
//所以，组合事件需要实现它，事件管理器也必须实现它
public interface ISSActionCallback
{

    void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Compeleted,

                        int intParam = 0,

                        string strParam = null,

                        Object objectParam = null);
    void actionDone(SSAction source);

}

public interface ISceneController

{

    void Restart();



    void StartGame();



    void Pause();



    void Resume();
    void ChangeActionManager();

}



public interface IGamePlaying

{

    void ShootDisk();



    void FreeDisk();



    void NextRound();



    void addScoreByRound();



    float getSpeedByRound();

}



public interface IActionManager

{

    void moveDisk();



    void clearActions();

}