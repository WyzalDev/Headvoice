using UnityEngine;

public class SpawnConsumableEvent : Event
{

    [SerializeField]
    private GameObject healthPrefab;

    [SerializeField]
    private GameObject armourPrefab;

    [SerializeField]
    private GameObject hitFruitPrefab;

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
        if(prefabToInstantiate == hitFruitPrefab) {
            GameObject.Find("Character").GetComponent<Character>().decreaseMood(5);
        } else {
            GameObject.Find("Character").GetComponent<Character>().increaseMood(5);
        }

        Instantiate(prefabToInstantiate,
                    ObjectsOfInterest.LastDestroyedObjectPosition,
                    Quaternion.Euler(Vector3.zero));
    }
}
