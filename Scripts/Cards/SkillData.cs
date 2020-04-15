using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// スキルやモンスターの属性にあたる列挙型
/// </summary>
///

public enum SkillType
{
    attack, support, special, exchange, count
}

[CreateAssetMenu(menuName = "MyScriptable/Create SkillData")]
public class SkillData : ScriptableObject
{
    public int damage;
    public Attr attr;
    public string skill_name;
    public string explanation;
    public SkillType skill_type;
    public int priority;
    public bool buff_to_ally = false;
    public Buff buff;

    /*
    private SkillData(int damage, Attr attr, string skill_name)
    {
        this.damage = damage;
        this.attr = attr;
        this.skill_name = skill_name;
    }

    /// <summary>
    /// SkillManagerに使用されることを想定されたインスタンス生成メソッド
    /// このメソッド自身がSkillManagerのインスタンス（シングルトン）を取得している
    /// </summary>
    /// <param name="damage">スキルのダメージ</param>
    /// <param name="attr">スキルの属性</param>
    /// <param name="skill_name">スキルの名前</param>
    /// <returns>生成されたインスタンス</returns>
    public static SkillData createNewSkill(int damage, Attr attr, string skill_name)
    {
        SkillData skill = new SkillData(damage, attr, skill_name);
        SkillManager skillManager = SkillManager.GetInstance();
        skillManager.AddCreatedSkill(skill);
        return skill;
    }
    */

    public void AddBuff(Player from_player, Player to_player)
    {
        if (buff_to_ally)
            from_player.Buff_manager.AddBuff(buff);
        else
            to_player.Buff_manager.AddBuff(buff);
        
    }

    public string Explain()
    {
        string sentence = "";
        sentence += "スキル名：" + this.skill_name + "　威力：" + this.damage + "　属性：" + this.attr.ToString();
        sentence += "\n" + explanation + "\n";

        return sentence;
    }
}
