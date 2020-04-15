using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : ScriptableObject
{
    public const int MAX_BATTLE_MONSTERS_NUM = 3;
    bool initialize = false;

    private static MonsterManager singleton = new MonsterManager();

    private MonsterManager()
    {

    }

    public static MonsterManager GetInstance()
    {
        return singleton;
    }

    private List<Monster> monster_List = new List<Monster>();
    //public List<Monster> monster_List = new List<Monster>();

    private int count_all_monsters = 0;

    public List<Monster> Monster_List { get => monster_List; set => monster_List = value; }

    public void AddCreatedMonster(Monster monster)
    {
        monster_List.Add(monster);
        count_all_monsters++;
        return;
    }

    public void InitMonsterManager()
    {
        if (initialize == true)
            return;
        /*
        //Debug.Log("ああああ");
        MonsterData monster_data1 = Resources.Load<MonsterData>("Data/MonsterData/NeknightData");
        MonsterData monster_data2 = Resources.Load<MonsterData>("Data/MonsterData/JuriaData");
        MonsterData monster_data3 = Resources.Load<MonsterData>("Data/MonsterData/AstelferiousData");
        MonsterData monster_data4 = Resources.Load<MonsterData>("Data/MonsterData/BauslidgeData");
        MonsterData monster_data5 = Resources.Load<MonsterData>("Data/MonsterData/FeariousquinData");
        MonsterData monster_data6 = Resources.Load<MonsterData>("Data/MonsterData/RaphaelData");
        MonsterData monster_data7 = Resources.Load<MonsterData>("Data/MonsterData/ReakwyData");
        MonsterData monster_data8 = Resources.Load<MonsterData>("Data/MonsterData/RogueData");
        //Sprite neknight_sprite = Resources.Load<Sprite>("Image/Monsters/ネナイト");
        //Sprite shani_sprite = Resources.Load<Sprite>("Image/Monsters/pose_syanikamaeru_man");
        Monster.createNewMonster(monster_data1);
        Monster.createNewMonster(monster_data2);
        Monster.createNewMonster(monster_data3);
        Monster.createNewMonster(monster_data4);
        Monster.createNewMonster(monster_data5);
        Monster.createNewMonster(monster_data6);
        Monster.createNewMonster(monster_data7);
        Monster.createNewMonster(monster_data8);
        */

        MonsterData[] skill_Data = Resources.LoadAll<MonsterData>("Data/MonsterData");
        foreach (MonsterData monster in skill_Data)
        {
                Monster.createNewMonster(monster);
        }
        initialize = true;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
