using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{

    public ItemType itemType;
    public int Num=0;

    public enum ItemType {
        Bomb_L,
        Bomb_S,
        Bomb_Timer,
        Teddy,
        TransferGate,
        Lego,
        CardBoard,
        Bottle,
        Carton,
        Pillow,
    }

    public Sprite GetSprite() {
        switch (itemType)
        {
            default:
            case ItemType.Bomb_L:return ItemAssets.Instance.Bomb_LSprite;
            case ItemType.Bomb_S: return ItemAssets.Instance.Bomb_SSprite;
            case ItemType.Bomb_Timer: return ItemAssets.Instance.Bomb_TimerSprite;
            case ItemType.Teddy:
                    {
                        Debug.Log("found"); return ItemAssets.Instance.TeddySprite;
                    }
            case ItemType.TransferGate: return ItemAssets.Instance.TransferGateSprite;
            case ItemType.Lego: return ItemAssets.Instance.LegoSprite;
            case ItemType.CardBoard:
                    {
                        Debug.Log("found"); return ItemAssets.Instance.CardBoardSprite;
                    }
            case ItemType.Bottle: return ItemAssets.Instance.BottleSprite;
            case ItemType.Carton: return ItemAssets.Instance.CartonSprite;
            case ItemType.Pillow:
                    {
                        Debug.Log("found"); return ItemAssets.Instance.PillowSprite;
                    }

        }
    }

}
