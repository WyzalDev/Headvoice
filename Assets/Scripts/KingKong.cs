using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class KingKong : AbstractEnemy
{
    public int countOfBoolets=2;
    public GameObject Boolet;
    public float timerForBust=0.5f;
    public float reloadTimer = 7f;
    public void Update()
    {
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(character.transform.position.x, character.transform.position.y));

        
        if (distance > privateDistance)
            {
                HitCharacter();
                
              }
            else
            {
            BustToCharacter();
                  }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Character")
        {
            collision.gameObject.GetComponent<Character>().takeDamage();
            
        }
    }

    private void BustToCharacter()
    {
        timer -= Time.deltaTime;
        if (timer >= reloadTimer-timerForBust)
        {
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, speed * 10 * Time.deltaTime);
        }
        if(timer <= 0)
        {
            timer = reloadTimer;
        }
    }
    protected void HitCharacter()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, speed * Time.deltaTime);
        }
        else
        {
            timer = 2;
            Instantiate(Boolet, transform.position, Quaternion.identity); 
           }

    }
}
