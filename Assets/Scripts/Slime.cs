using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Slime : AbstractEnemy
{
    public GameObject Boolet;
    public void Update()
    {
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(character.transform.position.x, character.transform.position.y));


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
            if (distance > privateDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, character.transform.position, speed * Time.deltaTime);
            }
            else
            { 
                HitCharacter();
                transform.position = Vector3.MoveTowards(transform.position, character.transform.position*-10 , speed * Time.deltaTime);
              
            }
          
        }
        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Character")
       {
            Vector2 dirrection = (Vector2.up * (character.transform.position.y - transform.position.y) + Vector2.right * ((character.transform.position.x - transform.position.x)));
            //rb.AddForce(dirrection * -1 * speed*2 * Time.deltaTime, ForceMode2D.Impulse);
            transform.position = Vector3.MoveTowards(transform.position, dirrection*-2, speed * Time.deltaTime);
        }


    }

    protected void HitCharacter()
    {
        if (timer >= 0) {
            timer -= Time.deltaTime;
           
                }
        else
        {
            timer = 2;
            Instantiate(Boolet, transform.position, Quaternion.identity);

        }
        
    }

    

   }
