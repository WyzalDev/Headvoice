using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterBoolet : MonoBehaviour
{
    protected Character character;
    protected Rigidbody2D rb;
    protected Vector2 dirrection;
    public float speed, timer = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (character == null)
        {
            character = GameObject.Find("Character").GetComponent<Character>();
        }
        if (character.objects.Count > 0) { 
            GameObject enemy = character.objects[0].gameObject; 
        name = "CharacterBoolet";
        Vector2 dirrection = (Vector2.up * (enemy.transform.position.y - transform.position.y) + Vector2.right * ((enemy.transform.position.x - transform.position.x)));
        rb.AddForce(dirrection * speed * Time.deltaTime, ForceMode2D.Impulse);}
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            DestroyProjectTile();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<AbstractEnemy>().takeDamage(character.baseDamage);
        }
        DestroyProjectTile();

    }

    protected void DestroyProjectTile()
    {

    }
}
