using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected int maxLife;
    protected int currenLife;

    public int CurrenLife
    {
        get => currenLife;

        set
        {
            currenLife = value;
            currenLife = Mathf.Clamp(currenLife, 0, maxLife);
            if(currenLife <= 0)
            {
                Die();
            }
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
