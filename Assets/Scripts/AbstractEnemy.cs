using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractEnemy : MonoBehaviour
{
    public GameObject[] point = new GameObject[9];
    public int action, rand = 0;
    public float speed = 1f;
    protected Character character;
    public float fieldOdView = 2f;
    public float privateDistance = 0.5f;
    protected float timer;
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

        
    }

    protected void HitCharacter()
    {

    }

    private void onCollisionEnter2D(Collision2D collision)
    {
        if (action == 0)
        {
            rand = Random.Range(0, 9);
        }
    }
}

