using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int depth;
    [SerializeField] Transform mapPiecePrefabGrass;
    [SerializeField] Transform mapPiecePrefabWater;
    [SerializeField] Transform mapPiecePrefabHill;
    [SerializeField] Transform playerPrefab;
    [SerializeField] Material disabledMaterial;

    [SerializeField] float maxHeight;
    [SerializeField] float wildness = 8f;

    Transform[,] map;

    float WaterDivios = 0.4f;
    float GrassDivios = 0.6f;


    // Start is called before the first frame update
    void Start()
    {
        map = new Transform[width, depth];
        GenerateMapPieces();
        GeneratePlayer();
    }

    private void GeneratePlayer()
    {
        int randomX = Random.Range(0, width);
        int randomZ = Random.Range(0, depth);

        Transform selectedPiece = map[randomX, randomZ];

        Cube cube = selectedPiece.GetComponent<Cube>();

        if (cube != null && cube.cubeType != Cube.Cubes.water)
        {
            float pieceHeight = selectedPiece.localScale.y;

            var player = Instantiate(playerPrefab, transform);
            player.localPosition = new Vector3(randomX, pieceHeight + 2f, randomZ);
        }
        else
        {
            Debug.Log("Vygeneroval se na vodì!, Generuji znova...");
            GeneratePlayer();
        }
    }





    private void GenerateMapPieces()
    {
        float startX = Random.Range(0, 0.5f);
        float startZ = Random.Range(0, 0.5f);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                var height = Mathf.PerlinNoise(
                    startX + (float)x / (width / wildness),
                    startZ + (float)z / (depth / wildness));

                if (height < WaterDivios)
                {
                    Generator(x, z, height, mapPiecePrefabWater);
                }
                else if (height < GrassDivios && height > WaterDivios)
                {
                    Generator(x, z, height, mapPiecePrefabGrass);
                }
                else
                {
                    Generator(x, z, height, mapPiecePrefabHill);    
                }

            }
        }
    }

    private void Generator(int x, int z, float height, Transform cube)
    {
        var piece = Instantiate(cube, transform);
        piece.localPosition = new Vector3(x, 0, z);
        piece.localScale = new Vector3(1, height * maxHeight, 1);

        if (Random.value > 0.5f)
        {
            piece.gameObject.SetActive(false);
        }

        map[x, z] = piece;
    }

}
