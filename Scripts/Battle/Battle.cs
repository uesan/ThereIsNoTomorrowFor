using UniRx.Async;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    public SceneController sceneController;
    private Player ally_player;
    private Player enemy_player;

    //Button[] buttons = new Button[8];
    Text ally_monster_name;
    Text enemy_monster_name;
    Text ally_hp_text;
    Text battle_text;
    Slider ally_monster_hp;
    Slider enemy_monster_hp;
    GameObject canvasGroup;
    SkillButton skillbuttons;
    SpriteRenderer ally_monster_spriteRenderer;
    SpriteRenderer enemy_monster_spriteRenderer;
    bool init_GetComp = true;

    public Player Ally_player { get => ally_player; set => ally_player = value; }
    public Player Enemy_player { get => enemy_player; set => enemy_player = value; }
    public bool Init_GetComp { get => init_GetComp; set => init_GetComp = value; }

    public void OnBattleInitalise(Player ally, Player enemy)
    {
        if (init_GetComp != true)
            return;
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        SetPlayers(ally, enemy);
        InitMonster();
        InitFlags(ally, enemy);
        InitButtons();
        InitText();
        InitHpSlider();
        InitCanvasGroup();
        this.init_GetComp = false;

        //とりあえず今はスキルセットにスキルを打ち込む関数をここで入れる
        //スキルセットをボタンにつける
        //モンスターの初期化
    }

    private void SetPlayers(Player ally, Player enemy)
    {
        this.Ally_player = ally;
        this.Enemy_player = enemy;
        ally.InitBattle();
        enemy.InitBattle();

        /*
         * !!!解決済み!!!
         * 戦闘モンスターを全部ディープコピーして、戦闘前の状態に戻るようにしたかったけど、
         * ディープコピーがUnityのクラスを継承したものだとできないっぽい
         * 解決策としては単純にプレハブかなんかでモンスター自体を複製するとかかな？
         * 
         * 多分理想は、
         * ScriptableObjectを継承したクラス -> Monobehaviourを継承したクラス
         * でモンスターを生成すればメモリを食わずに済むっぽい
         * 参考 https://ekulabo.com/about-scriptable-object
         */
         /*
        for (int i = 0; i < MonsterManager.MAX_BATTLE_MONSTERS_NUM; i++)
        {
            if (ally_player.Battle_monsters.Count > i)
            {
                Monster monster = new Monster(Ally_player.Battle_monsters[i]);
                ally.Monsters_on_battle.Add(monster);
                Debug.Log(Ally_player.Battle_monsters[i]);
            }
            //Monster allymonster = new Monster(Ally_player.Battle_monsters[i]);
            //Debug.Log(allymonster);
            //this.Ally_player.Monsters_on_battle.Add(allymonster);
            //Monster enemymonster = new Monster(enemy_player.Battle_monsters[i]);
            //Debug.Log(enemymonster);
            //this.Enemy_player.Monsters_on_battle.Add(enemymonster);
            if (enemy_player.Battle_monsters.Count > i)
            {
                enemy.Monsters_on_battle.Add(new Monster(Enemy_player.Battle_monsters[i]));
            }
        }
        ally.Buff_manager = new BuffManager(ally);
        ally.Buff_manager.Init();
        //enemy.Buff_manager.Init();
        */

        //ally.Monsters_on_battle = Ally_player.Battle_monsters;
        //enemy.Monsters_on_battle = Enemy_player.Battle_monsters;
    }

    private void InitFlags(Player ally, Player enemy)
    {
        ally.Win_flag = false;
        enemy.Win_flag = false;
        ally.Top_monster_death = false;
        enemy.Top_monster_death = false;
    }

    private void InitButtons()
    {
        if (init_GetComp)
        {
            this.skillbuttons = GameObject.Find("SkillButtons").GetComponent<SkillButton>();
            skillbuttons.Init(this);
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
            Debug.Log(buttons);
            Debug.Log(this.buttons);
            */
        }
    }

    private void InitCanvasGroup()
    {
        if (init_GetComp)
        {
            this.canvasGroup = GameObject.Find("SkillButtons");
        }
    }

    private void InitText()
    {
        if (init_GetComp)
        {
            ally_monster_name = GameObject.Find("AllyMonsterName").GetComponent<Text>();
            enemy_monster_name = GameObject.Find("EnemyMonsterName").GetComponent<Text>();
            ally_hp_text = GameObject.Find("AllyHP").GetComponent<Text>();
            battle_text = GameObject.Find("BattleText").GetComponent<Text>();
        }

        ally_monster_name.text = Ally_player.Top_monster.Monster_data.monster_name;
        Debug.Log("テキスト : " + ally_monster_name.text);
        enemy_monster_name.text = Enemy_player.Top_monster.Monster_data.monster_name;
        Debug.Log("InitText : " + Ally_player.Top_monster.Monster_data.monster_name);
    }

    private void InitHpSlider()
    {
        if (init_GetComp)
        {
            ally_monster_hp = GameObject.Find("AllyHpBar").GetComponent<Slider>();
            enemy_monster_hp = GameObject.Find("EnemyHpBar").GetComponent<Slider>();
        }

        ally_monster_hp.maxValue = Ally_player.Top_monster.Monster_data.max_hp;
        ally_monster_hp.value = Ally_player.Top_monster.Hp;
        enemy_monster_hp.maxValue = Enemy_player.Top_monster.Monster_data.max_hp;
        enemy_monster_hp.value = Enemy_player.Top_monster.Hp;
        ally_hp_text.text = Ally_player.Top_monster.Hp.ToString() + "/" + Ally_player.Top_monster.Monster_data.max_hp.ToString();

    }

    private void InitMonster()
    {
        if (init_GetComp)
        {
            ally_monster_spriteRenderer = GameObject.Find("AllyMonster").GetComponent<SpriteRenderer>();
            enemy_monster_spriteRenderer = GameObject.Find("EnemyMonster").GetComponent<SpriteRenderer>();
            ally_player.Top_monster = ally_player.Monsters_on_battle[0];
            enemy_player.Top_monster = enemy_player.Monsters_on_battle[0];
        }
        ally_monster_spriteRenderer.sprite = ally_player.Top_monster.Monster_data.sprite;
        enemy_monster_spriteRenderer.sprite = enemy_player.Top_monster.Monster_data.sprite;
        enemy_player.Top_monster = enemy_player.Top_monster;
    }

    public void ExchangeMonster(Player player, int num)
    {
        ExchangeMonster(player, ally_player.Monsters_on_battle[num]);
    }

    public void ExchangeMonster(Player player, Monster monster)
    {
        for (int i = 0; i < MonsterManager.MAX_BATTLE_MONSTERS_NUM && i < player.Monsters_on_battle.Count; i++)
        {
            if (player.Monsters_on_battle[i].Monster_data.monster_name == monster.Monster_data.monster_name)
            {
                player.Top_monster = monster;
                InitMonster();
                InitHpSlider();
                InitText();
                if(player.Is_ally)
                    skillbuttons.UpdateExchangeButton(this);
                return;
            }
        }
        Debug.Log("Top_monsterがデッキに存在しません");
    }

    public void UseSkill(SkillData skill, Player from_player, Player to_player)
    {
        Debug.Log(skill.skill_name + " 使用！");
        int count = 0;
        foreach (SkillData tmp_skill in from_player.Battle_skills)
        {
            if (tmp_skill.skill_name.Equals(skill.skill_name))
            {
                from_player.IntoCoolTurn(count);
            }
            count++;
        }
        ChangeBattleText(from_player.Top_monster.Monster_data.monster_name + "の" + from_player.Next_use_skill.skill_name + "!!");
        switch (from_player.Next_use_skill.skill_type)
        {
            case SkillType.attack:
                int damage = DamageCalculate(skill, from_player, to_player);
                to_player.Top_monster.Hp -= damage;
                Debug.Log(to_player.Player_name + "のHP" + to_player.Top_monster.Hp);
                if (to_player.Top_monster.Hp < 0)
                    to_player.Top_monster.Hp = 0;
                break;
            case SkillType.support:
                skill.AddBuff(from_player, to_player);
                break;

            case SkillType.exchange:
                break;
            default:
                break;
        }
    }

    private void ReflectHp(bool is_ally, Monster monster)
    {
        if (is_ally)
        {
            ally_monster_hp.value = monster.Hp;
            ally_hp_text.text = Ally_player.Top_monster.Hp.ToString() + "/" + Ally_player.Top_monster.Monster_data.max_hp.ToString();

        }
        else
            enemy_monster_hp.value = monster.Hp;

    }

    public void ChangeBattleText(string text)
    {
        battle_text.text = text;
    }

    public void DisplaySkillExp(int num)
    {
        ChangeBattleText(Ally_player.Battle_skills[num].Explain());
    }

    public void DisplayExchangeExp(int num)
    {
        if(Ally_player.Monsters_on_battle.Count > num)
            ChangeBattleText(Ally_player.Monsters_on_battle[num].Explain());
    }

    private int DamageCalculate(SkillData skill, Player from_player, Player to_player)
    {
        Attribute attribute = Attribute.GetInstance();
        int comp = attribute.GetComp(skill.attr, to_player.Top_monster.Monster_data.attr);
        int from_attack = from_player.Top_monster.Monster_data.attack + from_player.Buff_manager.Attack();
        int to_defense = to_player.Top_monster.Monster_data.defense + to_player.Buff_manager.Defense();
        return skill.damage * from_attack / (100 + to_defense) * comp / 100;
    }   

    /*
     * 戦闘中のモンスターの生存確認をするメソッド
     * 返り値 true : 生存
     *       false: 戦闘不能
     */
    private bool CheckSurvival(Player player)
    {
        if (player.Top_monster.Hp <= 0)
        {
            return false;
        }

        return true;
    }

    private bool CheckToLose(Player player)
    {
        for (int i = 0; i < MonsterManager.MAX_BATTLE_MONSTERS_NUM; i++)
        {
            if (player.Monsters_on_battle.Count == i)
            {
                break;
            }
            if (player.Monsters_on_battle[i].Hp > 0)
            {
                return false;
            }
        }
        return true;
    }

    /*
     * 先頭のモンスターが倒されたかを確認し、それによって処理をおこなうメソッド
     * このメソッドではクラス変数のwin_flagを操作する。
     * 返り値 true : 戦闘終了
     *       false: 戦闘継続
     */
    private bool KOCheck(Player checked_player, Player opp_player)
    {
        if (CheckSurvival(checked_player) == false)
        {
            //checked_player.Monsters_on_battle.RemoveAt(0);
            string sentence = checked_player.Player_name + "の、" + checked_player.Top_monster.Monster_data.monster_name + "はたおれた！";
            checked_player.Top_monster_death = true;
            if (CheckToLose(checked_player))
            {
                opp_player.Win_flag = true;
                EndBattle();
                return true;
            }
        }
        return false;
    }

    /*
     * ターンごとの先攻後攻を決めるためのメソッド
     * 返り値 true : 一つ目の引数のモンスターが先攻
     *       false: 二つ目の引数のモンスターが先攻
     */
    private bool FasterThan(Player target_player, Player compared_player)
    {
        /* Todo
         * 素早さ以外で先攻後攻が決まる特殊技の場合の処理をする必要がある
         */
        if( target_player.Next_use_skill.priority > compared_player.Next_use_skill.priority)
            return true;
        if (target_player.Next_use_skill.priority < compared_player.Next_use_skill.priority)
            return false;
        int target_speed = target_player.Top_monster.Monster_data.speed + target_player.Buff_manager.Speed();
        int compared_speed = compared_player.Top_monster.Monster_data.speed + compared_player.Buff_manager.Speed();
        if ( target_speed > compared_speed )
            return true;
        if ( target_speed < compared_speed )
            return false;
        int rand = Random.Range(0, 2);
        if (rand == 0)
            return true;
        return false;

    }

    
    public void ProgressTurn(SkillData skill)
    {
        ally_player.SetNextSkill(skill);
        ProgressTurnImp();
    }

    public void ProgressTurn(int num)
    {
        Ally_player.SetNextSkill(num);
        ProgressTurnImp();
    }
    private void ProgressTurnImp()
    {
        canvasGroup.SetActive(false);
        Ally_player.PastTurnStart();
        Enemy_player.PastTurnStart();
        //Todo ここで相手のスキルも設定、取得するのがよさそう
        StartTurn();
        Debug.Log("ターン終了");
        return;
    }

    private void EndTurn()
    {
        skillbuttons.UpdateButtons(this);
        Ally_player.PastTurnEnd(this);
        Enemy_player.PastTurnEnd(this);
        canvasGroup.SetActive(true);
    }

    async private void EndBattle()
    {
        FeedbackMonsters();
        ally_player.Monsters_on_battle.Clear();
        enemy_player.Monsters_on_battle.Clear();
        string name;
        if (Ally_player.Win_flag)
        {
            name = Ally_player.Player_name;
        }
        else if (Enemy_player.Win_flag)
        {
            name = Enemy_player.Player_name;
        }
        else
        {
            Debug.Log("引き分け");
            ChangeBattleText("引き分け");
            await UniTask.Delay(1000);
            sceneController.ChangeSceneToTitle();
            return;
        }

        Debug.Log(name + "の勝利");
        ChangeBattleText(name + "の勝利");
        await UniTask.Delay(1000);
        sceneController.ChangeSceneToTitle();
    }

    /* Todo
     * 経験値の取得やHPの減少などの変化を反映するメソッド（予定）
     */
    private void FeedbackMonsters()
    {
        return;
    }

    /*
     * １ターンの処理を行うメソッド
     * スキル選択後、このスキルを呼び出す
     * このメソッドの処理が終わりきるまでに戦闘が終了する可能性がある
     * 返り値 true : 戦闘終了
     *       false: 戦闘継続
     */
    private bool StartTurn()
    {
        Debug.Log("StartTurn開始");
        if (FasterThan(Ally_player, Enemy_player))
        {
            BattlePhase(Ally_player, Enemy_player);
            if (CheckEndBattle())
                return true;

        }
        else
        {
            BattlePhase(Enemy_player, Ally_player);
            if (CheckEndBattle())
                return true;
        }
        Debug.Log("StartTurn終了");
        return false;
    }

    private bool CheckEndBattle()
    {
        if (Ally_player.Win_flag || Enemy_player.Win_flag)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// プレイヤーがカードを選んだ後、バトルフェーズが１ターン終了するまでを処理するメソッド
    /// async/await を使用しており、WebGLへのビルドではエラーが起きるらしいので、
    /// UniRxを使っている
    /// 参考 https://yumineko.com/unitask-beginner/
    /// </summary>
    /// <param name="first_player">先攻プレイヤー</param>
    /// <param name="second_player">後攻プレイヤー</param>
    async void BattlePhase(Player first_player, Player second_player)
    {
        first_player.Top_monster_death = false;
        second_player.Top_monster_death = false;

        //先攻の攻撃処理
        UseSkill(first_player.Next_use_skill, first_player, second_player);
        Debug.Log(second_player.Player_name + "のHP" + second_player.Top_monster.Hp);
        ReflectHp(first_player.Is_ally, first_player.Top_monster);
        ReflectHp(second_player.Is_ally, second_player.Top_monster);
        await UniTask.Delay(1000);
        ChangeBattleText(DisplayIsEffective(SkillIsEffective(first_player, second_player)));
        await UniTask.Delay(1000);
        if (KOCheck(first_player, second_player))
            return;
        if (KOCheck(second_player, first_player))
            return;

        //後攻の攻撃処理
        if (second_player.Top_monster_death == false)
        {
            /* Todo
             * 相手が自爆していた場合攻撃スキルは発動せず、補助スキルは発動するようにしたい
             */
            UseSkill(second_player.Next_use_skill, second_player, first_player);
            ReflectHp(first_player.Is_ally, first_player.Top_monster);
            ReflectHp(second_player.Is_ally, second_player.Top_monster);
            await UniTask.Delay(1000);
            ChangeBattleText(DisplayIsEffective(SkillIsEffective(second_player, first_player)));
            await UniTask.Delay(1000);
        }

        if (KOCheck(second_player, first_player))
            return;
        if (KOCheck(first_player, second_player))
            return;

        EndTurn();
        //return false;
    }

    private Comp SkillIsEffective(Player attack_player, Player defence_player)
    {
        Attribute attribute = Attribute.GetInstance();
        int comp = attribute.GetComp(attack_player.Next_use_skill.attr, defence_player.Top_monster.Monster_data.attr);
        switch (comp)
        {
            case 0:
                return Comp.noDamage;
            case 50:
                return Comp.notGood;
            case 80:
                return Comp.toHoly;
            case 100:
                return Comp.normal;
            case 120:
                return Comp.toDark;
            case 200:
                return Comp.effective;
            default:
                break;
        }
        return Comp.normal;
    }

    private string DisplayIsEffective(Comp comp)
    {
        string sentence = "";

        switch (comp)
        {
            case Comp.noDamage:
                sentence = "効果はないみたいだ・・・";
                break;
            case Comp.notGood:
                sentence = "こうかはいまひとつのようだ";
                break;
            case Comp.toHoly:
                sentence = "光がダメージを軽減した！";
                break;
            case Comp.normal:
                return sentence;
            case Comp.toDark:
                sentence = "闇がダメージを増幅させた！";
                break;
            case Comp.effective:
                sentence = "こうかはばつぐんだ！";
                break;
            default:
                break;

        }
        return sentence;
    }

    /* 時間を止めるために作ったけどスレッドが違うっぽくて止まってるけど他のメソッドは止まってなさそう
    IEnumerator sleep()
    {

        Debug.Log("開始");
        yield return new WaitForSeconds(5);  //10秒待つ
        Debug.Log("5秒経ちました");

    }
    */

    // Start is called before the first frame update
    void Start()
    {
        OnBattleInitalise(ally_player, enemy_player);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
