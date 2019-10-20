using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class ActionManager : MonoBehaviour
    {

        public List<Action> actionSlots = new List<Action>();

        StateManager states;

        public ActionManager()
        {
            for (int i = 0; i < 4; i++)
            {
                Action a = new Action();
                a.input = (ActionInput)i;
                actionSlots.Add(a);
            }
        }

        public void Init(StateManager st)
        {
            states = st;
            UpdateActionsOneHanded();
        }

        public void UpdateActionsOneHanded()
        {
            EmpyAllSlots();
            Weapon w = states.inventoryManager.currentWeapon;
            for (int i = 0; i < w.actions.Count; i++)
            {
                Action a = GetAction(w.actions[i].input);
                a.targetAnimation = w.actions[i].targetAnimation;
            }
        }

        public void UpdateActionsTwoHanded()
        {
            EmpyAllSlots();
            Weapon w = states.inventoryManager.currentWeapon;
            for (int i = 0; i < w.two_handed_actions.Count; i++)
            {
                Action a = GetAction(w.two_handed_actions[i].input);
                a.targetAnimation = w.two_handed_actions[i].targetAnimation;
            }
        }

        void EmpyAllSlots()
        {
            for (int i = 0; i < 4; i++)
            {
                Action a = GetAction((ActionInput)i);
                a.targetAnimation = null;
            }
        }

        public Action GetActionSlots(StateManager st)
        {
            ActionInput a_input = GetActionInput(st);
            return GetAction(a_input);
        }

        Action GetAction(ActionInput input)
        {
            for (int i = 0; i < actionSlots.Count; i++)
            {
                if(actionSlots[i].input == input)
                    return actionSlots[i];
            }
            return null;
        }

        public ActionInput GetActionInput(StateManager st)
        {
            if (st.rb)
                return ActionInput.RB;
            if (st.rt)
                return ActionInput.RT;
            if (st.lb)
                return ActionInput.LB;
            if (st.lt)
                return ActionInput.LT;
            return ActionInput.RB;
        }
    }

    public enum ActionInput
    {
        RB, LB, RT, LT
    }

    [System.Serializable]
    public class Action
    {
        public ActionInput input;
        public string targetAnimation;


    }
}
