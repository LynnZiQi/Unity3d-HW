using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour {

    public int score;
     void Awake()
    {
        Reset();
    }
    public void Reset()
    {
        score = 0;

    }
    public void Record(DiskData disk)
    {
        score += disk.score;
    }
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
        //Debug.Log("GUI");
        width = Screen.width / 12;
        height = Screen.height / 12;
     //    GUI.TextArea(new Rect(width, height, 100, 30), "Score : " + score.ToString());

    }

    public void setScore(int s)
    {
        score = s;
    }

    public int getScore(DiskData disk)
    {
        return disk.score;
    }
}
