using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prac_Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer cardImg;
    [SerializeField] Sprite Back;
    [SerializeField] Sprite Front;

    public Item item;
    bool isFront;

    public void Setup(Item item, bool isFront)
    {
        this.item = item;
        this.isFront = isFront;

        if(this.isFront){
            cardImg.sprite=this.item.sprite;
        }
        else{
            card.sprite=Back;
        }
    }
}
