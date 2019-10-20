using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    public class InputHandler : MonoBehaviour
    {

        float vertical;
        float horizontal;
        bool left_stick_input;

        bool right_stick_input;
        bool rs_mem;

        bool b_input;
        bool a_input;
        bool x_input;
        bool y_input;
        bool y_input_mem;

        bool rb_input;
        float rt_axis;
        bool rt_input;
        bool lb_input;
        bool lb_input_mem;
        float lt_axis;
        bool lt_input;

        StateManager states;
        CameraManager cameraManager;

        float delta;

        // Start is called before the first frame update
        void Start()
        {
            states = GetComponent<StateManager>();
            states.Init();
            cameraManager = CameraManager.singleton;
            cameraManager.Init(states);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            GetInput();
            UpdateStates();
            states.FixedTick(delta);
            cameraManager.Tick(delta);

        }

        void Update()
        {
            delta = Time.deltaTime;
            states.Tick(delta);
        }

        void GetInput()
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");

            left_stick_input = Input.GetButton("RunInput");
            right_stick_input = Input.GetButton("LockonInput");

            lb_input = Input.GetButton("LB");
            rb_input = Input.GetButton("RB");

            a_input = Input.GetButton("A");
            b_input = Input.GetButton("B");
            x_input = Input.GetButton("X");
            y_input = Input.GetButton("Y");

            rt_input = Input.GetButton("RT");
            rt_axis = Input.GetAxis("RT");
            if(rt_axis != 0)
                rt_input = true;
            
            lt_input = Input.GetButton("LT");
            lt_axis = Input.GetAxis("LT");
            if(lt_axis != 0)
                lt_input = true;

        }

        void UpdateStates()
        {
            states.horizontal = horizontal;
            states.vertical = vertical;

            Vector3 v = vertical * cameraManager.transform.forward;
            Vector3 h = horizontal * cameraManager.transform.right;
            states.moveDirection = (v + h).normalized;
            float m = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            states.moveAmount = Mathf.Clamp01(m);

            states.rt = rt_input;
            states.lt = lt_input;
            states.rb = rb_input;
            states.lb = lb_input;
            states.a = a_input;
            states.b = b_input;
            states.x = x_input;
            states.y = y_input;
            states.lsb = left_stick_input;
            states.itemInput = x_input;
            states.rollInput = b_input;

            if (x_input)
            {
                left_stick_input = false;
                b_input = false;
            }

            if (left_stick_input)
            {
                states.run = (states.moveAmount > 0);
                if (states.run)
                    cameraManager.lockon = false;
            }
            else
            {
                states.run = false;
            }

            if (y_input != y_input_mem && y_input)
            {
                states.twoHanded = !states.twoHanded;
                states.HandleTwoHanded();
            }
            y_input_mem = y_input;

            if (states.lockonTarget != null)
            {
                if (states.lockonTarget.enemyStates.isDead)
                {
                    states.lockon = false;
                    states.lockonTarget = null;
                    states.lockonTransform = null;
                    cameraManager.lockon = false;
                    cameraManager.lockonTarget = null;
                }
            }
            

            if (rs_mem != right_stick_input && right_stick_input)
            {
                states.lockon = !states.lockon;
                if(states.lockonTarget == null)
                    states.lockon = false;
                cameraManager.lockonTarget = states.lockonTarget;
                states.lockonTransform = cameraManager.lockonTransform;
                cameraManager.lockon = states.lockon;
            }
            rs_mem = right_stick_input;

        }
    }
}
