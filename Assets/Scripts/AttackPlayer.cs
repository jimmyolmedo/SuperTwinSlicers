using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.GraphicsBuffer;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] GameObject direction;

    [SerializeField] LayerMask enemyLayer;

    [SerializeField] bool Attaking;

    [SerializeField] RigidbodyMovement rigidbodyMovement;

    [SerializeField] Animator animator;

    public float x;

    public bool CanAttack {  get; private set; }

    private void Awake()
    {
        CanAttack = true;
    }

    private void Update()
    {

    }

    public void StartedAirAttack()
    {
        if(rigidbodyMovement.isGrounded() == false)
        {
            //activar la rueda(ataque)
            direction.gameObject.SetActive(true);
            Debug.Log("atacando");

            //realentizar el tiempo
            Time.timeScale = 0.05f;

            Attaking = true;
            CanAttack = false;
        }
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

        //rigidbodyMovement.InvokeInpulse(direction.transform.position - transform.position, 10, 0.5f);
        LookAt();

        Vector3 localscale = transform.localScale;
        localscale.y = Mathf.Clamp(direction.transform.position.x, -1, 1);
        localscale.y = Mathf.Sign(localscale.y);
        transform.localScale = localscale;


        CanAttack = true;
    }

    public void NormalAttack()
    {
        if (!CanAttack) return;

        animator.SetTrigger("Attack");
    }

    public void DisableAttack()
    {
        CanAttack = false;
    }

    public void EnableAttack()
    {
        CanAttack = true;
    }


    void LookAt()
    {
        Vector3 Targetdirection = direction.transform.position - transform.position;

        // Calcula el ángulo en grados
        float angle = Mathf.Atan2(Targetdirection.y, Targetdirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Attaking)
        {
            RecoverPos();
        }
    }

    public void RecoverPos()
    {
        x = Mathf.Cos(transform.position.x);
        transform.localScale = new Vector3(transform.localScale.x, 1,1);
        transform.rotation = new Quaternion(0,0, 0, 0);
        Attaking = false;
    }

    private void OnDrawGizmos()
    {

    }


}
