using UnityEngine;

public abstract class Cube : MonoBehaviour
{
    public enum Cubes { water, grass, hill }
    public Cubes cubeType;

    protected virtual void AssignCubeType()
    {
    }
    void Start()
    {
        AssignCubeType();
    }
}
