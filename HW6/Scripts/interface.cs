using System.Collections;

using System.Collections.Generic;

using UnityEngine;




public interface ISSActionCallback

{

    void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Compeleted,

                        int intParam = 0,

                        string strParam = null,

                        Object objectParam = null);

}

public interface Publish
{

    void notify(ActorState state, int pos, GameObject actor);


    void add(Observer observer);


    void delete(Observer observer);
}


public interface Observer
{

    void notified(ActorState state, int pos, GameObject actor);
}

public interface ISceneController

{

    void Restart();



    void StartGame();



    void Pause();



    void Resume();

}