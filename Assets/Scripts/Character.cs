using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine.UI;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject Boolet;

    [SerializeField]
    private GameObject sceneEnder;
    private int health = 10;
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
    [SerializeField]
    private int maxDelay;

    [SerializeField]
    private int minDelay;
    
    [SerializeField]
    private int randomConsumableDelay;

    [SerializeField]
    private int maxForce;

    [SerializeField]
    private int minForce;
    private bool isDelayStarted = false;

    private bool isConsumableDelayStarted = false;
    private GameObject dialoguebox;
    private GameObject dialogueWindow;
    private bool isCoroutineStarted = false;

    public static List<string> dialogueQueue;
    public Animator animator;

    private Rigidbody2D rigidbody;
    
    [SerializeField]
    private int mood = 50;

    public int getMood() {
        return mood;
    }

    public void decreaseMood(int value) {
        if(mood <= value) {
            mood = 0;
        } else {
            mood-=value;
        }
    }

    public void increaseMood(int value) {
        if(mood-100 + value >= 0) {
            mood = 100;
        } else {
            mood+=value;
        }
    }

    public int getHealth(){
        return health;
    }

    public int getArmour(){
        return armour;
    }

    public void Heal(int healValue) {
        health += healValue;
        if (health>10) health = 10;
    }

    public void armourRestore(int armourValue) {
        armour += armourValue;
        if (armour>5) armour = 5;
    }
    void Start() {
        dialogueQueue = new List<string>{};
        dialoguebox = GameObject.FindGameObjectWithTag("DialogueBox");
        dialogueWindow = GameObject.FindGameObjectWithTag("DialogueWindow");
        dialoguebox.SetActive(false);
        rigidbody = GetComponent<Rigidbody2D>();
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
        checkMood();
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
        // string log = "";
        // foreach (Collider2D item in objects)
        // {
        //     log+= item.gameObject.name + "      ";
        // }
        // Debug.Log(log);
        //animator.SetTrigger("isRunning");
    }

    public void takeDamage() {
        if (armour>0) {
            armour--;
        } else if(health>0) {
            health--; 
        } else {
            Instantiate(sceneEnder);
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

    private void OnCollisionEnter2D(Collision2D collision2D) {
        
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

        if (collision.tag == "Consumable") {
            Debug.Log("enter in collision fields");
            Consumable consumable = collision.gameObject.GetComponent<Consumable>();
            consumable.consume();
            Destroy(consumable.gameObject);
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
            animator.SetTrigger("attack");
            Instantiate(Boolet, transform.position, Quaternion.identity);

        }

    }

    private void checkMood() {
        if(!isDelayStarted && mood < 30) {
            isDelayStarted = true;
            StartCoroutine(MovementDiscomfortTick());
        } else if(!isConsumableDelayStarted && mood > 70) {
            isConsumableDelayStarted = true;
            StartCoroutine(GetConsumableTick());
        }
    }

    IEnumerator DialogueBoxAutoTimeDisable(){
        yield return new WaitForSeconds(4f);
        dialoguebox.SetActive(false);
        isCoroutineStarted = false;
    }

    IEnumerator MovementDiscomfortTick(){
        int randomDelay = Random.Range(maxDelay, minDelay);
        yield return new WaitForSeconds(randomDelay);
        int randomForce = Random.Range(minForce, maxForce);
        Vector2 input = new Vector2(Random.Range(-1,1) * randomForce,
                                    Random.Range(-1,1) * randomForce);
        rigidbody.AddForce(input);
        isDelayStarted = false;
    }

    IEnumerator GetConsumableTick(){
        yield return new WaitForSeconds(randomConsumableDelay);
        
        if(armour!=5) {
            armourRestore(2);
            dialogueQueue.Add("Some armour, not bad");
        } else if(health!=10) {
            Heal(2);
            dialogueQueue.Add("Hahaha, found some health");
        }
               
        isConsumableDelayStarted = false;
    }

    public static void addRandomDialogueFromList(List<string> dialogues) {
        bool isAlreadyHasInQueue = false;
        string description = dialogues[Random.Range(0,dialogues.Count-1)];
        foreach (string item in dialogueQueue)
        {
            if(description.Equals(item)) {
                isAlreadyHasInQueue = true;
                break;
            }    
        }
        if(!isAlreadyHasInQueue){
            dialogueQueue.Add(description);
        }
    }

}
