using UnityEngine;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.Action
{
    public interface IAction
    {
        ActionData ActionData { get;  set; }
        GameObject CharacterObject { get;  set; }
        Character Character { get;  set; }
        string Description { get;  set; }


        /// <summary>
        /// Initializes data.
        /// </summary>
        /// <param name="data"></param>
        void SetData(ActionData data, GameObject character);
    }
}
