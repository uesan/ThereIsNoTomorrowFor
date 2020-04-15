using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : ScriptableObject
{
    public const int MAX_BATTLE_SKILLS_NUM = 7;
    public const int SKILL_COOL_TURN = 2;
    bool initialize = false;

    private static SkillManager singleton = new SkillManager();
    private SkillData exchange_skill = Resources.Load<SkillData>("Data/SkillData/Exchange");

    private SkillManager()
    {

    }

    public static SkillManager GetInstance()
    {
        return singleton;
    }

    private List<SkillData> skill_List = new List<SkillData>();

    private int count_all_skills = 0;

    public List<SkillData> Skill_List { get => skill_List; set => skill_List = value; }
    public SkillData Exchange_skill { get => exchange_skill; set => exchange_skill = value; }

    /// <summary>
    /// スキルを生成する時に呼ばれるメソッドで、生成されたスキルをスキル一覧(List)に追加する。
    /// </summary>
    /// <param name="skill">生成されたスキル</param>
    public void AddCreatedSkill(SkillData skill)
    {
        skill_List.Add(skill);
        count_all_skills++;
        return;
    }

    /// <summary>
    /// スキルの作成を一括でしてしまうつもりのスキル初期セットメソッド
    /// Resources.Loadを使いすぎると、重くなるらしいので、AssetBundleというのを使うべきらしい
    /// 参考 https://qiita.com/k7a/items/df6dd8ea66cbc5a1e21d
    /// </summary>
    public void InitSkillManager()
    {
        if (initialize == true)
            return;
        /*
        SkillData.createNewSkill(40, Attr.neutral, "掌底");
        SkillData.createNewSkill(50, Attr.dark, "吸収");
        SkillData.createNewSkill(60, Attr.holy, "聖別");
        SkillData.createNewSkill(70, Attr.fire, "火炎");
        SkillData.createNewSkill(80, Attr.wild, "蔓の刺突");
        SkillData.createNewSkill(90, Attr.water, "水遁 ~滝~");
        SkillData.createNewSkill(100, Attr.electro, "感電");
        SkillData.createNewSkill(110, Attr.earth, "隆起");
        */
        SkillData[] skill_Data = Resources.LoadAll<SkillData>("Data/SkillData");
        foreach(SkillData skill in skill_Data)
        {
            if(skill.skill_type != SkillType.exchange)
            {
                AddCreatedSkill(skill);
            }
        }
        /*
        AddCreatedSkill(Resources.Load<SkillData>("Data/SkillData/Shotei"));
        AddCreatedSkill(Resources.Load<SkillData>("Data/SkillData/Kyushu"));
        AddCreatedSkill(Resources.Load<SkillData>("Data/SkillData/Seibetsu"));
        AddCreatedSkill(Resources.Load<SkillData>("Data/SkillData/Kaen"));
        AddCreatedSkill(Resources.Load<SkillData>("Data/SkillData/Turunoshitotsu"));
        AddCreatedSkill(Resources.Load<SkillData>("Data/SkillData/Suiton_taki"));
        AddCreatedSkill(Resources.Load<SkillData>("Data/SkillData/Kanden"));
        AddCreatedSkill(Resources.Load<SkillData>("Data/SkillData/Ryuki"));
        AddCreatedSkill(Resources.Load<SkillData>("Data/SkillData/Heisei"));
        */
        //Debug.Log("ああああ");
        initialize = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
