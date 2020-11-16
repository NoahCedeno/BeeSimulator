using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using System.Runtime.CompilerServices;

public class PlayerBeeActions : PlayerActionSet
{
    public PlayerAction Left, Right;
    public PlayerAction Down, Up;
    public PlayerAction Forward, Backward;
    public PlayerAction TurnRight, TurnLeft;
    public PlayerAction Interact;

    public PlayerOneAxisAction XMove, YMove, ZMove, Yaw;

    public PlayerBeeActions()
    {
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");

        Down = CreatePlayerAction("Move Down");
        Up = CreatePlayerAction("Move Up");

        Forward = CreatePlayerAction("Move Forward");
        Backward = CreatePlayerAction("Move Backward");

        TurnRight = CreatePlayerAction("Turn Right");
        TurnLeft = CreatePlayerAction("Turn Left");

        Interact = CreatePlayerAction("Interact");

        //

        XMove = CreateOneAxisPlayerAction(Left, Right);
        YMove = CreateOneAxisPlayerAction(Down, Up);
        ZMove = CreateOneAxisPlayerAction(Backward, Forward);
        Yaw = CreateOneAxisPlayerAction(TurnLeft, TurnRight);
    }
}