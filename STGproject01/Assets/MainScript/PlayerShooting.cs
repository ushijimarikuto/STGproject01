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

    //音声ファイル格納用変数
    public AudioClip sound1;
    AudioSource audioSource;


    Dictionary<string, bool> move = new Dictionary<string, bool>
    {
        {"shot", false }
    };

    void Start () 
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        move["shot"] = Input.GetKey("space");      
    }


    void FixedUpdate()
    {
        seconds += Time.deltaTime;

        if (BulletCount <= 15)
        {
            // spaceキーが押された時
            if (move["shot"] & seconds >= waitTime)
            {
                //音(sound1)を鳴らす
                audioSource.PlayOneShot(sound1);

                // 弾丸の複製
                GameObject bullets = Instantiate(bullet) as GameObject;

                Vector3 force;

                force = this.gameObject.transform.forward * 5f;

                // Rigidbodyに力を加えて発射
                bullets.GetComponent<Rigidbody>().AddForce(force);

                // 弾丸の位置を調整
                bullets.transform.position = muzzle.position;

                //弾のゲージを減らす処理
                GameObject director = GameObject.Find("GaugeDirector");
                director.GetComponent<GaugeDirector>().DecreaseAmmoGauge();
                BulletCount += 1.0f;

                seconds = 0;
            }

        }else if(BulletCount >= 16)
        {
            StartCoroutine("shotTimer");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // 衝突した相手を闇の彼方に消し去ります
            Destroy(other.gameObject);
        }
    }

    IEnumerator shotTimer()
    {
        //弾のゲージを増やす処理
        GameObject director = GameObject.Find("GaugeDirector");
        director.GetComponent<GaugeDirector>().RiseAmmoGauge();

        yield return new WaitForSeconds(1.0f);

        BulletCount = 0;
    }
}
