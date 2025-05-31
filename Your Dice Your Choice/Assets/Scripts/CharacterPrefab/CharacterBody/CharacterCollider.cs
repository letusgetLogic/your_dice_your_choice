using Assets.Scripts.FieldPrefab;
using UnityEngine;

namespace Assets.Scripts.CharacterPrefab.CharacterBody
{
    public class CharacterCollider : MonoBehaviour
    {

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Field"))
            {
                var fieldObject = other.gameObject;
                var field = fieldObject.GetComponent<Field>();
                var character = transform.root.GetComponent<Character>();
                character.SetFieldIndex(field.Index);
            }
        }
    }
}
