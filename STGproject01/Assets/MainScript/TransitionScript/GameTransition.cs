using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTransition : MonoBehaviour
{
    //GameSceneに遷移する。
    public void toGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    //Titleに遷移する。
    public void toTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
