using UnityEngine;

public class KingKongAppearsEvent : Event
{
    [SerializeField]
    private GameObject kingkongPrefab;

    public override void doScenaria()
    {
        Instantiate(kingkongPrefab,
                    ObjectsOfInterest.LastDestroyedObjectPosition,
                    Quaternion.Euler(Vector3.zero));
        GameObject.Find("Character").GetComponent<Character>().decreaseMood(20);
    }
}
