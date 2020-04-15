using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Async;
/// <summary>
/// カードゲームをプレイするプレイヤー、敵、対戦相手を表すクラス
/// </summary>
public class Player : MonoBehaviour
{
    private List<SkillData> battle_skills = new List<SkillData>();
    private List<Monster> battle_monsters = new List<Monster>();
    private List<Monster> monsters_on_battle = new List<Monster>();
    private Monster top_monster;
    private SkillData next_use_skill;
    private bool win_flag;
    private bool top_monster_death;
    private bool is_ally;
    private string player_name;
    private int[] skill_CoolTurn = new int[SkillManager.MAX_BATTLE_SKILLS_NUM];
    private BuffManager buff_manager;



    public List<SkillData> Battle_skills { get => battle_skills; set => battle_skills = value; }
    public List<Monster> Battle_monsters { get => battle_monsters; set => battle_monsters = value; }
    public List<Monster> Monsters_on_battle { get => monsters_on_battle; set => monsters_on_battle = value; }
    public Monster Top_monster { get => top_monster; set => top_monster = value; }
    public SkillData Next_use_skill { get => next_use_skill; set => next_use_skill = value; }
    public bool Win_flag { get => win_flag; set => win_flag = value; }
    public bool Top_monster_death { get => top_monster_death; set => top_monster_death = value; }
    public bool Is_ally { get => is_ally; set => is_ally = value; }
    public string Player_name { get => player_name; set => player_name = value; }
    public int[] Skill_CoolTurn { get => skill_CoolTurn; set => skill_CoolTurn = value; }
    public BuffManager Buff_manager { get => buff_manager; set => buff_manager = value; }

    public Player(bool is_ally, string name)
    {
        this.is_ally = is_ally;
        this.player_name = name;
    }

    public void InitBattle()
    {
        for (int i = 0; i < MonsterManager.MAX_BATTLE_MONSTERS_NUM; i++)
        {
            if (Battle_monsters.Count > i)
            {
                Monster monster = new Monster(Battle_monsters[i]);
                Monsters_on_battle.Add(monster);
                Debug.Log(Battle_monsters[i]);
            }
            //Monster allymonster = new Monster(Ally_player.Battle_monsters[i]);
            //Debug.Log(allymonster);
            //this.Ally_player.Monsters_on_battle.Add(allymonster);
            //Monster enemymonster = new Monster(enemy_player.Battle_monsters[i]);
            //Debug.Log(enemymonster);
            //this.Enemy_player.Monsters_on_battle.Add(enemymonster);
        }
        Buff_manager = new BuffManager(this);
        Buff_manager.Init();
    }

    public bool AddToBattleSkills(SkillData skill)
    {
        return AddToBattleCards(battle_skills, SkillManager.MAX_BATTLE_SKILLS_NUM, skill);
    }

    public bool AddToBattleMonsters(Monster monster)
    {
        return AddToBattleCards(battle_monsters, MonsterManager.MAX_BATTLE_MONSTERS_NUM, monster);
    }

    private bool AddToBattleCards<Type>(List<Type> battle_cards, int max, Type card)
    {
        if (battle_cards.Count < max)
        {
            battle_cards.Add(card);
            return true;
        }
        Debug.Log("これ以上カードを追加できません");
        return false;
    }

    public void SetNextSkill(SkillData skill)
    {
        this.next_use_skill = skill;
    }

    public void SetNextSkill(int num)
    {
        if (num < SkillManager.MAX_BATTLE_SKILLS_NUM)
            this.next_use_skill = battle_skills[num];
    }

    public bool RemoveSkills(int num)
    {
        if (num >= SkillManager.MAX_BATTLE_SKILLS_NUM)
        {
            Debug.Log("デッキの最大枚数より大きい数でアクセスしています");
            return false;
        }
        if(num >= battle_skills.Count)
        {
            Debug.Log("デッキの枚数より大きい数でアクセスしています");
            return false;
        }
        battle_skills.RemoveAt(num);
        return true;
    }

    public void ReduceAllCoolTurn(int turn)
    {
        for(int count=0; count < battle_skills.Count; count++)
        {
            skill_CoolTurn[count] -= turn;
            if (skill_CoolTurn[count] < 0)
            {
                skill_CoolTurn[count] = 0;
            }
        }
    }

    public void IntoCoolTurn(int skill_num)
    {
        skill_CoolTurn[skill_num] = SkillManager.SKILL_COOL_TURN;
    }

    public void PastTurnStart()
    {
        if (!is_ally)
        {
            int num = Random.Range(0, SkillManager.MAX_BATTLE_SKILLS_NUM);
            SetNextSkill(num);
        }
        ReduceAllCoolTurn(1);
        return;
    }

    async public void PastTurnEnd(Battle battle)
    {
        Buff_manager.PastTurn();
        if (Top_monster_death)
        {
            foreach ( Monster monster in monsters_on_battle)
            {
                if(monster.Hp > 0)
                {
                    SkillManager skillManager = SkillManager.GetInstance();
                    battle.ExchangeMonster(this, monster);
                    battle.Ally_player.Buff_manager.ExchangeEffect();
                    string sentence = player_name + "は、" + monster.Monster_data.monster_name + "をくりだした！";
                    await UniTask.Delay(1000);
                    battle.ChangeBattleText(sentence);
                    break;
                }
            }
        }
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
