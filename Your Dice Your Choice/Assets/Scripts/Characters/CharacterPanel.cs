using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class CharacterPanel : MonoBehaviour
    {

        //public GameObject Avatar;
        //public GameObject[] DicePanel;

        private GameObject _character;

        /// <summary>
        /// Start method.
        /// </summary>
        void Start()
        {
            //InitializeSlots();

        }

        /// <summary>
        /// Initializes _character.
        /// </summary>
        /// <param name="character"></param>
        public void SetCharacter(GameObject character)
        {
            _character = character;
        }

        private void InitializeSlots()
        {
            throw new NotImplementedException();
        }
    }
}
