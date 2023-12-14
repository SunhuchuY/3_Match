using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using System.Threading.Tasks;
using Unity.Mathematics;

public class TileMatrix : MonoBehaviour
{
    [SerializeField] [Range(0,3)]
    private float _swapDuration = 1.25f;

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

    public async Task Swap(Tile currentTile, Tile changeToTargetTile)
    {
        // 스왑 하고있는 타일이 있을 경우, 종료
        if (currentTile.isSwiping || changeToTargetTile.isSwiping)
            return;

        Vector3 originalCurrentTIlePosition = currentTile.transform.position;
        Vector3 originalChangeToTargetTIlePosition = changeToTargetTile.transform.position;

        TileEnum currentTileType = currentTile.tileEnum;
        TileEnum changeToTargetTileType = changeToTargetTile.tileEnum;

        currentTile.transform.DOMove(originalChangeToTargetTIlePosition, _swapDuration);
        await changeToTargetTile.transform.DOMove(originalCurrentTIlePosition, _swapDuration)
            .SetEase(Ease.InOutBack)
            .OnStart(() => 
            {
                currentTile.isSwiping = true;
                changeToTargetTile.isSwiping = true;
            })
            .OnComplete(() =>
            {
                currentTile.transform.position = originalCurrentTIlePosition;
                changeToTargetTile.transform.position = originalChangeToTargetTIlePosition;

                currentTile.ChangeTargetTile(changeToTargetTileType);
                changeToTargetTile.ChangeTargetTile(currentTileType);


                currentTile.isSwiping = false;
                changeToTargetTile.isSwiping = false;
            })
            .AsyncWaitForCompletion();



    }

}
