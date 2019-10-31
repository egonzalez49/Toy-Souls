using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PC
{
    /// <summary>
    /// InputHandler class for user keyboard/mouse or controller input processing
    /// </summary>
    public class InputHandler : MonoBehaviour
    {
        // Values for inputs    
        float vertical;             // vertical look axis
        float horizontal;           // horizontal look axis

        bool left_stick_button;     // click left stick
        bool right_stick_button;    // click right stick
        bool rs_mem;                // memo for right stick button

        // Values for controller buttons
        bool b_input;               
        bool a_input;               
        bool x_input;
        bool y_input;
        bool y_input_mem;           // memo for Y button
        bool startBtn;

        // Bumpers and triggers
        bool rb_input;
        float rt_axis;              // Float value for trigger depression amount
        bool rt_input;              
        bool lb_input;
        float lt_axis;
        bool lt_input;              // Float value for trigger depression amount

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

        /// <summary>
        /// Acquire inputs from keyboard/mouse or controller.
        /// </summary>
        void GetInput()
        {
            vertical = Input.GetAxis(StaticStrings.Vertical);
            horizontal = Input.GetAxis(StaticStrings.Horizontal);

            startBtn = Input.GetButton(StaticStrings.Start);

            left_stick_button = Input.GetButton(StaticStrings.Run);
            right_stick_button = Input.GetButton(StaticStrings.LockOn);

            lb_input = Input.GetButton(StaticStrings.LeftBumper);
            rb_input = Input.GetButton(StaticStrings.RightBumper);

            a_input = Input.GetButton(StaticStrings.AButton);
            b_input = Input.GetButton(StaticStrings.BButton);
            x_input = Input.GetButton(StaticStrings.XButton);
            y_input = Input.GetButton(StaticStrings.YButton);

            rt_input = Input.GetButton(StaticStrings.RightTrigger);
            rt_axis = Input.GetAxis(StaticStrings.RightTrigger);
            if(rt_axis != 0)
                rt_input = true;
            
            lt_input = Input.GetButton(StaticStrings.LeftTrigger);
            lt_axis = Input.GetAxis(StaticStrings.LeftTrigger);
            if(lt_axis != 0)
                lt_input = true;

        }

        /// <summary>
        /// Update state machine for player character using inputs.
        /// </summary>
        void UpdateStates()
        {
            // Calculate camera vectors, movement direction and amounts.
            states.horizontal = horizontal;
            states.vertical = vertical;
            Vector3 v = vertical * cameraManager.transform.forward;
            Vector3 h = horizontal * cameraManager.transform.right;
            states.moveDirection = (v + h).normalized;
            float m = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            states.moveAmount = Mathf.Clamp01(m);

            // Determine if the PC is using item...
            if (x_input)
            {
                left_stick_button = false;
                b_input = false;
            }
            // Determine if the PC is running...
            if (left_stick_button)
            {
                states.run = (states.moveAmount > 0);
                if (states.run)
                    cameraManager.lockon = false;
            }
            else
            {
                states.run = false;
            }
            // Determine if the PC is two-handed...
            if (y_input != y_input_mem && y_input)
            {
                states.twoHanded = !states.twoHanded;
                states.HandleTwoHanded();
            }
            y_input_mem = y_input;
            // Determine if the PC is locking on to a target...
            if (states.lockonTarget != null)       // Disable lockon if target is dead.
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
            if (rs_mem != right_stick_button && right_stick_button) // Check if lockon button pressed.
            {
                states.lockon = !states.lockon;
                if (states.lockonTarget == null)
                    states.lockon = false;
                cameraManager.lockonTarget = states.lockonTarget;
                states.lockonTransform = cameraManager.lockonTransform;
                cameraManager.lockon = states.lockon;
            }
            rs_mem = right_stick_button;

            // Set button information for the state machine.
            states.rt = rt_input;
            states.lt = lt_input;
            states.rb = rb_input;
            states.lb = lb_input;
            states.a = a_input;
            states.b = b_input;
            states.x = x_input;
            states.y = y_input;
            states.lsb = left_stick_button;
            states.startBtn = startBtn;
            states.itemInput = x_input;
            states.rollInput = b_input;
        }
    }
}
