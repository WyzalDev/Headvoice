using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractProjectTile : MonoBehaviour
{
    protected Character character;
    protected Rigidbody2D rb;
    protected Vector2 dirrection;
    public float speed, timer = 1f;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //call before start
    protected void initiate()
    {  
        rb = GetComponent<Rigidbody2D>();
        if (character == null)
        {
            character = GameObject.Find("Character").GetComponent<Character>();
        }
    }


}
