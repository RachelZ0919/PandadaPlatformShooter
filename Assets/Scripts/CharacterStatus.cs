using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    public int HP = 100;
    public float attackDistance = 0.5f;
    public float attackCooldown = 5f; //攻击间隔
    public GameObject bulletPrefab;
    private GameObject tempBullet;
    [Tooltip("视野范围")]
    public float sightDistance = 5f;

    public void Attack(Vector3 targetPosition)
    {
        tempBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 shootDir = (transform.position - targetPosition).normalized;
        float angle = Vector2.Angle(shootDir, Vector2.right);
        if (shootDir.y < 0)
        {
            angle = -angle;
        }
        Debug.Log("angle" + angle);
        tempBullet.GetComponent<Transform>().eulerAngles = new Vector3(0, 0, angle);
    }

}
