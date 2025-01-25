using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject knightPrefab;
    [SerializeField] private GameObject[] PrefabObjects;
    [SerializeField] private Utils.ParentObject[] ParentObjectsType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform transform = GetComponent<Transform>();
        GameObject newKnight = Instantiate(knightPrefab, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
