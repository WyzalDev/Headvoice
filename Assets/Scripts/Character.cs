using UnityEngine;

public sealed class Character : MonoBehaviour
{

    [Range(0,10)]
    private int health = 10;
    [Range(0,5)]
    private int armour = 5;
    private int baseDamage = 10;

    //In that range character start tracking enemies
    [Header("Enemy Tracking")]
    [SerializeField]
    private float scanRange = 5;

    [SerializeField]
    private Weapon weapon;

    private static Character character;

    private Character() {}
 
    public static Character getInstance() {
        if(character==null) {
            character = new Character();
        }
        return character;
    }

    private void FixedUpdate() {

    }

    public void takeDamage() {
        if (armour>0) {
            armour--;
        } else if(health>0) {
            health--; 
        } else {
            //TODO ENDGAME
        }
    }

    public void takeNewWeapon(Weapon newWeapon) {
        //TODO throw old weapon on ground
        weapon = newWeapon;
    }

    // private List<Collider2D> scanEnemies() {
    //     return Physics2D.CircleCastAll().RemoveAll((Collider2D x) => x is Enemy);
    // }

 


}
