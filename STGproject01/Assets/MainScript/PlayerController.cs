using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float speed = 100;
    public float moveForceMultiplier;
    public GameObject LifeGauge;
    public GameObject ExploadObj;
    public GameObject ExploadPos;
    public bool EnemyAArea, EnemyBArea, EnemyCArea, DangerArea , DieArea , CliarArea = false;

    //音声ファイル格納用変数
    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource;


    // 水平移動時に機首を左右に向けるトルク
    public float yawTorqueMagnitude = 20.0f;

    // 垂直移動時に機首を上下に向けるトルク
    public float pitchTorqueMagnitude = 40.0f;

    // 水平移動時に機体を左右に傾けるトルク
    public float rollTorqueMagnitude = 20.0f;

    // バネのように姿勢を元に戻すトルク
    public float restoringTorqueMagnitude = 20.0f;

    private Vector3 Player_pos;
    private new Rigidbody rigidbody;



    void Awake()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
        
        rigidbody = GetComponent<Rigidbody>();
        // バネ復元力でゆらゆら揺れ続けるのを防ぐため、angularDragを大きめにしておく
        rigidbody.angularDrag = 25.0f;
    }



    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // xとyにspeedを掛ける
        rigidbody.AddForce(x * speed, y * speed, 15);
        Vector3 moveVector = Vector3.zero;
        rigidbody.AddForce(moveForceMultiplier * (moveVector - rigidbody.velocity));


        // プレイヤーの入力に応じて姿勢をひねろうとするトルク
        Vector3 rotationTorque = new Vector3(-y * pitchTorqueMagnitude, x * yawTorqueMagnitude, -x * rollTorqueMagnitude);


        // 現在の姿勢のずれに比例した大きさで逆方向にひねろうとするトルク
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Vector3 forward = transform.forward;
        Vector3 restoringTorque = new Vector3(forward.y - up.z, right.z - forward.x, up.x - right.y) * restoringTorqueMagnitude;

        // 機体にトルクを加える
        rigidbody.AddTorque(rotationTorque + restoringTorque);

        if (LifeGauge.GetComponent<Image>().fillAmount <= 0)
        {
            StartCoroutine("PlayerDie");
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //敵に当たるとダメージを受ける処理
            GameObject director = GameObject.Find("GaugeDirector");
            director.GetComponent<GaugeDirector>().DecreaseHp();

            //当たった敵を非表示にする
            other.gameObject.SetActive(false);

            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound1);
        }

        if (other.gameObject.tag == "EBullet")
        {
            //敵の弾に当たるとダメージを受ける処理
            GameObject director = GameObject.Find("GaugeDirector");
            director.GetComponent<GaugeDirector>().DecreaseHp();

            //当たった敵を非表示にする
            other.gameObject.SetActive(false);

            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound1);
        }
        
        if (other.gameObject.name == "EnemyAArea")
        {
            EnemyAArea = true;
        }

        if (other.gameObject.name == "EnemyBArea")
        {
            EnemyBArea = true;
        }

        if (other.gameObject.name == "EnemyCArea")
        {
            EnemyCArea = true;
        }

        if (other.gameObject.name == "DieArea")
        {
            DieArea = true;
            StartCoroutine("PlayerDie");
        }
        
        if (other.gameObject.name == "CliarArea")
        {
            CliarArea = true;
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            //地形に当たるとダメージを受ける処理
            GameObject director = GameObject.Find("GaugeDirector");
            director.GetComponent<GaugeDirector>().DecreaseHp();

            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound1);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "EnemyAArea")
        {
            EnemyAArea = false;
        }

        if (other.gameObject.name == "EnemyBArea")
        {
            EnemyBArea = false;
        }

        if (other.gameObject.name == "EnemyCArea")
        {
            EnemyCArea = false;
        }

        if (other.gameObject.name == "DangerArea")
        {
            DangerArea = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "DangerArea")
        {
            DangerArea = true;
        }
    }

    IEnumerator PlayerDie()
    {
        //音(sound2)を鳴らす
        audioSource.PlayOneShot(sound2);
        Instantiate (ExploadObj, ExploadPos.transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("GameOverScene");
    }
}
