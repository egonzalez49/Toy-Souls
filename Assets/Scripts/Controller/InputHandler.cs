using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OOB
{
    public class InputHandler : MonoBehaviour
    {

        float vertical;
        float horizontal;
        bool left_stick_input;
        bool b_input;
        bool a_input;
        bool x_input;
        bool y_input;

        bool rb_input;
        float rt_axis;
        bool rt_input;
        bool lb_input;
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
            cameraManager.Init(this.transform);
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


            lb_input = Input.GetButtonDown("LB");
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

            if (lb_input)
            {
                states.twoHanded = !states.twoHanded;
                states.HandleTwoHanded();
            }

            if (left_stick_input)
            {
                states.run = (states.moveAmount > 0);
            }
            else
            {
                states.run = false;
            }

            

        }
    }
}
