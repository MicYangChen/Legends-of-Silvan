using System.Collections;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // An array to store instances
    private static DontDestroy[] instances;

    public int instanceIndex;

    void Awake()
    {
        // If instances array is null, initialize it
        if (instances == null)
        {
            instances = new DontDestroy[2]; // Index = 0 for Player saved data, 1 for UI saved data
        }

        // Check if there is an instance at this index
        if (instances[instanceIndex] != null && instances[instanceIndex] != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Set this instance in the array
        instances[instanceIndex] = this;

        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public static void DestroyPersistingObjects()
    {
        if (instances != null)
        {
            for (int i = 0; i < instances.Length; i++)
            {
                if (instances[i] != null)
                {
                    Destroy(instances[i].gameObject);
                }
            }
        }
    }
}
