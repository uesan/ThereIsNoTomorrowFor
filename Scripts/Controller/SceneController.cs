using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    Tmp tmp;
    // ボタンをクリックするとBattleSceneに移動します
    public void ChangeSceneToBattle()
    {
        //　イベントに登録
        SceneManager.sceneLoaded += GameSceneLoaded;
        //　シーンの切替
        SceneManager.LoadScene("Battle");
    }
    public void ChangeSceneToTitle()
    {
        SceneManager.LoadScene("Title");
    }
    
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        var gameManager = GameObject.Find("BattleManager").GetComponent<Battle>();

        gameManager.Ally_player = tmp.ally_player;
        gameManager.Enemy_player = tmp.enemy_player;
        // イベントから削除
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

    /// <summary>
    /// エラーが出るのが嫌すぎてここだけ慣れないtry文使った。
    /// 今後はもっと使いたい。
    /// 参考 https://ufcpp.net/study/csharp/oo_exception.html
    /// </summary>
    void Start()
    {
        try
        {
            tmp = GameObject.Find("Test").GetComponent<Tmp>();
        }
        catch (NullReferenceException)
        {
            Debug.Log("TestがSceneに存在しません");
        }
    }
    
}
