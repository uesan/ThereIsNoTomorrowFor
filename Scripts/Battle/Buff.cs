using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectRange
{
    Top, All,
}

[CreateAssetMenu(menuName = "MyScriptable/Create BuffData")]
public class Buff : ScriptableObject
{
    public int attack;
    public int defense;
    public int speed;
    public int max_rest_turn;
    public EffectRange range;
    public bool is_const;

    private int now_rest_turn;

    public int Now_rest_turn { get => now_rest_turn; set => now_rest_turn = value; }
}
