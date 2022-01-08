using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerArea : MonoBehaviour
{
    public GameObject player;
    public GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().DangerArea == true)
        {
            Panel.SetActive(true);

        }else if(player.GetComponent<PlayerController>().DangerArea == false)
        {
            Panel.SetActive(false);
        }
    }
}
