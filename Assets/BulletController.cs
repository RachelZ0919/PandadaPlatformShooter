using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private UnityEngine.Vector3 shootDir;
    Rigidbody2D rbody;
    public float moveSpeed = 240f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 8f);
        rbody = GetComponent<Rigidbody2D>();
        rbody.AddForce(-transform.right * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            Destroy(this.gameObject);
        }
    }
}
