﻿using UnityEngine;
using Assets.Scripts.CharacterPrefab;

namespace Assets.Scripts.ActionDatas
{
    public class Defend : ActionBase
    {
        public Defend(ActionData data, GameObject characterObject) : base(data, characterObject) { }

        public override void SetInteractible(int diceNumber)
        {

        }

        public override void ShowInteractible()
        {
            throw new System.NotImplementedException();
        }

        public override void DeactivateInteractible()
        {
            throw new System.NotImplementedException();
        }

        public override void HandleInput(GameObject fieldObject)
        {
            Debug.Log("Defend!");
        }

    }
}

