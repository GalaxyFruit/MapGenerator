using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCube : Cube
{
    protected override void AssignCubeType()
    {
        cubeType = Cubes.water;
    }
}
