using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
    private ISceneController action;
    private FirstSceneController sceneController;

    float width, height;
    // Use this for initialization
    void Start () {
        action = SSDirector.getInstance().currentSceneController as ISceneController;

       sceneController = Singleton<FirstSceneController>.Instance;
    }
    float castw(float scale)
    {
        return (Screen.width - width) / scale;
    }

    float casth(float scale)
    {
        return (Screen.height - height) / scale;
    }
    // Update is called once per frame
    void Update () {
		
	}

    void OnGUI()

    {
        width = Screen.width / 12;
        height = Screen.height / 12;

        if ( sceneController.isStarted == false)
        {

            if (GUI.Button(new Rect(castw(2f) + 20, casth(6f)+60 , 50, 50), "Start"))
            {
                action.StartGame();

            }

        }
        else
        { 

            if (GUI.Button(new Rect(width + 120, height, 60, 30), "Restart"))
            {

               action.Restart();

            }

            if (GUI.Button(new Rect(width + 200, height, 80, 30), "Change"))
            {

               action.ChangeActionManager();

                action.Restart();

            }

            if (sceneController.isPaused == true)
            {

                if (GUI.Button(new Rect(castw(2f) + 20, casth(6f) + 60, 100, 50), "Next Round"))
                {
                    //  if (sceneController.score >= sceneController.round*50)
                    action.Resume();


                    //  if (GUI.Button(new Rect(castw(2f) + 20, casth(6f) + 60, 50, 50), "GameOver"))
                    // {
                    //     action.Restart();
                    //      }

                    //      }
                }

            }

            GUI.TextArea(new Rect(width, height, 100, 30), "Score:" + sceneController.score.ToString());
            GUI.TextArea(new Rect(width, height + 50, 100, 30), "Round : " + sceneController.round.ToString());
        }

    }
}
