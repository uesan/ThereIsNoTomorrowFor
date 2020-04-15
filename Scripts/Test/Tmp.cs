using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tmp : MonoBehaviour
{
    Battle battle1;
    public Player ally_player;
    public Player enemy_player;

    public void Test()
    {
        ally_player = new Player(true, "あなた");
        enemy_player = new Player(false, "てき");
        SkillManager skill_manager = SkillManager.GetInstance();
        MonsterManager monster_manager = MonsterManager.GetInstance();
        skill_manager.InitSkillManager();
        monster_manager.InitMonsterManager();
        ally_player.AddToBattleSkills(skill_manager.Skill_List[0]);
        ally_player.AddToBattleSkills(skill_manager.Skill_List[6]);
        ally_player.AddToBattleSkills(skill_manager.Skill_List[8]);
        ally_player.AddToBattleSkills(skill_manager.Skill_List[11]);
        ally_player.AddToBattleSkills(skill_manager.Skill_List[14]);
        ally_player.AddToBattleSkills(skill_manager.Skill_List[16]);
        ally_player.AddToBattleSkills(skill_manager.Skill_List[19]);

        enemy_player.AddToBattleSkills(skill_manager.Skill_List[1]);
        enemy_player.AddToBattleSkills(skill_manager.Skill_List[7]);
        enemy_player.AddToBattleSkills(skill_manager.Skill_List[9]);
        enemy_player.AddToBattleSkills(skill_manager.Skill_List[10]);
        enemy_player.AddToBattleSkills(skill_manager.Skill_List[13]);
        enemy_player.AddToBattleSkills(skill_manager.Skill_List[17]);
        enemy_player.AddToBattleSkills(skill_manager.Skill_List[18]);

        List<int> array = new List<int>();
        int random = 0;
        bool flag = false;
        for (int i = 0; i < MonsterManager.MAX_BATTLE_MONSTERS_NUM; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                flag = false;
                random = Random.Range(0, monster_manager.Monster_List.Count);
                foreach (int num in array)
                {
                    if (num == random)
                    {
                        flag = true;
                        continue;
                    }
                }
                if (flag)
                    continue;
                array.Add(random);
                break;
            }
            ally_player.AddToBattleMonsters(monster_manager.Monster_List[random]);
        }
        for (int i = 0; i < MonsterManager.MAX_BATTLE_MONSTERS_NUM; i++)
        {
            for(int j = 0; j < 100; j++)
            {
                random = Random.Range(0, monster_manager.Monster_List.Count);
                foreach (int num in array)
                {
                    if (num == random)
                    {
                        flag = true;
                        continue;
                    }
                }
                if (flag)
                    continue;
                array.Add(random);
                break;
            }
            enemy_player.AddToBattleMonsters(monster_manager.Monster_List[random]);
        }

        //ally_player.AddToBattleMonsters(monster_manager.Monster_List[0]);
        //enemy_player.AddToBattleMonsters(monster_manager.Monster_List[1]);
        //ally_player.AddToBattleMonsters(monster_manager.monster_List[0]);
        //enemy_player.AddToBattleMonsters(monster_manager.monster_List[1]);
        Debug.Log("test : " + ally_player.Battle_monsters[0]);
        Debug.Log("test2 : " + monster_manager.Monster_List[0]);
        //Debug.Log("test2 : " + monster_manager.monster_List[0]);
        //enemy_player.SetNextSkill(6);
        //battle1.OnBattleInitalise(ally_player, enemy_player);
        //StartBattle();
    }

    public void StartBattle()
    {
        battle1 = GameObject.Find("BattleManager").GetComponent<Battle>();
        battle1.OnBattleInitalise(ally_player, enemy_player);
    }

    public void Confirm()
    {
        Debug.Log(battle1.Ally_player.Battle_monsters[0].Hp);
        Debug.Log(battle1.Ally_player.Monsters_on_battle[0].Hp);
    }

    // Start is called before the first frame update
    void Start()
    {
        Test();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
