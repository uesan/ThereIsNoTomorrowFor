using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    /*
     * 本当に必要なもの
     * hp,lv,equipped_skill;
     */
    private MonsterData monster_data;
    //private string monster_name;
    private int hp;
    //private int max_hp;
    //private int defense;
    //private int attack;
    //private int speed;
    //private Attr attr;
    //private Skill unique_skill;
    private SkillData equiped_skill;
    //private Sprite sprite;


    public MonsterData Monster_data { get => monster_data; set => monster_data = value; }
    //public string Monster_name { get => monster_name; set => monster_name = value; }
    public int Hp { get => hp; set => hp = value; }
    //public int Max_hp { get => max_hp; set => max_hp = value; }
    //public int Defense { get => defense; set => defense = value; }
    //public int Attack { get => attack; set => attack = value; }
    //public Attr Attr { get => attr; set => attr = value; }
    //public int Speed { get => speed; set => speed = value; }
    //public Sprite Sprite { get => sprite; set => sprite = value; }

    /*
    Monster(string monster_name, int max_hp, int defense, int attack, int speed, Attr attr, Sprite sprite, MonsterData monster_data)
    {
        this.monster_name = monster_name;
        this.max_hp = max_hp;
        this.hp = max_hp;
        this.defense = defense;
        this.attack = attack;
        this.speed = speed;
        this.attr = attr;
        this.sprite = sprite;
        this.monster_data = monster_data;
    }
    public Monster(Monster copy_monster)
    {
        this.monster_name = copy_monster.monster_name;
        this.max_hp = copy_monster.max_hp;
        this.hp = copy_monster.max_hp;
        this.defense = copy_monster.defense;
        this.attack = copy_monster.attack;
        this.speed = copy_monster.speed;
        this.attr = copy_monster.attr;
        this.unique_skill = copy_monster.unique_skill;
        this.equiped_skill = copy_monster.equiped_skill;
        this.monster_data = copy_monster.monster_data;
    }

    public static Monster createNewMonster(string monster_name, int max_hp, int defense, int attack, int speed, Attr attr, Sprite sprite, MonsterData monster_data)
    {
        Monster monster = new Monster(monster_name, max_hp, defense, attack, speed, attr, sprite, monster_data);
        MonsterManager MonsterManager = MonsterManager.GetInstance();
        MonsterManager.AddCreatedMonster(monster);
        return monster;
    }
    */
    Monster(MonsterData monster_data)
    { 
        this.hp = monster_data.max_hp;
        this.monster_data = monster_data;
    }
    public Monster(Monster copy_monster)
    {
        this.hp = copy_monster.Hp;
        this.equiped_skill = copy_monster.equiped_skill;
        this.monster_data = copy_monster.monster_data;
    }

    public static Monster createNewMonster(MonsterData monster_data)
    {
        Monster monster = new Monster(monster_data);
        MonsterManager MonsterManager = MonsterManager.GetInstance();
        MonsterManager.AddCreatedMonster(monster);
        return monster;
    }

    public string Explain()
    {
        string sentence = "";
        sentence += "モンスター名：" + this.monster_data.monster_name +
            "\n攻撃：" + this.monster_data.attack + " 防御：" + this.monster_data.defense +
            "\nすばやさ：" + this.monster_data.speed + "　属性：" + this.monster_data.attr.ToString() +
            "\nHP：" + this.Hp + " / " + this.monster_data.max_hp + "\n";
        //sentence += Explanation + "\n";

        return sentence;
    }
}
