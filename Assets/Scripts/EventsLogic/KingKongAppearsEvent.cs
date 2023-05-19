using UnityEngine;

public class KingKongAppearsEvent : Event
{
    [SerializeField]
    private GameObject kingkongPrefab;

    private Character character;

    void Start()
    {
        character = GameObject.Find("Character").GetComponent<Character>();
    }

    public override void doScenaria()
    {
        Instantiate(kingkongPrefab,
                    ObjectsOfInterest.LastDestroyedObjectPosition,
                    Quaternion.Euler(Vector3.zero));
    }
}
