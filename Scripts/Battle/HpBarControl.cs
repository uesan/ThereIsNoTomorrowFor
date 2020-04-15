using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ←※これを忘れずに入れる

public class HpBarControl : MonoBehaviour
{

    Slider _slider;
    void Start()
    {
        // スライダーを取得する
        _slider = GameObject.Find("AllyHpBar").GetComponent<Slider>();
        _slider.value = _slider.maxValue;
    }

    public void DecHp(int value)
    {
        _slider.value -= value;
    }

    public void SetHp(int value)
    {
        if (_slider.maxValue < value || _slider.minValue > value)
        {
            Debug.Log("スクロールバー（HPバー）の上限または下限を超えています");
            return;
        }
        _slider.value = value;

    }

    public void Attack()
    {
        DecHp(5);
    }
    /*
    void Update()
    {
        if (_slider.value < 1)
        {
            // 最大を超えたら0に戻す
            _slider.value = _slider.maxValue;
        }

        // HPゲージに値を設定
        DecHp(1);
    }*/
}
