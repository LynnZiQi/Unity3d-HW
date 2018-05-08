using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : SSActionManager, ISSActionCallback, Observer
{

        public enum ActionState : int { IDLE, WALKLEFT, WALKFORWARD, WALKRIGHT, WALKBACK }
        // 各种动作

        private Animator ani;
        // 动作

        private SSAction currentAction;
        private ActionState currentState;
        public Publish publisher;

        private const float walkSpeed = 1f; //巡逻
        private const float runSpeed = 3f; //追击


        // Use this for initialization
        void Start()
        {
            ani = this.gameObject.GetComponent<Animator>();
            publisher = Publisher.getInstance();
            publisher.add(this);


            currentState = ActionState.IDLE;
            idle();
            // 开始时，静止状态
        }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Compeleted, int intParam = 0, string strParam = null, Object objParam = null)
        {
            //循环状态
            if ((int)currentState > 4)
                currentState -= 4;
            else currentState += 1;
            // 改变当前状态

            switch (currentState)
            {
                case ActionState.WALKLEFT:
                    walkLeft();
                    break;
                case ActionState.WALKRIGHT:
                    walkRight();
                    break;
                case ActionState.WALKFORWARD:
                    walkForward();
                    break;
                case ActionState.WALKBACK:
                    walkBack();
                    break;
                default:
                    idle();
                    break;
            }

        }

        public void idle()
        {
            currentAction = IdleAction.GetIdleAction(Random.Range(1, 1.5f), ani);
            this.runAction(this.gameObject, currentAction, this);
        }

        public void walkLeft()
        {
            Vector3 target = Vector3.left * Random.Range(4, 7) + this.transform.position;
            currentAction = WalkAction.GetWalkAction(target, walkSpeed, ani);
            this.runAction(this.gameObject, currentAction, this);
        }
        public void walkRight()
        {
            Vector3 target = Vector3.right * Random.Range(4, 7) + this.transform.position;
            currentAction = WalkAction.GetWalkAction(target, walkSpeed, ani);
            this.runAction(this.gameObject, currentAction, this);
        }

        public void walkForward()
        {
            Vector3 target = Vector3.forward * Random.Range(4, 7) + this.transform.position;
            currentAction = WalkAction.GetWalkAction(target, walkSpeed, ani);
            this.runAction(this.gameObject, currentAction, this);
        }

        public void walkBack()
        {
            Vector3 target = Vector3.back * Random.Range(4, 7) + this.transform.position;
            currentAction = WalkAction.GetWalkAction(target, walkSpeed, ani);
            this.runAction(this.gameObject, currentAction, this);
        }


        public void turnNextDirection()
        {
            currentAction.destroy = true;
            // 销毁当前动作
            //相反方向动作
            switch (currentState)
            {
                case ActionState.WALKLEFT:
                    currentState = ActionState.WALKRIGHT;
                    walkRight();
                    break;
                case ActionState.WALKRIGHT:
                    currentState = ActionState.WALKLEFT;
                    walkLeft();
                    break;
                case ActionState.WALKFORWARD:
                    currentState = ActionState.WALKBACK;
                    walkBack();
                    break;
                case ActionState.WALKBACK:
                    currentState = ActionState.WALKFORWARD;
                    walkForward();
                    break;
            }
        }

        public void FollowAction(GameObject gameobject)
        {
            currentAction.destroy = true;
            // 销毁当前动作
            currentAction = RunAction.GetRunAction(gameobject.transform, runSpeed, ani);
            this.runAction(this.gameObject, currentAction, this);
            // 跑向目标方向
        }

        public void PatrolAction()
        {
            currentAction.destroy = true;
            // 销毁当前动作
            idle();
            // 重新进行动作循环
        }

        public void stopAllAction()
        {
            currentAction.destroy = true;
            currentAction = IdleAction.GetIdleAction(-1f, ani);
            this.runAction(this.gameObject, currentAction, this);
            // 永久站立
        }

        private void OnCollisionEnter(Collision collision)
        {

            Transform parent = collision.gameObject.transform.parent;
            if (parent != null && parent.CompareTag("Wall")) turnNextDirection();
            // 撞到墙
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Door")) turnNextDirection();
            // 走出巡逻区域
        }


        public void notified(ActorState state, int pos, GameObject actor)
        {
            if (state == ActorState.BE_FOLLOWED)
            {
                if (pos == this.gameObject.name[this.gameObject.name.Length - 1] - '0')
                    FollowAction(actor);

                else
                    PatrolAction();

            }
            // 角色死亡，结束动作
            else if (state == ActorState.DEATH)
            {
                stopAllAction();
            }

        }


    }
