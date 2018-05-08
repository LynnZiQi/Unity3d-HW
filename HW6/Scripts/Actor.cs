
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Actor : MonoBehaviour
{

    private Animator ani;


    private Vector3 velocity;


    private float rotateSpeed = 15f;
    private float runSpeed = 8f;


    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
        {
        if (!ani.GetBool("isLive"))
            return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        velocity = new Vector3(x, 0, z);

        ani.speed = 1 + ani.GetFloat("Speed") / 2;
        ani.SetFloat("Speed", Mathf.Max(Mathf.Abs(x), Mathf.Abs(z)));

       
        if (x != 0 || z != 0)
        {

            Quaternion rotation = Quaternion.LookRotation(velocity);
            if (transform.rotation != rotation)
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * rotateSpeed);
        }

        this.transform.position += velocity * Time.fixedDeltaTime * runSpeed;

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Area"))
        {

            int patrol = collider.gameObject.name[collider.gameObject.name.Length - 1] - '0';

            Publish publish = Publisher.getInstance();
            publish.notify(ActorState.BE_FOLLOWED, patrol, this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Patrol") && ani.GetBool("isLive"))
        {
            ani.SetBool("isLive", false);
            ani.SetTrigger("toDie");


            Publish publish = Publisher.getInstance();
            publish.notify(ActorState.DEATH, 0, null);

        }
    }
}
