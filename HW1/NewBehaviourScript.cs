using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    //重置棋盘，以重新开始游戏
    void reset(int row, int col)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                chessboard[i, j] = 0;
            }
        }
        player = 1; //重置，使"X"为先手
    }

    int WhoWin(int row, int col)
    {
        for (int i = 0; i < row; i++)
        {
            if (chessboard[i, 0] != 0)
            {
                if (chessboard[i, 1] == chessboard[i, 0] && chessboard[i, 1] == chessboard[i, 2])
                {
                    return chessboard[i, 0]; //横向连成一排
                }
            }
        }
        for (int i = 0; i < row; i++)
        {
            if (chessboard[0,i ] != 0)
            {
                if (chessboard[1, i] == chessboard[0, i] && chessboard[1,i] == chessboard[2, i])
                {
                    return chessboard[0, i]; //纵向连成一排
                }
            }
        }
        //斜方向
        if ((chessboard[1,1] != 0) && (chessboard[0,0] == chessboard[2, 2] && chessboard[1,1] == chessboard[2,2]) ||  (chessboard[2, 0] == chessboard[0, 2] && chessboard[0,2] == chessboard[1,1]))
        {
            return chessboard[1, 1];
        }

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (chessboard[i, j] == 0)
                    return 0;//还没下完
            }
        }
        //平局情况
        return 2;
    }
	// Use this for initialization
	void Start () {
        reset(N, M);  //开始直接进行棋盘初始化
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void play(int row, int col, int result)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (chessboard[i,j] == 1)
                {
                    GUI.Button(new Rect(300 + i * 50, 100 + j * 50, 50, 50), "X");

                }
                if (chessboard[i, j] == -1)
                {
                    GUI.Button(new Rect(300 + i * 50, 100 + j * 50, 50, 50), "O");

                }
                if (GUI.Button(new Rect(300 + i * 50, 100 + j * 50, 50, 50), ""))
                {
                    if (result == 0)
                    {
                        chessboard[i, j] = player;
                        player = -player;
                    }
                }
            }
        }
    }
    //绘制棋盘
    void OnGUI()
    {
        //先判断状态
        if (GUI.Button(new Rect(325, 300, 100,50), "Restart"))
        {
            reset(N, M);
        }
        if (WhoWin(N, M) == 1)
        {
            //GUI.Box(new Rect(300, 100, 50, 50), "X win!");
            GUI.TextField(new Rect(325, 40, 100, 30), "X win!");

        }
        else if (WhoWin(N, M) == -1)
        {
            GUI.TextField(new Rect(325, 40, 100, 30), "O win!");
        } else if(WhoWin(N, M) == 2)
        {
            GUI.TextField(new Rect(325, 40, 100, 30), "Draw Game!");

        }
        play(N, M, WhoWin(N, M));

        
    }
    private const int N = 3;
    private const int M = 3;//棋盘大小，方便更改
    /*虽然井字棋规定3x3，但可拓展成五子棋，所以没有严格定义棋盘规格*/

    private int[,] chessboard = new int[N, M];
    private int player = 1; //以1代表player为"X"，-1代表player为"O"

  
}

