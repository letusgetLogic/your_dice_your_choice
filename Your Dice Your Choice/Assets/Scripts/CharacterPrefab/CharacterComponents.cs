using System.Xml.Linq;
using Assets.Scripts.CharacterPrefab.CharacterBody;
using UnityEngine;

namespace Assets.Scripts.CharacterPrefab
{
    public class CharacterComponents : MonoBehaviour
    {
        public Transform PivotTransform { get; protected set; }
        public Transform BodyPivotTransform { get; protected set; }
        public Transform BodyTransform { get; protected set; }
        public Transform LeftHandTransform { get; protected set; }
        public Transform RightHandTransform { get; protected set; }
        public GameObject HoverColor { get; protected set; }
        public CharacterMouseEvent MouseEvent => BodyTransform.gameObject.GetComponent<CharacterMouseEvent>();
        public GameObject[] Borders => new GameObject[]
        {
            BodyTransform.Find("Border").gameObject,
            LeftHandTransform.Find("Border").gameObject,
            RightHandTransform.Find("Border").gameObject,
        };

        public SpriteRenderer[] ColorSpriteRenderers => new SpriteRenderer[]
        {
            BodyTransform.Find("Color").gameObject.GetComponent<SpriteRenderer>(),
            LeftHandTransform.Find("Color").gameObject.GetComponent < SpriteRenderer >(),
            RightHandTransform.Find("Color").gameObject.GetComponent < SpriteRenderer >(),
        };

        /// <summary>
        /// Awake method.
        /// </summary>
        private void Awake()
        {
            PivotTransform = transform.Find("Pivot");
            BodyPivotTransform = PivotTransform.Find("Body Pivot");
            BodyTransform = BodyPivotTransform.Find("Character Body");
            LeftHandTransform = BodyTransform.Find("Character Left Hand");
            RightHandTransform = BodyTransform.Find("Character Right Hand");

            HoverColor = BodyPivotTransform.Find("Hover Color").gameObject;
        }

        /// <summary>
        /// Sets the component enabled true/false.
        /// </summary>
        /// <param name="component"></param>
        /// <param name="value"></param>
        public void SetEnabled(Component component, bool value)
        {
            if (component is Behaviour behaviour)
            {
                behaviour.enabled = value;
            }
        }
    }
}
