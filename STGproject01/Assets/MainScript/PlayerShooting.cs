using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    // bullet prefab
    public GameObject bullet;

    public float BulletCount = 0;

    // 弾丸発射点
    public Transform muzzle;

    // 時間の変数
    private float seconds;

    // クールタイム
    public float waitTime = 0.1f;


    Dictionary<string, bool> move = new Dictionary<string, bool>
    {
        {"shot", false }
    };


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move["shot"] = Input.GetKey("space");      
    }


    void FixedUpdate()
    {
        seconds += Time.deltaTime;

        if (BulletCount <= 12)
        {
            // spaceキーが押された時
            if (move["shot"] & seconds >= waitTime)
            {
                // 弾丸の複製
                GameObject bullets = Instantiate(bullet) as GameObject;

                Vector3 force;

                force = this.gameObject.transform.forward * 10f;

                // Rigidbodyに力を加えて発射
                bullets.GetComponent<Rigidbody>().AddForce(force);

                // 弾丸の位置を調整
                bullets.transform.position = muzzle.position;

                GameObject director = GameObject.Find("GaugeDirector");
                director.GetComponent<GaugeDirector>().DecreaseAmmoGauge();
                BulletCount += 1.0f;

                seconds = 0;
            }

        }else if(BulletCount >= 13)
        {
            StartCoroutine("shotTimer");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // 衝突した相手を闇の彼方に消し去ります。
            Destroy(other.gameObject);
        }
    }

    IEnumerator shotTimer()
    {
        GameObject director = GameObject.Find("GaugeDirector");
        director.GetComponent<GaugeDirector>().RiseAmmoGauge();

        yield return new WaitForSeconds(1.0f);

        BulletCount = 0;
    }
}
