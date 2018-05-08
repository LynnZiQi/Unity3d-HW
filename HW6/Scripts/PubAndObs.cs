using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ActorState { BE_FOLLOWED, DEATH }
public class PubAndObs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
public class Publisher : Publish
{

    public delegate void GameEvent(ActorState state, int pos, GameObject actor);
    public static GameEvent OnGameEventNotify;

    private static Publish _instance;
    public static Publish getInstance()
    {
        if (_instance == null) _instance = new Publisher();
        return _instance;
    }

    public void notify(ActorState state, int pos, GameObject actor)
    {
        if (OnGameEventNotify != null)
            OnGameEventNotify(state, pos, actor);

    }

    public void add(Observer observer)
    {
        OnGameEventNotify += observer.notified;
    }

    public void delete(Observer observer)
    {
        OnGameEventNotify -= observer.notified;
    }
}
