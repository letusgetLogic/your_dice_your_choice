namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterRotation : CharacterComponents
    {
        /// <summary>
        /// Rotate the pivot point.
        /// </summary>
        public void RotatePivot(int number)
        {
            var rotation = PivotTransform.rotation;
            rotation.z += number;
            PivotTransform.rotation = rotation;
        }
    }
}

