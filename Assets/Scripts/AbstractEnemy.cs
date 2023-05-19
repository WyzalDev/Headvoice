using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractEnemy : MonoBehaviour
{
    public int health;
    public GameObject[] point = new GameObject[9];
    public int action, rand = 0;
    public float speed = 1f;
    protected Character character;
    public float fieldOdView = 2f;
    public float privateDistance = 0.5f;
    protected float timer;
    public Animator animator;
    //protected Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        if (character== null)
        {
            character = GameObject.Find("Character").GetComponent<Character>();
        }
    }

    void Update()
    {

        
    }

    protected void HitCharacter()
    {

    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0){
            onKill();
        }
    } 

    public virtual void onKill()
    {
        Destroy(gameObject);
    }

    private void onCollisionEnter2D(Collision2D collision)
    {
        if (action == 0)
        {
            rand = Random.Range(0, 9);
        }
    }
}

