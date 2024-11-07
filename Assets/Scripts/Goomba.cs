using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Goomba : Enemy, Idamageable
{

    [SerializeField] float speedLvl1;
    [SerializeField] float speedLvl2;
    [SerializeField] float direction = 1;
    [SerializeField] bool canMove;
    [SerializeField] Rigidbody2D rB;
    Transform player;
    [SerializeField] bool leftFromPlayer;
     bool slowingDown;

    private void Awake()
    {
        player = Character.instance.transform;
    }

    private void Update()
    {
        player = Character.instance.transform;
    }

    private void FixedUpdate()
    {
        LvlBehavior();
    }

    void Move(float _speed)
    {
        if (canMove)
        {
            rB.velocity = new Vector2(direction * _speed * Time.deltaTime, 0);
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
        IEnumerator change()
        {
            if (!slowingDown) yield break;

            // Desacelera gradualmente hasta que la velocidad llegue a 0.
            float initialSpeed = speedLvl2;
            for (float i = 0; i < 1.5f; i += Time.deltaTime)
            {
                speedLvl2 = Mathf.Lerp(initialSpeed, 0f, i / 1.5f);
                yield return null;
            }

            // Asegúrate de que la velocidad sea exactamente 0 y cambia la dirección.
            speedLvl2 = 0f;
            leftFromPlayer = !leftFromPlayer; // Cambia la dirección.
            direction = leftFromPlayer ? 1f : -1f;

            // Pausa breve antes de restaurar la velocidad.
            yield return new WaitForSeconds(0.5f);
            RestoreSpeed();
            slowingDown = false;
        }

        
        // Verifica si el objeto ha pasado la posición del jugador.
        if (!slowingDown && HasPassedPlayer())
        {
            slowingDown = true;
            StartCoroutine(change());
        }
    }

    void RestoreSpeed()
    {
        speedLvl2 = 800f;
    }

    bool HasPassedPlayer()
    {
        // Detecta si el objeto ha pasado al jugador dependiendo de la dirección
        if (direction == 1f && transform.position.x >= player.position.x)
        {
            return true; // Ha pasado al jugador moviéndose a la derecha
        }
        else if (direction == -1f && transform.position.x <= player.position.x)
        {
            return true; // Ha pasado al jugador moviéndose a la izquierda
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            direction *= -1;
        }
    }
}
