using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMainButton : MonoBehaviour
{
    public void toGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
