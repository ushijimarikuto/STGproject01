using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyA : MonoBehaviour
{
 
    public GameObject player;
    public GameObject Bullet;

    public float speed = 150f;
   
    void Start()
    {
        player = GameObject.Find("Player");

        StartCoroutine("EnemyAshot");
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
 
    IEnumerator EnemyAshot()
    {

        while (true)
        {
            if (player.GetComponent<PlayerController>().EnemyAArea == true)
            {
                var shot = Instantiate(Bullet, transform.position, Quaternion.identity);
                shot.GetComponent<Rigidbody>().velocity = transform.forward.normalized * speed;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
