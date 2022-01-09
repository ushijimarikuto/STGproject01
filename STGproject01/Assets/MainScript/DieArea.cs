using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieArea : MonoBehaviour
{
    public GameObject player;
    public GameObject ExploadObj;
    public GameObject ExploadPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().DieArea == true)
        {
            StartCoroutine("PlayerDie");
        }
    }

    IEnumerator PlayerDie()
    {
        Instantiate (ExploadObj, ExploadPos.transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("GameOverScene");
    }
}
