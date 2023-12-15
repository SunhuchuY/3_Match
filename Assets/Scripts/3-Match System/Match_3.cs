using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public struct TilePosition
{
    public int x;
    public int y;
}

public class Match_3 : MonoBehaviour
{
    readonly int[] dx = { 0, 0, 1, -1 };
    readonly int[] dy = { -1, 1, 0, 0 };


    [SerializeField]
    private TileMatrix _tileMatrix;

    [SerializeField]
    private Score _score;


    public List<TilePosition> MatchSearchTileList(TilePosition currentPosition)
    {
        var matchList = new List<TilePosition>();
        var queue = new Queue<TilePosition>();
        var check = new bool[_tileMatrix.row, _tileMatrix.column];

        // ���� ��ġ�κ��� ����
        queue.Enqueue(new TilePosition { y = currentPosition.y, x = currentPosition.x });
        check[currentPosition.y, currentPosition.x] = true;
        matchList.Add(new TilePosition { y = currentPosition.y,  x = currentPosition.x });

        while (queue.Count > 0) 
        {
            var pair = queue.Dequeue(); 
            int curX = pair.x;
            int curY = pair.y;

            for (int i = 0; i < 4; i++)
            {
                int nx = curX + dx[i];
                int ny = curY + dy[i];

                // �� ������ ���� ���, ����
                if (nx < 0 || nx >= _tileMatrix.column || ny < 0 || ny >= _tileMatrix.row)
                    continue;

                // �̹� �湮 �ߴ� ���, ����
                if (check[ny, nx])
                    continue;

                // ��ġ ���� �ʴ� ���, ����
                if (_tileMatrix.GetTile(nx, ny).tileEnum != _tileMatrix.GetTile(curX, curY).tileEnum)
                    continue;

                // ���� �ϰ��ִ� Ÿ��, ����
                if (_tileMatrix.GetTile(nx, ny).isSwiping)
                    continue;

                // Boom �ϰ��ִ� Ÿ��, ����
                if (_tileMatrix.GetTile(nx, ny).isBoom)
                    continue;

                queue.Enqueue(new TilePosition { y = ny, x = nx });
                check[ny, nx] = true;
                matchList.Add(new TilePosition { y = ny, x = nx });
            }
        } 

        return matchList;
    }   

    public void Match_Action(List<TilePosition> matchPositionList)
    {
        // �׽�Ʈ�� �����ø���
        _score.AttributeScore(matchPositionList.Count * 10);

        // ��ġ �� ���� ��Ʈ����, ����
        foreach (var matchPosition in matchPositionList)
        {
            Debug.Log("x: " + matchPosition.x + "," + "y: " + matchPosition.y);

            var matchTile = _tileMatrix.GetTile(matchPosition.x, matchPosition.y);
            matchTile.Boom();
            matchTile.SetRandomTile(_tileMatrix.GetTile(matchPosition.x, matchPosition.y).tileEnum);
        }
    }

    public bool IsMatchOver3(List<TilePosition> tileList)
    {
        Debug.Log("count : " + tileList.Count);
        if (tileList.Count >= 3)
            return true;
        else
            return false;
    }

}
