using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Red_Knight;
    public GameObject Blue_Knight;
    public GameObject Red_Mage;
    public GameObject Blue_Mage;
    public GameObject GetPrefabInstance(Utils.ParentObject type, Utils.Role role, Path path)
    {
        // Instantiate a unit, set its path, and return it
        Path newPath = createRandomPath(path);
        
        switch (type)
        {
            case Utils.ParentObject.Knight:
                return GetKnight(role, newPath);
            case Utils.ParentObject.Mage:
                return GetMage(role, newPath);
            default:
                return GetKnight(role, newPath);
        }
    }

    private GameObject GetKnight(Utils.Role role, Path path)
    {
        GameObject result = null;
        switch (role)
        {
            case Utils.Role.Attacker:
                result = Instantiate(Red_Knight, path.waypoints[0]);
                result.GetComponent<Knight>().setPath(path);
                return result;
            case Utils.Role.Defender:
                result = Instantiate(Blue_Knight, path.waypoints[0]);
                result.GetComponent<Knight>().setPath(path);
                return result;
            default:
                result = Instantiate(Red_Knight, path.waypoints[0]);
                result.GetComponent<Knight>().setPath(path);
                return result;
        }
    }

    private GameObject GetMage(Utils.Role role, Path path)
    {
        GameObject result = null;
        switch (role)
        {
            case Utils.Role.Attacker:
                result = Instantiate(Red_Mage, path.waypoints[0]);
                result.GetComponent<Mage>().setPath(path);
                return result;
            case Utils.Role.Defender:
                result = Instantiate(Blue_Mage, path.waypoints[0]);
                result.GetComponent<Mage>().setPath(path);
                return result;
            default:
                result = Instantiate(Red_Mage, path.waypoints[0]);
                result.GetComponent<Mage>().setPath(path);
                return result;
        }
    }

    private Path createRandomPath(Path path)
    {
        Path newPath = Instantiate(path);
        for (int i = 0; i < path.waypoints.Length - 1; i++)
        {
            newPath.waypoints[i].position = new Vector3(path.waypoints[i].position.x + Random.Range(-0.5f, 0.5f),
                                                        path.waypoints[i].position.y + Random.Range(-0.5f, 0.5f));
        }
        return newPath;
    }
}
