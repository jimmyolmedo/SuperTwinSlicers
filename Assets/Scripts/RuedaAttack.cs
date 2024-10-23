using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuedaAttack : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] float distance;


    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        float xx = player.position.x - mousePos.x;
        float yy = player.position.y - mousePos.y;
        float rad = Mathf.Atan2(yy, xx);

        float x = Mathf.Cos(rad) * distance;
        float y = Mathf.Sin(rad) * distance;

        transform.localPosition = new Vector3(x, y);
    }
}
