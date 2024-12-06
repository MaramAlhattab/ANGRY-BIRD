using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput PlayerInput;
    public InputAction mousePositionAction;
    private InputAction mouseAction;

    public static Vector2 mousePosition;
    public static bool wasleftmousebuttonpreesed;
    public static bool wasleftmousebuttonReleased;
    public static bool isleftmousepreesed;
    void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        mousePositionAction = PlayerInput.actions["MousePosition"];
        mouseAction = PlayerInput.actions["Mouse"];
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition=mousePositionAction.ReadValue<Vector2>();

        wasleftmousebuttonpreesed = mouseAction.WasPerformedThisFrame();
        wasleftmousebuttonReleased = mouseAction.WasReleasedThisFrame();
        isleftmousepreesed = mouseAction.IsPressed();
    }
}
