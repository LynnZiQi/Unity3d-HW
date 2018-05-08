using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserGUI: MonoBehaviour
{
    private ISceneController action;
    public Text centerText;
    float width, height;
    private SceneController sceneController;
    // Use this for initialization
    void Start()
    {
        action = SSDirector.getInstance().currentSceneController as ISceneController;
        sceneController = Singleton<SceneController>.Instance;
    }
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
        width = Screen.width / 12;
        height = Screen.height / 12;

        if (sceneController.isStarted == false)
        {

            if (GUI.Button(new Rect(castw(2f) + 20, casth(6f) + 60, 100, 100), "Start"))
            {
                action.StartGame();

            }

        }
        //GUI.TextArea(new Rect(width, height, 100, 30), sceneController.scoreText.text);
    }
    public void loseGame()
    {
        centerText.text = "GameOver!!";
    }

    public void resetGame()
    {
        centerText.text = "";
    }

    public void winGame()
    {
        centerText.text = "YouWin!";
    }

}
