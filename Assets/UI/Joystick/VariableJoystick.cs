using UnityEngine;

public class VariableJoystick : MonoBehaviour
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }
    public Vector2 Direction => new(Horizontal, Vertical);
}
