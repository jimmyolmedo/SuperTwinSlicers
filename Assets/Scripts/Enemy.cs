using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
     [SerializeField] protected float level = 0;

    public float Level
    {
        get => level;

        set
        {
            level = value;
            level = Mathf.Clamp(level, 0, 2);
        }
    }

    protected void LvlBehavior()
    {
        if(level == 0)
        {
            Lvl1();
        }
        else if(level == 1)
        {
            Lvl2();
        }
        else if (level == 2)
        {
            Lvl3();
        }
    }

    protected abstract void Lvl1();

    protected abstract void Lvl2();

    protected abstract void Lvl3();
}
