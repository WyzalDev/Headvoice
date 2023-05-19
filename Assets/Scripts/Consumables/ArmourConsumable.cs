using UnityEngine;

public class ArmourConsumable : Consumable
{
    Character character;

    [SerializeField]
    private int maxArmour;

    void Start() {
        character = GameObject.Find("Character").GetComponent<Character>();
    }
    
    public override void consume()
    {
        int randomArmour = Random.Range(1, maxArmour);
        character.armourRestore(randomArmour);
    }
}
