using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillCube : Cube
{
    protected override void AssignCubeType()
    {
        cubeType = Cubes.hill;
    }
}
