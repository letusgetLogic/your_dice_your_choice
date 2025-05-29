using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class Character : MonoBehaviour
    {
        public CharacterData Data { get; private set; }

        public float OriginHP { get; private set; }
        public float OriginAP { get; private set; }
        public float OriginDP { get; private set; }

        public GameObject Panel { get; private set; }

        /// <summary>
        /// Initialize Data.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(CharacterData data)
        {
            Data = data;
            OriginHP = data.HP;
            OriginAP = data.AP;
            OriginDP = data.DP;
        }

        /// <summary>
        /// Initialize Panel.
        /// </summary>
        /// <param name="panel"></param>
        public void SetPanel(GameObject panel)
        {
            Panel = panel;
        }
    }
}

