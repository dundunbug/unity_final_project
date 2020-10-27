using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
   public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this; 
     //   pfitplaced = transform.Find("pfitplaced");
    }


    public Transform pfitplaced;
    
    public Sprite Bomb_LSprite;
    public Sprite Bomb_SSprite;
    public Sprite Bomb_TimerSprite;
    public Sprite TeddySprite;
    public Sprite TransferGateSprite;
    public Sprite LegoSprite;
    public Sprite CardBoardSprite;
    public Sprite BottleSprite;
    public Sprite CartonSprite;
    public Sprite PillowSprite;
}
