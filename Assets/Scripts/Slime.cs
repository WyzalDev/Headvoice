using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : AbstractEnemy
{
   
    public void Update()
    {
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(character.transform.position.x, character.transform.position.y));
        gameObject.GetComponent<SpriteRenderer>().flipX = transform.position.x - character.transform.position.x < 0;
        if (distance <= fieldOdView)
        {
            action = 1;

        }
        else action = 0;

        if (action == 0)
        {
            if (point[rand].transform.parent != null)
            {
                for (int i = 0; i < point.Length; i++)
                {
                    point[i].transform.parent = null;
                }
            }
            if (transform.position != point[rand].transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, point[rand].transform.position, speed * Time.deltaTime);
            }
            else
            {
                rand = Random.Range(0, 9);
            }

        }
        if (action == 1)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, speed * Time.deltaTime);
            
          

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.name == "Character")
        {
           if (timer >= 0)
        {
            timer -= Time.deltaTime;

        }
        else
        {
            timer = 2;
            //animator.SetTrigger("attack");

            collision.gameObject.GetComponent<Character>().takeDamage();

        } 
        }


    }

    

}
