using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] GameObject direction;

    [SerializeField] LayerMask enemyLayer;

    [SerializeField] RigidbodyMovement rigidbodyMovement;



    public void StartedAirAttack()
    {
        //activar la rueda(ataque)
        direction.gameObject.SetActive(true);
        Debug.Log("atacando");

        //realentizar el tiempo
        Time.timeScale = 0.05f;
    }

    public void EndedAirAttack()
    {
        //desactivar la rueda
        direction.gameObject.SetActive(false);

        //volver el tiempo a la normalidad
        Time.timeScale = 1.0f;

        //ejecutar ataque
        //Debug.DrawLine(direction.transform.position, (direction.transform.position - transform.position) ,Color.red, 3f);
        //RaycastHit2D hit = Physics2D.Raycast(direction.transform.position, direction.transform.position - transform.position, 3f, enemyLayer);


        Debug.Log("he atacado");
    }


    private void OnDrawGizmos()
    {

    }


}
