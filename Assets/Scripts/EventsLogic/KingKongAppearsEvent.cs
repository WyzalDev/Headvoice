using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingKongAppearsEvent : Event
{
    [SerializeField]
    private GameObject kingkongPrefab;

    [SerializeField]
    private string description1;

    [SerializeField]
    private string description2;

    [SerializeField]
    private string description3;

    public override void doScenaria()
    {
        Character.addRandomDialogueFromList(new List<string>{description1, description2, description3});
        Instantiate(kingkongPrefab,
                    ObjectsOfInterest.LastDestroyedObjectPosition,
                    Quaternion.Euler(Vector3.zero));
        GameObject.Find("Character").GetComponent<Character>().decreaseMood(20);
    }
}
