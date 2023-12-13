using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public enum TileEnum
{
    apple = 0,
    banana = 1,
    orange = 2,
    grape = 3
}


[System.Serializable]
public class TileSprite
{
    [SerializeField]
    private TileEnum _tileEnum;
    public TileEnum tileEnum => _tileEnum;

    [SerializeField]
    public Sprite _tileSprite;
    public Sprite tileSprite => _tileSprite;

}


public class Match_3_Manager : MonoBehaviour
{
    public static Match_3_Manager Instance;

    [SerializeField]
    private Grid _grid;

    [SerializeField]
    private Match_3 _match_3;

    public TileMatrix _tileMatrix;
        
    [SerializeField]
    private List<TileSprite> _tileSprites = new List<TileSprite>();
    public List<TileSprite> tileSprites => _tileSprites;

    private event Action<(int x, int y)> _TileEndDragEvent;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _grid.GridInstantiate();

        // Add Event Action
        _TileEndDragEvent += _match_3.Match_Action;
    }

    public void TileEndDragEventHandler((int x, int y) currentPosition, Tile curTile ,Tile targetTile)
    {
        _tileMatrix.Swap(curTile, targetTile);
        _TileEndDragEvent?.Invoke(currentPosition);
    }
}
