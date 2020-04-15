using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create MonsterData")]
public class MonsterData : ScriptableObject
{
    public string monster_name;
    public int max_hp;
    public int defense;
    public int attack;
    public int speed;
    public Attr attr;
    public Sprite sprite;

    /*
     * スプライトを入れるかどうかは考える余地あり？
    private string monster_name;
    private int max_hp;
    private int defense;
    private int attack;
    private int speed;
    private Attr attr;

    public string Monster_name { get => monster_name; set => monster_name = value; }
    public int Max_hp { get => max_hp; set => max_hp = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Attack { get => attack; set => attack = value; }
    public int Speed { get => speed; set => speed = value; }
    public Attr Attr { get => attr; set => attr = value; }
    */
}