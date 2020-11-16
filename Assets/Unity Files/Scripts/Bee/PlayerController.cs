using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Must include namespace!
using BeeSystem;
using MultiplayerBasicExample;
using InControl;

/// <summary>
/// This class provides basic player input for the BeeController to take in!
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Vector3 Movement;

    public BeeController Bee;
    public Collider Other;

    public PlayerBeeActions BeeActions;

    private void Start()
    {
        // Start with no movement.
        Movement = Vector3.zero;

        // InControl
        BeeActions = new PlayerBeeActions();

        BeeActions.Left.AddDefaultBinding(Key.LeftArrow);
        BeeActions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);

        BeeActions.Right.AddDefaultBinding(Key.RightArrow);
        BeeActions.Right.AddDefaultBinding(InputControlType.LeftStickRight);

        BeeActions.Up.AddDefaultBinding(Key.I);
        BeeActions.Up.AddDefaultBinding(InputControlType.RightBumper);

        BeeActions.Down.AddDefaultBinding(Key.K);
        BeeActions.Down.AddDefaultBinding(InputControlType.LeftBumper);

        BeeActions.Forward.AddDefaultBinding(Key.UpArrow);
        BeeActions.Forward.AddDefaultBinding(InputControlType.LeftStickUp);

        BeeActions.Backward.AddDefaultBinding(Key.DownArrow);
        BeeActions.Backward.AddDefaultBinding(InputControlType.LeftStickDown);

        BeeActions.TurnLeft.AddDefaultBinding(Key.J);
        BeeActions.TurnLeft.AddDefaultBinding(InputControlType.RightStickLeft);

        BeeActions.TurnRight.AddDefaultBinding(Key.L);
        BeeActions.TurnRight.AddDefaultBinding(InputControlType.RightStickRight);

        BeeActions.Interact.AddDefaultBinding(Key.Space);
        BeeActions.Interact.AddDefaultBinding(InputControlType.Action1);
    }

    private void Update()
    {
        // X Movement
        if (BeeActions.XMove.Value != 0)
        {
            //Movement.x = Input.GetAxis("Horizontal");
            Movement.x = BeeActions.XMove.Value;
        }

        // Z Movement
        if (BeeActions.ZMove.Value != 0)
        {
            //Movement.z = Input.GetAxis("Vertical");
            Movement.z = BeeActions.ZMove.Value;
        }

        // Y Movement
        //int verticalInput = 0;
        //if (Input.GetKey(KeyCode.E)) verticalInput = 1;
        //else if (Input.GetKey(KeyCode.Q)) verticalInput = -1;
        float verticalInput = BeeActions.YMove.Value;

        Movement.y = verticalInput; // Because this is our Y Acceleration!

        // Turn on Y / Yaw
        float yawDirection = 0;
        //if (Input.GetKey(KeyCode.J) || Input.GetButton("Fire1")) yawDirection = -1;
        //else if (Input.GetKey(KeyCode.L) || Input.GetButton("Fire3")) yawDirection = 1;
        yawDirection = BeeActions.Yaw.Value;

        // Interact
        //bool isInteracting = Input.GetKey(KeyCode.K) || Input.GetButton("Fire2");
        bool isInteracting = BeeActions.Interact.WasPressed;

        // Finally,
        Bee.Move(Movement);
        Bee.Turn(yawDirection);

        if (isInteracting && Other != null)
        {
            Bee.Interact(Other);
        }
    }

    // + + + + + | Collision Handling | + + + + +

    // When triggering a collider, make it my collider
    public void OnTriggerEnter(Collider other)
    {
        Other = other;
    }

    // When leaving a collider, let it go
    public void OnTriggerExit(Collider other)
    {
        Other = null;
    }
}