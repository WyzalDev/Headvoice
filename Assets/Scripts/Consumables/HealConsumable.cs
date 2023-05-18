using UnityEngine;

public class HealConsumable : Consumable
{
    Character character;

    [SerializeField]
    private int maxHeal = 2;

    void Start() {
        character = GameObject.Find("Character").GetComponent<Character>();
    }

    public override void consume() {
        int randomHeal = Random.Range(1,maxHeal);
        character.Heal(randomHeal);
    }
}
