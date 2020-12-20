using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMovingController : MonoBehaviour
{
    public Transform[] wayPoints;
    private int index;
    public float moveSpeed;


    void Start()
    {
        int length = wayPoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (wayPoints[index].GetComponent<Collider2D>().OverlapPoint(transform.position))
        {
            index = (index + 1) % wayPoints.Length;
        }

        transform.position = Vector3.MoveTowards(transform.position,
            wayPoints[index].position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 9 || collision.gameObject.layer == 10))
        {
            collision.transform.parent = this.transform;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 9 || collision.gameObject.layer == 10))
        {
            collision.transform.parent = this.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 9 || collision.gameObject.layer == 10) && collision.transform.position.y > transform.position.y)
        {
            transform.DetachChildren();
        }
    }

}
