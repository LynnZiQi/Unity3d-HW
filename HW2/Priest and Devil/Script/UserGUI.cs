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
    void Start()
    {
        action = SSDirector.getInstance().currentScenceController as IUserAction;
    }

    void OnGUI()
    {
  
        if (SSDirector.getInstance().state == State.WIN)//胜利
        {
            StopAllCoroutines();
            GUI.Button(new Rect(300, 50, 50,50), "Win!");
        }
        else if (SSDirector.getInstance().state == State.LOSE)//失败
        {
            StopAllCoroutines();
            GUI.Button(new Rect(300, 50,50,50), "Lose!");

        }
    }

    // Update is called once per frame
    void Update()
    {
        action.OnClick();
    }

}