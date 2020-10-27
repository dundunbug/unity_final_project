using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemplaced : MonoBehaviour
{
    // Start is called before the first frame update
    public static Itemplaced MakeItem(Vector3 position, Item item) {
        Transform transform = Instantiate(ItemAssets.Instance.pfitplaced, position, Quaternion.identity);

        Itemplaced itplaced= transform.GetComponent<Itemplaced>();
        itplaced.SetItem(item);
        Debug.Log("item made");
        return itplaced;
    }

    //itemAssets ptitplaced;
    private Item item;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.GetSprite();
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }
}
