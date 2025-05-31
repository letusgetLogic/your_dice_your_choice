using Assets.Scripts.FieldPrefab;
using UnityEngine;

namespace Assets.Scripts.CharacterPrefab.CharacterBody
{
    public class CharacterCollider : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("OnCollisionEnter");
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("OnTriggerEnter2D" + other.gameObject.tag);
            Debug.Log(other.gameObject.name + " | " + other.gameObject.tag);

            if (other.gameObject.tag == "Field")
            {
                var fieldObject = other.gameObject;
                var field = fieldObject.GetComponent<Field>();
                var character = transform.root.GetComponent<Character>();
                character.SetFieldIndex(field.Index); Debug.Log(field.Index);
            }
        }
    }
}
