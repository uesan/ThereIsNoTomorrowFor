using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager
{
    private MonsterData data_sum = new MonsterData();
    private List<Buff> buffs = new List<Buff>();
    private Player player = new Player(false, "unknown");

    public BuffManager(Player player)
    {
        this.Player = player;
    }

    public MonsterData Data_sum { get => data_sum; set => data_sum = value; }
    internal List<Buff> Buffs { get => buffs; set => buffs = value; }
    public Player Player { get => player; set => player = value; }

    public void Init()
    {
        buffs = new List<Buff>();
    }

    public int Attack()
    {
        SumBuffs();
        return Data_sum.attack;
    }
    public int Defense()
    {
        SumBuffs();
        return Data_sum.defense;
    }
    public int Speed()
    {
        SumBuffs();
        return Data_sum.speed;
    }

    public void AddBuff(Buff buff)
    {
        buff.Now_rest_turn = buff.max_rest_turn;
        buffs.Add(buff);
    }

    public void PastTurn()
    {
        int count = 0;
        List<int> array = new List<int>();
        foreach (Buff buff in buffs)
        {
            buff.Now_rest_turn--;
            Debug.Log("このバフの残りターン：" + buff.Now_rest_turn);
            if (buff.Now_rest_turn <= 0)
                array.Add(count);
            count++;
        }
        for (int i = 0; i < array.Count; i++)
            buffs.RemoveAt(array[i]);
    }

    public void ExchangeEffect()
    {
        int count = 0;
        List<int> array = new List<int>();
        foreach (Buff buff in buffs)
        {
            if (buff.range == EffectRange.Top)
                array.Add(count);
            count++;
        }
        for(int i = 0; i < array.Count; i++)
            buffs.RemoveAt(array[i]);
    }

    private void SumBuffs()
    {
        Data_sum = new MonsterData();
        foreach(Buff buff in Buffs)
        {
            Calculate(buff);
        }
    }

    private void Calculate(Buff buff)
    {
        if (buff.is_const)
        {
            Data_sum.attack += buff.attack;
            Data_sum.defense += buff.defense;
            Data_sum.speed += buff.speed;
        }
        else
        {
            Data_sum.attack += Player.Top_monster.Monster_data.attack * buff.attack / 100;
            Data_sum.defense += Player.Top_monster.Monster_data.defense * buff.defense / 100;
            Data_sum.speed += Player.Top_monster.Monster_data.speed * buff.speed / 100;
        }
    }
}