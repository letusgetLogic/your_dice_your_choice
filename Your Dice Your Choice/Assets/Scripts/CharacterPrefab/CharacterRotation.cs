namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterRotation : CharacterComponents
    {
        /// <summary>
        /// Rotate the body transform.
        /// </summary>
        public void RotateBodyTransform(int number)
        {
            var rotation = BodyTransform.rotation;
            rotation.z += number;
            BodyTransform.rotation = rotation;
        }
    }
}

