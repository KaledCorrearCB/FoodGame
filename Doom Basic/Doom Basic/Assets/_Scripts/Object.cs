
using UnityEngine;

public class Object : MonoBehaviour
{
    public string type;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(type);
        Destroy(gameObject);
    }
}
