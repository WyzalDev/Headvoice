using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EventsContainer : MonoBehaviour
{
    private List<GameObject> eventsContainer;

    [SerializeField]
    private GameObject spawnEnemyEventObject;

    List<Event> randomEvents;

    List<Event> remainingRandomEvents;

    void Start() {
        var spawnEnemyEvent = spawnEnemyEventObject.GetComponent<SpawnEnemyEvent>();
        eventsContainer = new List<GameObject>();
        randomEvents = new List<Event>();
        randomEvents.Add(spawnEnemyEvent);
        randomEvents.Add(spawnEnemyEvent);
        randomEvents.Add(spawnEnemyEvent);
        remainingRandomEvents = new List<Event>();
    }

    void Update() { 
        foreach (GameObject item in eventsContainer)
        {
            item.GetComponent<Event>().doScenaria();
            Destroy(item);
        }
        eventsContainer.Clear();
    }

    public void startRandomEvent() {
        if(remainingRandomEvents.Count == 0) {
            refillRemainingRandomEvents();
        }
        int randomNumber = Random.Range(0, remainingRandomEvents.Count);
        GameObject randomEvent = Instantiate(remainingRandomEvents[randomNumber].gameObject);
        eventsContainer.Add(randomEvent);
        remainingRandomEvents.RemoveAt(randomNumber);
    }

    private void refillRemainingRandomEvents() {
        remainingRandomEvents =  randomEvents.OrderBy( x => Random.value ).ToList();
    }
    
}
