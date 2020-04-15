using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIについて
/// https://qiita.com/ogawa-to/items/5046ca1f3e65843e4fb7
/// </summary>
public class SkillButton : MonoBehaviour
{
    public const int EQUIPSKILL_BUTTON_NUM = 1;
    public const int EXCHANGE_BUTTON_NUM = MonsterManager.MAX_BATTLE_MONSTERS_NUM;
    public const int ALL_BUTTON_NUM = SkillManager.MAX_BATTLE_SKILLS_NUM + EQUIPSKILL_BUTTON_NUM + EXCHANGE_BUTTON_NUM;
    private Image[] image_List = new Image[SkillManager.MAX_BATTLE_SKILLS_NUM];
    private List<Sprite> moons = new List<Sprite>();
    private Button[] buttons = new Button[ALL_BUTTON_NUM];
    private Battle battle = new Battle();

    public void Init(Battle battle)
    {
        this.battle = battle;
        string tmp,button_name = "Button";
        for (int i = 0;i < ALL_BUTTON_NUM;i++)
        {
            tmp = button_name + i.ToString();
            this.buttons[i] = GameObject.Find(tmp).GetComponent<Button>();
        }
        /*
        Button button0 = GameObject.Find("Button0").GetComponent<Button>();
        Button button1 = GameObject.Find("Button1").GetComponent<Button>();
        Button button2 = GameObject.Find("Button2").GetComponent<Button>();
        Button button3 = GameObject.Find("Button3").GetComponent<Button>();
        Button button4 = GameObject.Find("Button4").GetComponent<Button>();
        Button button5 = GameObject.Find("Button5").GetComponent<Button>();
        Button button6 = GameObject.Find("Button6").GetComponent<Button>();
        Button button7 = GameObject.Find("Button7").GetComponent<Button>();
        Button[] buttons = { button0, button1, button2, button3, button4, button5, button6, button7 };
        this.buttons = buttons;
        */

        int count = 0;
        foreach (SkillData skill in battle.Ally_player.Battle_skills)
        {
            Debug.Log(count);
            this.buttons[count].GetComponentInChildren<Text>().text = skill.skill_name;
            count++;
        }
        UpdateExchangeButton(battle);
    }

    public void UpdateButtons(Battle battle)
    {
        UpdateExchangeButton(battle);
        UpdateCoolTurn(battle);
    }

    public void UpdateExchangeButton(Battle battle)
    {
        for (int i = 0; i < MonsterManager.MAX_BATTLE_MONSTERS_NUM; i++)
        {
            if(battle.Ally_player.Monsters_on_battle.Count < i + 1)
            {
                this.buttons[ALL_BUTTON_NUM - EXCHANGE_BUTTON_NUM + i].interactable = false;
                continue;
            }
            if (battle.Ally_player.Monsters_on_battle[i].Monster_data.monster_name.Equals(battle.Ally_player.Top_monster.Monster_data.monster_name))
            {
                this.buttons[ALL_BUTTON_NUM - EXCHANGE_BUTTON_NUM + i].interactable = false;
            }
            else if (battle.Ally_player.Monsters_on_battle[i].Hp <= 0)
            {
                this.buttons[ALL_BUTTON_NUM - EXCHANGE_BUTTON_NUM + i].interactable = false;
            }
            else
            {
                this.buttons[ALL_BUTTON_NUM - EXCHANGE_BUTTON_NUM + i].interactable = true;
            }
        }
    }

    public void UseExchangeButton(int num)
    {
        SkillManager skillManager = SkillManager.GetInstance();
        battle.ExchangeMonster(battle.Ally_player, num);
        battle.Ally_player.Buff_manager.ExchangeEffect();
        battle.ProgressTurn(skillManager.Exchange_skill);
    }

    /*
    public void ChangeActivateEXButton(int num, bool flag)
    {
        buttons[SkillManager.MAX_BATTLE_SKILLS_NUM + num].interactable = flag;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        Sprite full_moon = Resources.Load<Sprite>("Image/Others/Moons/満月");
        Sprite half_moon = Resources.Load<Sprite>("Image/Others/Moons/半月");
        Sprite new_moon = Resources.Load<Sprite>("Image/Others/Moons/新月");

        moons.Add(full_moon);
        moons.Add(half_moon);
        moons.Add(new_moon);

        Image[] images = this.GetComponentsInChildren<Image>();
        int count = 0;
        int id = '0';
        string name = "CoolTurn";
        string tmp_name = "";
        foreach (Image image in images)
        {
            for (count = 0, id = '0'; count < SkillManager.MAX_BATTLE_SKILLS_NUM; count++)
            {
                tmp_name = name + (char)id;
                if (image.gameObject.name.Equals(tmp_name))
                {
                    image_List[id - '0'] = image;
                    break;
                }
                id++;
            }
        }
    }

    public void UpdateCoolTurn(Battle battle)
    {
        int count = 0;
        foreach (int cool_turn in battle.Ally_player.Skill_CoolTurn)
        {
            Debug.Log("Skill" + count + " : " + cool_turn);
            if (cool_turn == 0)
            {
                buttons[count].interactable = true;
            }
            else
            {
                buttons[count].interactable = false;
            }
            ChangeMoonImage(cool_turn, image_List[count]);
            count++;
        }
    }

    void ChangeMoonImage(int cool_turn, Image image)
    {
        image.sprite = moons[cool_turn];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
