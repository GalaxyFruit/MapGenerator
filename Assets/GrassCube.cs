using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassCube : Cube
{
    protected override void AssignCubeType()
    {
        cubeType = Cubes.grass;
    }
}
