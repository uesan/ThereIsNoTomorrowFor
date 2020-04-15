using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 起動時に動くやつ用のクラスウィンドウの設定とか。
/// </summary>
public class GameInitial
{
    /// <summary>
    /// 参考 http://h-sao.com/blog/2016/05/08/change-unity-window-size/
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(1800, 960, false, 60);
        SceneManager.LoadScene("Title");
    }

}
