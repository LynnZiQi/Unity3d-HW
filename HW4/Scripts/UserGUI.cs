using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
    private DiskFactory factory;
    private ScoreRecorder recorder;
    private RoundController controller;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
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

        GUI.TextArea(new Rect(width, height + 50, 100, 30), "Round : " + controller.round.ToString());
        GUI.TextArea(new Rect(width, height, 100, 30), "Score : " + recorder.score.ToString());
        if (controller.currentN== 0 && factory.usedCount == 0)
        {

            if (GUI.Button(new Rect(width + 110, height, 50, 30), "start"))
            {

                /*          if (round != 1)
                          {
                              Debug.Log("ROUND = 2");
                              this.nextRound();
                          }
                          */

                this.controller.nextRound();

            }

            if (GUI.Button(new Rect(width + 180, height, 50, 30), "restart"))
            {

                this.controller.reStart();

            }



        }



    }
}
