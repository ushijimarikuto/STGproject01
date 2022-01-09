using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour
{
 
    public GameObject player;
    public GameObject Bullet;

    public float speed = 250f;
   
    void Start()
    {
        player = GameObject.Find("Player");

        StartCoroutine("EnemyBshot");
    }

 
    void FixedUpdate()
    {
        transform.LookAt(player.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PBullet")
        {
            //消滅
            Destroy(this.gameObject);
        }
	}
 
    IEnumerator EnemyBshot()
    {

        while (true)
        {
            if (player.GetComponent<PlayerController>().EnemyBArea == true)
            {
                var shot = Instantiate(Bullet, transform.position, Quaternion.identity);
                shot.GetComponent<Rigidbody>().velocity = transform.forward.normalized * speed;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
