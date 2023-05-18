using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class SpawnEnemyEvent : Event
{
    List<string> randomDialogs = new List<string>{"something1","something2","something3"};

    [SerializeField]
    private GameObject spiderPrefab;
    [SerializeField]
    private GameObject slimePrefab;

    public override void doScenaria()
    {
        //Send random dialog to character dialogue box
        int randomNumber = Random.Range(0,randomDialogs.Count);
        bool isAlreadyHasInQueue = false;
        foreach (string item in Character.dialogueQueue)
        {
            if(randomDialogs[randomNumber].Equals(item)) {
                isAlreadyHasInQueue = true;
                break;
            }    
        }
        if(!isAlreadyHasInQueue){
            Character.dialogueQueue.Add(randomDialogs[randomNumber]);
        }
        // GameObject dialoguebox = GameObject.Find("Character").GetComponent<Character>().getDialogueStuff()[0];
        // dialoguebox.SetActive(true);
        // GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<TMP_Text>().text = randomDialogs[randomNumber];

        //Scenaria
        int randomNumberOfSlimes = Random.Range(0, 2);
        int randomNumberOfSpiders = Random.Range(0, 2);
        if(randomNumberOfSlimes == 0 && randomNumberOfSpiders == 0) {
            randomNumberOfSpiders = 1;
        }
        Vector3 lastObjectPosition = ObjectsOfInterest.LastDestroyedObjectPosition;
        //slime cycle
        for(int i=0; i<=randomNumberOfSlimes; i++) {
            Instantiate(slimePrefab, lastObjectPosition, Quaternion.Euler(Vector3.zero));
        }
        //spider cycle
        for(int i=0; i<=randomNumberOfSpiders; i++) {
            Instantiate(spiderPrefab, lastObjectPosition, Quaternion.Euler(Vector3.zero));
        }
    }
}
