using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attr
{
    fire, wild, water, electro, earth, dark, holy, neutral, count
}

public enum Comp
{
    noDamage, notGood, toHoly, normal, toDark, effective,
}

public class Attribute : ScriptableObject
{
    private static Attribute singleton = new Attribute();
    private Attribute()
    {

    }

    public static Attribute GetInstance()
    {
        return singleton;
    }

    /// <summary>
    /// %で相性ダメージ係数を表している配列。使用側で100で割ることが想定されている。
    /// 理由は全てintで計算した方が誤差が出ないし、勝手に整数に丸めてくれるので楽だから。
    /// 基本的にAttrを２つ受け取って、１次元目に攻撃側、２次元目に防御側がくることを考えている
    /// </summary>
    private int[,] compTable = new int[(int)Attr.count, (int)Attr.count] {
        {100, 200,  50, 100,  50, 120,  80, 100},
        { 50, 100, 100, 100, 200, 120,  80, 100},
        {200,  50, 100, 100, 200, 120,  80, 100},
        {100,  50, 200, 100,   0, 120,  80, 100},
        {200, 100,  50, 200, 100, 120,  80, 100},
        {100, 100, 100, 100, 100, 100, 200, 100},
        {100, 100, 100, 100, 100, 200, 100, 100},
        {100, 100, 100, 100, 100, 100, 100, 100},
    };

    public int[,] CompTable { get => compTable; set => compTable = value; }

    public int GetComp(Attr attackAttr, Attr defenceAttr)
    {
        return CompTable[(int)attackAttr, (int)defenceAttr];
    }
}
