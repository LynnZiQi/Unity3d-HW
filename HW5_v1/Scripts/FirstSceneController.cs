using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class FirstSceneController : MonoBehaviour, ISceneController, IUserAction

{

    public enum MoveMode

    {

        CCMove,

        PhysicsMove
    }


    public Camera cam;

    private DiskFactory factory;

    private IActionManager actionManager;



    public MoveMode moveMode { get; set; }



    public int round { get; set; }



    public int trials { get; set; }



    public int score { get; set; }



    public bool isStarted { get; set; }



    public bool isPaused { get; set; }



    void Awake()

    {

        factory = Singleton<DiskFactory>.Instance;

        moveMode = MoveMode.PhysicsMove;

        actionManager = Singleton<PhysicsActionManager>.Instance;

        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;

        this.isStarted = false;

        this.isPaused = false;



        this.round = 1;

        this.score = 0;

    }



    public void ChangeActionManager()

    {

        if (this.moveMode == MoveMode.CCMove)
        {

            this.actionManager = Singleton<PhysicsActionManager>.Instance;

            this.moveMode = MoveMode.PhysicsMove;

        }
        else
        {

            this.actionManager = Singleton<CCActionManager>.Instance;

            this.moveMode = MoveMode.CCMove;

        }

    }



    public void Pause()

    {

        this.isPaused = true;

        CancelInvoke();

    }



    public void Resume()

    {

        this.isPaused = false;

        sendDisk();

    }



    public void Restart()

    {

        round = 1;

        score = 0;

        CancelInvoke();

        FreeDisk();

        StartGame();

    }


    public void StartGame()

    {

        this.isStarted = true;

        this.isPaused = false;

        diskComp = 0;

        diskShot = 0;

        sendDisk();

    }

    public void GameOver()

    {

        this.isStarted = false;

        this.isPaused = true;

    }



    public void sendDisk()

    {


        InvokeRepeating("ShootSingleDisk", 1f, 1.2f);

    }



    int diskShot = 0;

    public int diskComp = 0;

    private void ShootSingleDisk()

    {
        //默认发送十个飞碟
        if (diskShot == 10)
        {

            CancelInvoke();

        }

        this.actionManager.moveDisk();

    }



    public void FreeDisk()

    {

        this.factory.FreeAllDisks();

        this.actionManager.clearActions();

    }



    public void NextRound()

    {
        ++round;

        FreeDisk();

        diskShot = 0;
        diskComp = 0;

        Pause();


    }



    public void recordScore()

    {

        this.score += this.round * 10;

    }



    public float getSpeed()

    {

        return 10f + 5f * round;

    }







    // Update is called once per frame

    void FixedUpdate()

    {

        if (diskComp == 10)
        {

            NextRound();

        }

        if (Input.GetMouseButtonDown(0))
        {


            Camera ca = cam.GetComponent<Camera>();

            Ray ray = ca.ScreenPointToRay(Input.mousePosition);

            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit))
            {

                if (rayHit.collider.gameObject.tag.Contains("Disk") && isPaused == false)
                { 

                    this.recordScore();
                    rayHit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    DiskData disk = rayHit.collider.gameObject.GetComponent<DiskData>();


                    factory.Free(disk.UsedIndex);

                    disk.currentSSAction.enable = false;
                    disk.currentSSAction.destroy = true;


                }

            }

        }

    }





}