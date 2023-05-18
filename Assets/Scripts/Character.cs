using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine.UI;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject Boolet;
    [Range(0,10)]
    private int health = 10;
    [Range(0,5)]
    private int armour = 5;
    public int baseDamage = 10;
    protected float timer;
    //In that range character start tracking enemies
    [Header("Tracking")]
    [SerializeField]
    private float scanRange = 5f;

    [SerializeField]
    private float scanChestRange = 1f;
    public bool isSword=true;
    public List<Collider2D> objects;
    [SerializeField]
    private Weapon weapon;
    private GameObject dialoguebox;
    private GameObject dialogueWindow;
    private bool isCoroutineStarted = false;

    public static List<string> dialogueQueue;

    public Animator animator;
    void Start() {
        dialogueQueue = new List<string>{"Something"};
        dialoguebox = GameObject.FindGameObjectWithTag("DialogueBox");
        dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow");
    }

    public List<GameObject> getDialogueStuff() {
        return new List<GameObject>{dialoguebox, dialogueWindow};
    }

    void Update() {

        if(dialogueQueue.Count > 0 && !isCoroutineStarted) {
            dialoguebox.SetActive(true);
            isCoroutineStarted = true;
            dialogueWindow.GetComponent<TMP_Text>().text = dialogueQueue[0];
            dialogueQueue.RemoveAt(0);
            StartCoroutine(DialogueBoxAutoTimeDisable());
        }
    }

    private void FixedUpdate() {
        timer -= Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.R))
        {
            isSword = !isSword;
        }
        objects = scanEnemies();
        if (!isSword && objects.Count>0) {
            ShootEnemy();

        }
        string log = "";
        foreach (Collider2D item in objects)
        {
            log+= item.gameObject.name + "      ";
        }
        Debug.Log(log);
        //animator.SetTrigger("isRunning");
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

    private void OnTriggerStay2D(Collider2D collision)
    {
       if (collision.tag == "Enemy")
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;

            }
            else
            {
                timer = 2;
                animator.SetTrigger("attack");

                collision.gameObject.GetComponent<AbstractEnemy>().takeDamage(baseDamage);

            }
            
        }
        
    }

    private void ShootEnemy()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;

        }
        else
        {
            timer = 2;
            Instantiate(Boolet, transform.position, Quaternion.identity);

        }

    }

    IEnumerator DialogueBoxAutoTimeDisable(){
        yield return new WaitForSeconds(2f);
        dialoguebox.SetActive(false);
        isCoroutineStarted = false;
    }

}
