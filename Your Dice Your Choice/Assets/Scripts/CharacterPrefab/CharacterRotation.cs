namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterRotation : CharacterComponents
    {
        /// <summary>
        /// Rotate the pivot point.
        /// </summary>
        public void RotatePivot(int number)
        {
            var rotation = BodyTransform.rotation;
            rotation.z += number;
            BodyTransform.rotation = rotation;
        }
    }
}

