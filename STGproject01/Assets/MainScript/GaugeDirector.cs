using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GaugeDirector : MonoBehaviour
{
    GameObject LifeGauge;
    GameObject BulletGauge;

    // Start is called before the first frame update
    void Start()
    {
        this.LifeGauge = GameObject.Find("LifeGauge");
        this.BulletGauge = GameObject.Find("BulletGauge");

    }


    // HPを減らす処理
    public void DecreaseHp()
    {
        this.LifeGauge.GetComponent<Image>().fillAmount -= 0.1f;
    }

    // 弾薬ゲージを減らす処理
    public void DecreaseAmmoGauge()
    {
        this.BulletGauge.GetComponent<Image>().fillAmount -= 0.05f;
    }

    // 弾薬ゲージを増やす処理
    public void RiseAmmoGauge()
    {
        this.BulletGauge.GetComponent<Image>().fillAmount += Time.deltaTime;
    }
}