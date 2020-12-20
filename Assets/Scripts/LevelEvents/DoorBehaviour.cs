using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic.Managers;

/// <summary>
/// 传送门
/// </summary>
public class DoorBehaviour : MonoBehaviour
{
    public int nextLevel;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GoToLevel(nextLevel);
        }
    }

    public void Spawn()
    {
        animator.SetBool("isOpen", true);
    }
}
