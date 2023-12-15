using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public enum TileEnum
{
    apple = 0,
    banana = 1,
    orange = 2,
    grape = 3,
    four = 4,
    five = 5,
    six = 6, 
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
    }

    public async Task TileEndDragEventHandler(TilePosition currentPosition, Tile curTile, Tile targetTile)
    {
        await _tileMatrix.Swap(curTile, targetTile);
        MatchAction(currentPosition);
    }

    private void MatchAction(TilePosition currentPosition)
    {
        List<TilePosition> matchPositionList = _match_3.MatchSearchTileList(currentPosition);

        // 매치가 3 미만인 경우, 종료
        if (!_match_3.IsMatchOver3(matchPositionList))
            return;

        _match_3.Match_Action(matchPositionList);
    }
}
