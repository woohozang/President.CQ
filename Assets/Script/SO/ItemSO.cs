using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public Sprite sprite;
}

//파일 이름이 ItemSo 이고 Scriptable Object/ItemSO 이 폴더에서 생성 가능 
[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]


public class ItemSO : ScriptableObject
{
    public Item[] items;
}
