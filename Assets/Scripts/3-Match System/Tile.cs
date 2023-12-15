using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public bool isBoom { get; private set; }

    [SerializeField]
    private ParticleSystem _particleSystem;

    public TileEnum tileEnum { get; private set; }

    private Image _tileImage;


    private void Awake()
    {
        // GetComponent
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
    /// 해당 타입 설정을 막기 위함.
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
        // 타입에 맞는 이미지 교체 및 타입 변경
        tileEnum = _tileEnum;
        _tileImage.sprite = Match_3_Manager.Instance.tileSprites[(int)_tileEnum].tileSprite;
    }

    public async Task Boom()
    {
        isBoom = true;
        _tileImage.color = new Color(_tileImage.color.r, _tileImage.color.g, _tileImage.color.b, 0);
        _particleSystem.Play();

        while (_particleSystem.isPlaying)
            await Task.Delay(10);

        _tileImage.color = new Color(_tileImage.color.r, _tileImage.color.g, _tileImage.color.b, 1);
        isBoom = false;
    }
}
