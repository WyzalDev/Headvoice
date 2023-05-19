using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private GameObject healthPrefab;

    [SerializeField]
    private GameObject armourPrefab;

    [SerializeField]
    private GameObject goodMood;

    [SerializeField]
    private GameObject badMood;

    private GameObject currentMood;

    [SerializeField]
    private GameObject normalMood;

    LinkedList<GameObject> healthBar;
    LinkedList<GameObject> armourBar;

    [SerializeField]
    GameObject parentHealthBar;

    [SerializeField]
    GameObject parentArmourBar;

    [SerializeField]
    GameObject parentMoodBar;

    [SerializeField]
    GameObject kingKongTextBar;

    Character character;

    [SerializeField]
    private int indent=20;

    [SerializeField]
    Vector2 moodBarPosition;

    [SerializeField]
    Vector2 nextHealthPosition;

    [SerializeField]
    Vector2 nextArmourPosition;

    
    void Start() {
        character = GameObject.Find("Character").GetComponent<Character>();
        healthBar = new LinkedList<GameObject>();
        armourBar = new LinkedList<GameObject>();
        Debug.Log(moodBarPosition.ToString() + "    " + nextHealthPosition.ToString() + "     " + nextArmourPosition);
        //health
        for(int i=0; i<character.getHealth(); i++) {
            GameObject health = Instantiate(healthPrefab);
            health.transform.GetComponent<RectTransform>().anchoredPosition3D = nextHealthPosition;

            health.transform.SetParent(parentHealthBar.transform, false);
            healthBar.AddLast(health);
            nextHealthPosition += new Vector2(indent, 0);
        }
        //armour
        for(int i=0; i<character.getArmour(); i++) {
            GameObject armour = Instantiate(armourPrefab);
            armour.transform.GetComponent<RectTransform>().anchoredPosition3D = nextArmourPosition;
            
            armour.transform.SetParent(parentArmourBar.transform, false);
            armour.transform.parent = parentArmourBar.transform;
            armourBar.AddLast(armour);
            nextArmourPosition += new Vector2(indent, 0);
        }

        currentMood = Instantiate(normalMood, moodBarPosition, Quaternion.Euler(Vector3.zero));
        currentMood.transform.SetParent(parentMoodBar.transform, false);

    }

    void Update() {
        if(isHpChanged()) {
            changeHp();
        }
        if(isArmourChanged()) {
            ChangeArmour();
        }
        tryChangeMood();
        if(isKingKongCounterChanged()) {
            changeKingKongCounterText();
        }
    }

    bool isKingKongCounterChanged() {
        TMP_Text textComponent = kingKongTextBar.GetComponent<TMP_Text>(); 
        EventsContainer eventsContainer = GameObject.Find("EventController").GetComponent<EventsContainer>();
        return !textComponent.text.Equals(eventsContainer.getKingKongAppearCounter().ToString());
    }

    void changeKingKongCounterText() {
        kingKongTextBar.GetComponent<TMP_Text>().text = GameObject.Find("EventController").GetComponent<EventsContainer>().getKingKongAppearCounter().ToString();
    }

    void tryChangeMood() {
        int mood = character.getMood();
        if(mood < 30) {
            if(!currentMood.name.Equals("BadMood")) {
                changeMood(badMood);
            }
        } else if (mood < 70) {
            if(!currentMood.name.Equals("NormalMood")) {
                changeMood(normalMood);
            }    
        } else {
            if(!currentMood.name.Equals("GoodMood")) {
                changeMood(goodMood);
            } 
        }
    }

    void changeMood(GameObject prefab) {
        Destroy(currentMood);
        currentMood = Instantiate(prefab, moodBarPosition, Quaternion.Euler(Vector3.zero));
        currentMood.transform.SetParent(parentMoodBar.transform, false);
    }

    void changeHp(){
        if(healthBar.Count > character.getHealth()) {
            GameObject health = healthBar.Last.Value;
            Destroy(health);
            healthBar.RemoveLast();
            nextHealthPosition -= new Vector2(indent, 0);
        }
        if(healthBar.Count < character.getHealth()) {
            GameObject health = Instantiate(healthPrefab);
            health.transform.GetComponent<RectTransform>().anchoredPosition3D = nextHealthPosition;

            health.transform.SetParent(parentHealthBar.transform, false);
            healthBar.AddLast(health);
            nextHealthPosition += new Vector2(indent, 0);
        }
    }

    void ChangeArmour() {
        if(armourBar.Count > character.getArmour()) {
            GameObject armour = armourBar.Last.Value;
            Destroy(armour);
            armourBar.RemoveLast();
            nextArmourPosition -= new Vector2(indent, 0);
        }
        if(armourBar.Count < character.getArmour()) {
            GameObject armour = Instantiate(armourPrefab);
            armour.transform.GetComponent<RectTransform>().anchoredPosition3D = nextArmourPosition;

            armour.transform.SetParent(parentArmourBar.transform, false);
            armourBar.AddLast(armour);
            nextArmourPosition += new Vector2(indent, 0);
        }
    }

    bool isArmourChanged() {
        return armourBar.Count != character.getArmour();
    }

    bool isHpChanged() {
        return healthBar.Count != character.getHealth();
    }
}
