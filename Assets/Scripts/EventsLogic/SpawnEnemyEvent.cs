using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class SpawnEnemyEvent : Event
{
    [SerializeField]
    private GameObject spiderPrefab;
    [SerializeField]
    private GameObject slimePrefab;

    [SerializeField]
    private string description1;

    [SerializeField]
    private string description2;

    [SerializeField]
    private string description3;


    public override void doScenaria()
    {
        //Send random dialog to character dialogue box
        Character.addRandomDialogueFromList(new List<string>{description1, description2, description3});
        // GameObject dialoguebox = GameObject.Find("Character").GetComponent<Character>().getDialogueStuff()[0];
        // dialoguebox.SetActive(true);
        // GameObject.FindGameObjectWithTag("DialogueWindow").GetComponent<TMP_Text>().text = randomDialogs[randomNumber];

        //Scenaria
        int randomNumberOfSlimes = Random.Range(0, 2);
        int randomNumberOfSpiders = Random.Range(0, 2);
        if(randomNumberOfSlimes == 0 && randomNumberOfSpiders == 0) {
            randomNumberOfSpiders = 1;
        }
        GameObject.Find("Character").GetComponent<Character>().decreaseMood((randomNumberOfSlimes+randomNumberOfSpiders+2)*5);
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
