using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
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

    private void FixedUpdate() {
        List<Collider2D> objects = scanEnemies();
        string log = "";
        foreach (Collider2D item in objects)
        {
            log+= item.gameObject.name + "      ";
        }
        Debug.Log(log);
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

    private List<Collider2D> scanEnemies() {
        List<Collider2D> colliders =new List<Collider2D>(Physics2D.OverlapCircleAll(transform.position, scanRange));
        colliders.RemoveAll(item => !isHaveEnemyComponent(item));
        return colliders;
    }

    private bool isHaveEnemyComponent(Collider2D collider) {
        return collider.gameObject.GetComponent<AbstractEnemy>() != null;
    }


}
