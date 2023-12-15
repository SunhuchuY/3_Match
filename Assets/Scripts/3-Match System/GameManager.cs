using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum GameStateType
{
    GameOver,
    GamerPause,
    GameStart
}

public interface IDragAndDrop
{
    public void BeginDrag(BaseEventData baseEventData);

    public void OnDrag(BaseEventData baseEventData);

    public void EndDrag(BaseEventData baseEventData);
}

public class Attributive
{
    public int currentValue { get; private set; }

    public void Attribute(int attributeAmount)
    {
       currentValue += attributeAmount;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private Transform _gameoverPanel;

    [SerializeField]
    private float _gameoverMoveDuration = 1f;

    [SerializeField]
    private Transform _gameoverStartPosition, _gameoverEndPosition;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        _timer.TimerStart();
        SoundManager.Instance.AudioPlay(GameStateType.GameStart);
    }

    public void GameOver()
    {
        SoundManager.Instance.AudioPlay(GameStateType.GameOver);
        _gameoverPanel.position = _gameoverStartPosition.position;
        _gameoverPanel.gameObject.SetActive(true);
            
        _gameoverPanel.DOMove(_gameoverEndPosition.position, _gameoverMoveDuration);
    }
}
