using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Goomba : Enemy, Idamageable
{

    [SerializeField] float speedLvl1;
    [SerializeField] float speedLvl2;
    [SerializeField] float Direction = 1;
    [SerializeField] bool canMove;
    [SerializeField] Rigidbody2D rB;
    [SerializeField] bool leftFromPlayer;

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        LvlBehavior();
    }

    void Move(float _speed)
    {
        if (canMove)
        {
            rB.velocity = new Vector2(Direction * _speed * Time.deltaTime, 0);
        }
    }

    public void GetDamage()
    {
        Level++;
    }

    protected override void Lvl1()
    {
        Debug.Log("estoy en nivel 1");
        Move(speedLvl1);
    }

    protected override void Lvl2()
    {
        Debug.Log("estoy en nivel 2");
        Move(speedLvl2);
    }

    protected override void Lvl3()
    {
        Debug.Log("estoy en nivel 3");
        Move(speedLvl2);
        ChangeDirection();

    }


    void ChangeDirection()
    {
        IEnumerator change(float a, float b)
        {
            canMove = false;
            for (float i = 0; i < 1.5f; i += Time.deltaTime)
            {
                speedLvl2 = Mathf.Lerp(a, b, i/1.5f);
                yield return null;
            }
            canMove = true;
        }

        float value = Mathf.Sign(Character.instance.transform.position.x - transform.position.x);

        if(value != Direction)
        {
            canMove = false;
        }

        if (leftFromPlayer == true && transform.position.x > Character.instance.transform.position.x)
        {

            change(speedLvl2, 0);

            Direction = -1;
            leftFromPlayer = false;

            change(0, speedLvl2);
        }

        else if (leftFromPlayer == false && transform.position.x < Character.instance.transform.position.x)
        {
            canMove = false;
            change(speedLvl2, 0);

            Direction = 1;
            leftFromPlayer = true;

            change(0, speedLvl2);
            canMove = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            Direction *= -1;
        }
    }
}
