using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    [SerializeField] Transform player;
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        transform.localScale = player.localScale;
    }
}
