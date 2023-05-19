using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boolet : AbstractProjectTile
{
    private bool flag=true;
    // Start is called before the first frame update
    void Start()
    {
        initiate();
        name = "Boolet";
        Vector2 dirrection = (Vector2.up *  (character.transform.position.y - transform.position.y) + Vector2.right * ((character.transform.position.x - transform.position.x)));
        rb.AddForce(dirrection * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if( timer <= 0f)
        {
            DestroyProjectTile();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Character" && flag)
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Character>().takeDamage();
            flag = false;
        }
        DestroyProjectTile();

    }

    protected void DestroyProjectTile()
    {
        
    }
}
