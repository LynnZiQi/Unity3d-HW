using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour, Observer, ISceneController
{
    public Text scoreText;
    public Text centerText;

    private ScoreRecorder record;
    private UserGUI UI;
    private PatrolFactory factory;

    private float[] posx = { -5, 7, -5, 5 };
    private float[] posz = { -5, -7, 5, 5 };

    public bool isStarted { get; set; }
    public bool isPaused { get; set; }


    void Awake()
    {

        this.isStarted = false;

        this.isPaused = false;
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;

    }
    // Use this for initialization
    void Start()
    {
        record = new ScoreRecorder();
        record.scoreText = scoreText;
        UI = new UserGUI();
        UI.centerText = centerText;

        factory = Singleton<PatrolFactory>.Instance;

        //订阅者添加事件
        Publish publisher = Publisher.getInstance();
        publisher.add(this);


    }

    private void LoadResources()
    {

        Instantiate(Resources.Load("prefabs/Ami"), new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
        for (int i = 0; i < 4; i++)
        {
            //利用factory产生巡逻兵
            GameObject patrol = factory.getPatrolObject(new Vector3(posx[i], 0, posz[i]), Quaternion.Euler(new Vector3(0, 180, 0)));
            patrol.name = "Patrol" + (i + 1);
        }
    }


    public void notified(ActorState state, int pos, GameObject actor)
    {
        if (state == ActorState.BE_FOLLOWED)
            record.addScore(1); //摆脱巡逻兵，加分
        else
        {
            UI.loseGame();

        }
        if (record.score >= 5) //玩家得分超过5，获得游戏胜利
        {
            UI.winGame();
            state = ActorState.DEATH; //直接利用Death结束游戏
            CancelInvoke();
        }
    }

    public void Restart()
    {
        throw new System.NotImplementedException();
    }

    public void StartGame()
    {
        this.isStarted = true;
        this.isPaused = false;
        LoadResources();
    }

    public void Pause()
    {
        this.isPaused = true;

        CancelInvoke();
    }

    public void Resume()
    {
        throw new System.NotImplementedException();
    }

}
