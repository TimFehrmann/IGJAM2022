using UnityEngine;

[CreateAssetMenu]
public class InputConfig : ScriptableObject
{
    [SerializeField]
    private INPUTTYPE inputType;

    public INPUTTYPE InputType { get => inputType; }

    public enum INPUTTYPE
    {
        MOUSE,
        TOUCH
    }
}