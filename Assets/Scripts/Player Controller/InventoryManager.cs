﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class InventoryManager : MonoBehaviour
    {

        public Weapon currentWeapon;
        public void Init()
        {

        }
    }

    [System.Serializable]
    public class Weapon
    {
        public List<Action> actions;
        public List<Action> two_handed_actions;
        public GameObject weaponModel;

    }
}

