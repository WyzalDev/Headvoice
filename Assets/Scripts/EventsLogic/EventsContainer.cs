using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EventsContainer : MonoBehaviour
{
    private List<GameObject> eventsContainer;

    [SerializeField]
    private GameObject spawnEnemyEventObject;

    [SerializeField]
    private GameObject spawnConsumableEventObject;

    [SerializeField]
    private GameObject kingKongAppearsEventObject;

    List<Event> randomEvents;

    List<Event> remainingRandomEvents;

    private KingKongAppearsEvent kingKongAppearsEvent;

    private int eventsBeforeKingKong = 5;

    void Start() {
        SpawnEnemyEvent spawnEnemyEvent = spawnEnemyEventObject.GetComponent<SpawnEnemyEvent>();
        SpawnConsumableEvent spawnConsumableEvent = spawnConsumableEventObject.GetComponent<SpawnConsumableEvent>();
        kingKongAppearsEvent = kingKongAppearsEventObject.GetComponent<KingKongAppearsEvent>();
        eventsContainer = new List<GameObject>();
        randomEvents = new List<Event>();
        randomEvents.Add(spawnConsumableEvent);
        randomEvents.Add(spawnConsumableEvent);
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
            return;
        }
        int randomNumber = Random.Range(0, remainingRandomEvents.Count);
        GameObject randomEvent = Instantiate(remainingRandomEvents[randomNumber].gameObject);
        eventsContainer.Add(randomEvent);
        remainingRandomEvents.RemoveAt(randomNumber);
    }

    private void refillRemainingRandomEvents() {
        GameObject kingKongCloneEvent = Instantiate(kingKongAppearsEventObject);
        kingKongCloneEvent.GetComponent<KingKongAppearsEvent>().doScenaria();
        Destroy(kingKongCloneEvent);
        remainingRandomEvents =  randomEvents.OrderBy( x => Random.value ).ToList();
    }

    public void refreshKingKongEventCounter() {
        eventsBeforeKingKong = randomEvents.Count;
    }
    
}
