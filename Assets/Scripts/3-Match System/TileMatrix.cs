using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Android.Types;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering;

public class TileMatrix : MonoBehaviour
{
    private const int _row = 5;
    public int row => _row;

    private const int _column = 5;
    public int column => _column;   
        
    private Tile[,] _tileMap = new Tile[_row, _column];


    public void InitializeTile(int targetX, int targetY, Tile _tile)
    {
        _tileMap[targetY, targetX] = _tile;
        _tileMap[targetY, targetX].SetPosition(targetX, targetY);
    }

    public Tile GetTile(int x, int y)
    {
        if (x < 0 || y < 0 || x >= column || y >= row)
            return null;

        return _tileMap[y, x];
    }
    
    public Transform GetTransformPosition(Tile tile)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (_tileMap[i, j] = tile)
                {
                    return _tileMap[i, j].transform;
                }
            }
        }
        return _tileMap[0, 0].transform;
    }

    public (int, int) GetPosition(Tile tile)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (_tileMap[i, j] = tile)
                {
                    return (i, j);
                }
            }
        }
        return (0, 0);
    }

    public void Swap(Tile currentTile, Tile changeToTargetTile)
    {
        //var currentTilePosition = (currentTile.x, currentTile.y);
        //var changeToTargetTilePosition = (changeToTargetTile.x, changeToTargetTile.y);

        var currentTileType = currentTile.tileEnum;
        var changeToTargetTileType = changeToTargetTile.tileEnum;

        //currentTile.SetPosition(changeToTargetTilePosition.x, changeToTargetTilePosition.y);
        //changeToTargetTile.SetPosition(currentTilePosition.x, currentTilePosition.y);

        currentTile.ChangeTargetTile(changeToTargetTileType);
        changeToTargetTile.ChangeTargetTile(currentTileType);
    }
}
