using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoundController

{

    void gameStart();


    void gameStop();


    void nextRound();

    int getRound();

    void setRound();

    int getScore(DiskData disk);

    void reStart();

}


public class RoundController : MonoBehaviour, IRoundController {
    private DiskFactory factory;
    private ScoreRecorder recorder;


    private int currentTime;
    private int countPersecond;

    private bool shot;
    public int round;

    public int N;
    public int currentN; //已经用过的飞盘
    float width, height;
    float castw(float scale)
    {
        return (Screen.width - width) / scale;
    }

    float casth(float scale)
    {
        return (Screen.height - height) / scale;
    }
    void OnGUI()

    {
        // Debug.Log("RoundGUI");
        width = Screen.width / 12;
        height = Screen.height / 12;
    /*    if (recorder.score < (round*100 -100)&& recorder.score != 0 && currentN == 0 && factory.usedCount == 0)
        {
            GUI.TextArea(new Rect(width + 300, height + 200, 100, 30), "Game Over!");
            if (GUI.Button(new Rect(width + 180, height, 50, 30), "restart"))
            {

                this.reStart();

            }
        }*/
  /*      if (round == 2)
        {
           if( GUI.Button(new Rect(width + 300, height + 200, 100, 30), "Game Over!"))
            this.reStart();
        }*/
        GUI.TextArea(new Rect(width, height+50, 100, 30), "Round : " + round.ToString());
        GUI.TextArea(new Rect(width, height, 100, 30), "Score : " + recorder.score.ToString());
        if (currentN == 0 && factory.usedCount== 0)
        {

            if (GUI.Button(new Rect(width + 110, height, 50, 30), "start") )
            {

                /*          if (round != 1)
                          {
                              Debug.Log("ROUND = 2");
                              this.nextRound();
                          }
                          */

                this.nextRound();

            }

            if (GUI.Button(new Rect(width + 180, height, 50, 30), "restart"))
            {

                this.reStart();

            }



        }
     /*   if (currentN == 0 && recorder.score <= round * 100 / 2 && recorder.score != 0)
        {

            if (GUI.Button(new Rect(width + 150, height + 150, 100, 100), "GameOver!"))
            {

                this.reStart();

            }
        }*/
     

    }

    void Awake()
    {
        setRound();
        factory = Singleton<DiskFactory>.Instance;
        recorder = Singleton<ScoreRecorder>.Instance;

        countPersecond = (int)(1f / Time.deltaTime);

        
    }

    public void setRound()
    {
        round = 0;
    }

    public int getRound()
    {
        return round;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentTime++;

        if (shot && currentN < N)
        {
            
            if (currentTime % countPersecond == 0)
            {
                currentN++;
                if (currentN == N)
                {
                    gameStop();
                    return;
                }

                factory.getDiskCount(round);
            }
        }

	}

    public int getScore(DiskData disk)
    {
        recorder.Record(disk);
        return disk.score;
    }

    public void nextRound()
    {
        round++;

        factory.FreeDisk();
        currentTime = 0;
        currentN = 0;
        gameStart();
    }

    public void gameStart()
    {
        shot = true;
    }
    public void gameStop()
    {
        shot = false;
        currentN = 0;
    }

    public void reStart()
    {
        gameStop();
        round = 0;
       
        recorder.Reset();
        nextRound();
    }
}
