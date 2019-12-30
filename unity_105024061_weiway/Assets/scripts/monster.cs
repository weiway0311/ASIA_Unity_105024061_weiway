using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : MonoBehaviour
{
    [Header("移動速度"), Range(1,1000)]
    public int speed = 10;
    [Header("轉向速度"), Range(1.5f, 100f)]
    public float turnspd = 20.5f;
    [Header("玩家")]
    public string _name = "怪物";


    public Transform tran;
    public Rigidbody rig;
    public Animator ani;
    public Rigidbody attackrig;

    private void Update()
    {
        turn();
        walk();
        attack();
    }

    private void walk()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("attack")) return;
        float v = Input.GetAxis("Vertical");
        rig.AddForce(tran.forward * speed * v * Time.deltaTime);
        ani.SetBool("walk", v != 0);
    }
    private void turn()
    {
        float h = Input.GetAxis("Horizontal");
        tran.Rotate(0, turnspd * h * Time.deltaTime , 0);
    }
    private void attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("attack");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Burger" && ani.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());

            other.GetComponent<HingeJoint>().connectedBody = attackrig;
        }
    }

}
