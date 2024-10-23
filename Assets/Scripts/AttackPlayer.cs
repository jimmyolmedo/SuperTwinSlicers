using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] GameObject direction;

    [SerializeField] LayerMask enemyLayer;

    private void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //activar la rueda(ataque)
            direction.gameObject.SetActive(true);
            Debug.Log("atacando");

            //realentizar el tiempo
            Time.timeScale = 0.05f;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //desactivar la rueda
            direction.gameObject.SetActive(false);
            Debug.DrawRay(direction.transform.position, direction.transform.position - transform.position, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(direction.transform.position, direction.transform.position - transform.position, 3f, enemyLayer);
            Debug.Log("he atacado");

            //volver el tiempo a la normalidad
            Time.timeScale = 1.0f;

            //ejecutar ataque
        }


    }

    private void OnDrawGizmos()
    {
       Gizmos.color = Color.red;

        Gizmos.DrawRay(direction.transform.position, direction.transform.position - transform.position);
    }


}
