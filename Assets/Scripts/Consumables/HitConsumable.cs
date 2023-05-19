using UnityEngine;

public class HitConsumable : Consumable
{
    Character character;
    void Start() {
        character = GameObject.Find("Character").GetComponent<Character>();
    }
    
    public override void consume()
    {
        character.takeDamage();
    }
}
