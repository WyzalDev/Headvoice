using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine.UI;
using UnityEngine;

public class ObjectsOfInterest : MonoBehaviour
{
    private Character character;

    private bool isButtonsActive;

    [SerializeField]
    private string description1;

    [SerializeField]
    private string description2;

    [SerializeField]
    private string description3;

    private GameObject dialoguebox;

    private EventsContainer eventsContainer;

    [SerializeField]
    private float distanceToEnableButtons = 1f;

    public static Vector3 LastDestroyedObjectPosition = Vector3.zero;
    
    
    void Start()
    {
        character = GameObject.Find("Character").GetComponent<Character>();
        eventsContainer = GameObject.Find("EventController").GetComponent<EventsContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        isButtonsActive = enableButtonIfPlayerNear();
        if(isButtonsActive) {
            checkIfEPressed();
            checkIfQPressed();
        }
    }

    void checkIfEPressed() {
        if(Input.GetKeyDown(KeyCode.E)) {
            LastDestroyedObjectPosition = gameObject.transform.position;
            eventsContainer.startRandomEvent();
            Destroy(gameObject);
        }
    }

    void checkIfQPressed() {
        if(Input.GetKeyDown(KeyCode.Q)) {
            Character.addRandomDialogueFromList(new List<string>{description1, description2, description3});
        }
    }

    bool enableButtonIfPlayerNear() {
        if (Vector2.Distance(character.transform.position, transform.position)<=distanceToEnableButtons) {
            return true;
        } 
        if (Vector2.Distance(character.transform.position, transform.position)>distanceToEnableButtons) {
            return false;
        }
        return false;
    }
}
