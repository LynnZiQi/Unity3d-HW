using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void OnClick();
}

public class UserGUI : MonoBehaviour
{
    private IUserAction action;

    // Use this for initialization
    float width, height;
    void Start()
    {
        action = SSDirector.getInstance().currentScenceController as IUserAction;



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


        if (SSDirector.getInstance().state == State.WIN)//胜利
        {
            StopAllCoroutines();
            GUI.Button(new Rect(castw(2f) + 20, casth(6f) - 20, 50, 50), "Win!");
        }
        else if (SSDirector.getInstance().state == State.LOSE)//失败
        {
            StopAllCoroutines();
            GUI.Button(new Rect(castw(2f) + 20, casth(6f) - 20, 50, 50), "Lose!");

        }
    }

    // Update is called once per frame
    void Update()
    {
        action.OnClick();
    }

}