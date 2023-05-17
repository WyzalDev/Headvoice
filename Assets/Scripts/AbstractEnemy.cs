using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractEnemy : MonoBehaviour
{
    public GameObject[] point = new GameObject[9];
    public int action, rand = 0;
    public float speed = 1f;
    private Character character;
    public int fieldOdView = 2;
    // Start is called before the first frame update
    void Start()
    {
        if(character== null)
        {
            character = GameObject.Find("Character").GetComponent<Character>();
        }
    }

    void Update()
    {
        
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(character.transform.position.x, character.transform.position.y)) <= fieldOdView)
        {
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, speed * Time.deltaTime);

        } else {
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

        } }

        
    }

    private void onCollisionEnter2D(Collision2D collision)
    {
        if (action == 0)
        {
            rand = Random.Range(0, 9);
        }
    }
}

