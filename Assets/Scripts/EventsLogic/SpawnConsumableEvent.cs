using UnityEngine;

public class SpawnConsumableEvent : Event
{

    [SerializeField]
    private GameObject healthPrefab;

    [SerializeField]
    private GameObject armourPrefab;

    [SerializeField]
    private GameObject hitFruitPrefab;

    Character character;
    void Start() {
        character = GameObject.Find("Character").GetComponent<Character>();
    }
    public override void doScenaria()
    {
        int randomPrefab = Random.Range(1,3);
        GameObject prefabToInstantiate = null;
        switch(randomPrefab) {
            case 1:
            {
                prefabToInstantiate = healthPrefab;
                break;
            }
            case 2:
            {
                prefabToInstantiate = hitFruitPrefab;
                break;
            }
            case 3:
            {
                prefabToInstantiate = armourPrefab;
                break;
            }
        }
        Instantiate(prefabToInstantiate,
                    ObjectsOfInterest.LastDestroyedObjectPosition,
                    Quaternion.Euler(Vector3.zero));
    }
}
