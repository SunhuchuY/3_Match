using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;


public class Tile : MonoBehaviour
{
    private int _initializeX;
    public int x 
    {
        get
        {
            return _initializeX;
        }
    }

    private int _initializeY;
    public int y
    {
        get
        {
            return _initializeY;
        }
    }

    public bool isSwiping { get; set; }

    public TileEnum tileEnum { get; private set; }

    private Image _tileImage;


    private void Awake()
    {
        _tileImage = GetComponent<Image>(); 
    }

    public void SetPosition(int _x, int _y)
    {
        _initializeX = _x; 
        _initializeY = _y;
    }   

    private TileEnum RandomTileEnum()
    {
        var tileValues = Enum.GetValues(typeof(TileEnum));
        var randomIndex = Random.Range(0, tileValues.Length);

        return (TileEnum)tileValues.GetValue(randomIndex);
    }

    public void SetRandomTile()
    {
        var randomTileEnum = RandomTileEnum();
        ChangeTargetTile(randomTileEnum);
    }

    /// <summary>
    /// �ش� Ÿ�� ������ ���� ����.
    /// </summary>
    /// <param name="exeptionTIle"></param>
    public void SetRandomTile(TileEnum exeptionTIle)
    {
        TileEnum randomTileEnum = RandomTileEnum();
        
        while (exeptionTIle == randomTileEnum)
        {
            randomTileEnum = RandomTileEnum();
        }

        ChangeTargetTile(randomTileEnum);
    }

    public void ChangeTargetTile(TileEnum _tileEnum)
    {
        // Ÿ�Կ� �´� �̹��� ��ü �� Ÿ�� ����
        tileEnum = _tileEnum;
        _tileImage.sprite = Match_3_Manager.Instance.tileSprites[(int)_tileEnum].tileSprite;
    }
}
