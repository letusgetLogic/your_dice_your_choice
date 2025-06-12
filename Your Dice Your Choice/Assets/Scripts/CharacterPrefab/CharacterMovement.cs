using UnityEngine;

namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterMovement : CharacterComponents
    {
        public void MoveTo(Vector3 position)
        {
            transform.position = position;
        }
    }
}

