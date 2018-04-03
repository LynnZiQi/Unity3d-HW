﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum State { START, LOSE,WIN };
public class SSDirector : System.Object
{
    public State state { get; set; }
    public static SSDirector _instance;
    public ISceneController currentScenceController { get; set; }
    public bool running { get; set; }

    //单例模式
    public static SSDirector getInstance()
    {
        if (_instance == null)
        {
            _instance = new SSDirector();
        }
        return _instance;
    }

    public int getFPS() 
    {
        return Application.targetFrameRate;
    }

    public void setFPS(int fps)
    {
        Application.targetFrameRate = fps;
    }
   
}