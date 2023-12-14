using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TileDragAndDrop : MonoBehaviour
{
    readonly int[] dx = { 0, 0, 1, -1 };
    readonly int[] dy = { -1, 1, 0, 0 };

    private EventTrigger _EventTrigger;
    private Tile _Tile;

    private event Action<BaseEventData> _BeginDragEvent;
    private event Action<BaseEventData> _OnDragEvent;
    private event Action<BaseEventData> _EndDragEvent;


    private void Awake()
    {
        // GetComponent
        _EventTrigger = GetComponent<EventTrigger>();
        _Tile = GetComponent<Tile>();

        // Add Event
        _BeginDragEvent += BeginDrag;
        _OnDragEvent += OnDrag;
        _EndDragEvent += EndDrag;

        // Add or replace the existing OnBeginDrag listener
        AddEventTriggerListener(EventTriggerType.BeginDrag, BeginDrag);
        AddEventTriggerListener(EventTriggerType.Drag, OnDrag);
        AddEventTriggerListener(EventTriggerType.EndDrag, EndDrag);
    }

    public void BeginDrag(BaseEventData baseEventData)
    {

    }

    public void OnDrag(BaseEventData baseEventData)
    {

    }   


    public void EndDrag(BaseEventData baseEventData)
    {
        var _TileMatrix  = Match_3_Manager.Instance._tileMatrix;     
        var pointerEventData = (PointerEventData)baseEventData;
        var position = Camera.main.ScreenToWorldPoint(pointerEventData.position);

        // 현재 타일포지션과 마우스포지션이 변화가 없다면, 드래그 안한 것으로 간주
        if (Vector2.Distance(position, _Tile.transform.position) == 0)
            return;

        // 타겟팅 되는 타일 체크
        for (int i = 0; i < 4; i++)
        {
            int nx = _Tile.x + dx[i];
            int ny = _Tile.y + dy[i];

            Tile targetTile = _TileMatrix.GetTile(nx, ny);

            // 맵 밖으로 나간 경우, 종료
            if (nx < 0 || nx >= _TileMatrix.column || ny < 0 || ny >= _TileMatrix.row)
                continue;

            // 타겟이 잡히지 않는 경우, 종료
            if (targetTile == null)
                continue;

            // 거리가 일정 범위 이상인 경우, 종료
            if (Vector2.Distance(position, targetTile.transform.position) > 1)
                continue;

            // 타겟이 잡힘
            Match_3_Manager.Instance.TileEndDragEventHandler(new TilePosition { y = ny, x = nx }, _Tile, targetTile);
            return;
        }
    }

    private void AddEventTriggerListener(EventTriggerType eventTriggerType ,UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback.AddListener(callback);
        _EventTrigger.triggers.Add(entry);
    }
}
