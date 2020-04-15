using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMonster : MonoBehaviour
{
    private SpriteRenderer monster_sprite;

    void ChangeMonster(Sprite sprite)
    {
        this.monster_sprite.sprite = sprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        monster_sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
