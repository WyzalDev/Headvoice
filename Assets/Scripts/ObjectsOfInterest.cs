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
    private string description;

    private GameObject dialoguebox;

    [SerializeField]
    private float distanceToEnableButtons = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        dialoguebox = GameObject.FindGameObjectWithTag("DialogueBox");
        character = GameObject.Find("Character").GetComponent<Character>();
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
            Destroy(gameObject);
        }
    }

    void checkIfQPressed() {
        if(Input.GetKeyDown(KeyCode.Q)) {
            dialoguebox.SetActive(true);
            GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<TMP_Text>().text = description;
        }
    }

    bool enableButtonIfPlayerNear() {
        if (Vector2.Distance(character.transform.position, transform.position)<=distanceToEnableButtons && !dialoguebox.activeSelf) {
            
            return true;
        } 
        if (Vector2.Distance(character.transform.position, transform.position)>distanceToEnableButtons && dialoguebox.activeSelf) {
            return false;
        }
        return dialoguebox.activeSelf;
    }
}
