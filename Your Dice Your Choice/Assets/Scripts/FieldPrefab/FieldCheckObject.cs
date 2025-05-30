using UnityEngine;
using Assets.Scripts.CharacterPrefab;

public class FieldCheckObject : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var otherScript = other.gameObject.GetComponent<Character>();
        }
    }
}
