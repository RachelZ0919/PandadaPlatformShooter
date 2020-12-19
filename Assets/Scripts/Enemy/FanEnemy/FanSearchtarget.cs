using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSearchtarget : MonoBehaviour
{
    public Collider2D target;
    public bool canAttack = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = other;
            canAttack = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!target && other.tag == "Player")
        {
            target = other;
            canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == target)
        {
            target = null;
            canAttack = false;
        }
    }
}
