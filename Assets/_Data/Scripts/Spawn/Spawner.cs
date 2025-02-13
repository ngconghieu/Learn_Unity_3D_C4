using UnityEngine;

public class Spawner : GameMonoBehaviour
{
    public virtual Transform Spawn(Transform prefab)
    {
        Transform newObject = Instantiate(prefab);
        return newObject;
    }

    public virtual Transform Spawn(Transform prefab, Vector3 position, Quaternion rotation)
    {
        Transform newObject = Instantiate(prefab, position, rotation);
        newObject.gameObject.SetActive(true);
        return newObject;
    }
}
