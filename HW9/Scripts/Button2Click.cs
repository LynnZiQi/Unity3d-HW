using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Button2Click : MonoBehaviour
{
    public Button mButton;

    // Use this for initialization
    void Start()
    {
        Button btn = mButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void TaskOnClick()
    {
        Application.LoadLevel(2); //加载场景二
    }
}
