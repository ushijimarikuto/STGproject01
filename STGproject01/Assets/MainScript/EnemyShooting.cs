using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyShooting : MonoBehaviour
{
 
    public GameObject player;
    public GameObject Bullet;

    public float speed = 150.0f;
   
    void Start()
    {
        player = GameObject.Find("Player");

        StartCoroutine("shotEBullet");
    }

 
    void FixedUpdate()
    {
        transform.LookAt(player.transform);
    }
 
    IEnumerator shotEBullet()
    {

        while (true)
        {
            if (player.GetComponent<PlayerController>().EnemyArea == true)
            {
                var shot = Instantiate(Bullet, transform.position, Quaternion.identity);
                shot.GetComponent<Rigidbody>().velocity = transform.forward.normalized * speed;
            }

            yield return new WaitForSeconds(2.0f);
        }
    }
}
