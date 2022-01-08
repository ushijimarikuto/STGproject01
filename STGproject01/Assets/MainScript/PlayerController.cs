using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float speed = 70;
    public float moveForceMultiplier;
    public bool EnemyArea;
    public bool DangerArea = false;


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
        rigidbody = GetComponent<Rigidbody>();
        // バネ復元力でゆらゆら揺れ続けるのを防ぐため、angularDragを大きめにしておく
        rigidbody.angularDrag = 25.0f;
    }



    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // xとyにspeedを掛ける
        rigidbody.AddForce(x * speed, y * speed, 10);
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
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject director = GameObject.Find("GaugeDirector");
            director.GetComponent<GaugeDirector>().DecreaseHp();

            other.gameObject.SetActive(false);
            //当たった敵は削除する
            // Object.Destroy(other.gameObject);
        }
        
        if (other.gameObject.name == "EnemyArea")
        {
            EnemyArea = true;
        }
        
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            GameObject director = GameObject.Find("GaugeDirector");
            director.GetComponent<GaugeDirector>().DecreaseHp();
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "EnemyArea")
        {
            EnemyArea = false;
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
}
