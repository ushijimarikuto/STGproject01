using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CliarArea : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().CliarArea == true)
        {
            SceneManager.LoadScene("CliarScene");
        }
    }
}
