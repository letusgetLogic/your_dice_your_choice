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
        public GameObject HoverColor {  get; protected set; }
        //public CharacterMouseEvent MouseEvent { get; protected set; }

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
            //MouseEvent = BodyTransform.gameObject.GetComponent<CharacterMouseEvent>();
        }

        //private void Update()
        //{
        //    Debug.Log(transform.root.gameObject.GetComponent<Character>().Name + "CharacterMouseEvent>().enabled " + BodyTransform.gameObject.GetComponent<CharacterMouseEvent>().enabled);
            
        //    //if (BodyTransform.gameObject.GetComponent<CharacterMouseEvent>().enabled == false)
        //    //    Debug.Log("CharacterMouseEvent of " + transform.root.gameObject.GetComponent<Character>().Name + " activeSelf = false!");
        //}
    }
}
