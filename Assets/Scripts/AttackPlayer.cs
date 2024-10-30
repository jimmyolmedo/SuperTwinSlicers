using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] GameObject direction;

    [SerializeField] LayerMask enemyLayer;

    [SerializeField] bool Attaking;

    [SerializeField] RigidbodyMovement rigidbodyMovement;

    [SerializeField] Animator animator;

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

        //IEnumerator LerpQuaternion()
        //{
        //    for(float i  = 0; i < 0.01f;  i += Time.deltaTime)
        //    {
        //        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, i/0.01f);
        //        yield return null;
        //    }
        //}

        //StartCoroutine(LerpQuaternion());

        Quaternion rotation = transform.rotation;
        rotation.z = 0;
        transform.rotation = rotation;

        //Vector3 localscale = transform.localScale;
        //localscale.x = Mathf.Clamp(direction.transform.position.x, -1, 1);
        //localscale.x = Mathf.Sign(localscale.x);
        //transform.localScale = localscale;

        Attaking = false;
    }

    private void OnDrawGizmos()
    {

    }


}
