using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class PhysicsMoveToAction : SSAction
{





    public float speed;



    private float disappeareTime;
    private float currentTime;

    public override void Start()

    {

        this.enable = true;
        disappeareTime = (int)(2f / Time.deltaTime);
        currentTime = 0;
        this.gameObject.transform.position = this.originPosition;


    }




    public static PhysicsMoveToAction GetSSAction(float speed)

    {

        PhysicsMoveToAction action = ScriptableObject.CreateInstance<PhysicsMoveToAction>();

        action.speed = speed;

        action.originPosition = SSAction.getRandomStartPoint();

        return action;

    }



    public override void FixedUpdate()

    {

        Rigidbody mmRigibody = this.gameObject.GetComponent<Rigidbody>();

        if (this.enable && !this.isGameEnded)
        {

            currentTime++;
            if (mmRigibody)
            {
               mmRigibody.velocity = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), speed);
            }

        }

        if (this.isGameEnded)
        {

            reStart();

            mmRigibody.MovePosition(this.originPosition);

            mmRigibody.velocity = Vector3.zero;

            this.callback.SSActionEvent(this);

        }

    }



    public void reStart()
    {

        this.gameObject.transform.position = this.originPosition;
        currentTime = 0;
        this.enable = false;

    }




    public bool isGameEnded
    {
        get
        {
            return this.currentTime >= disappeareTime;
        }
    }






}