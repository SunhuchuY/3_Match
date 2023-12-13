using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Match_3 : MonoBehaviour
{
    readonly int[] dx = { 0, 0, 1, -1 };
    readonly int[] dy = { -1, 1, 0, 0 };


    [SerializeField]
    private TileMatrix tileMatrix;

    private List<(int y, int x)> MatchSearchTileList((int x, int y) currentPosition)
    {
        var matchList = new List<(int y, int x)>();
        var queue = new Queue<(int y, int x)>();
        var check = new bool[tileMatrix.row, tileMatrix.column];

        // ���� ��ġ�κ��� ����
        queue.Enqueue((currentPosition.y, currentPosition.x));
        check[currentPosition.y, currentPosition.x] = true;
        matchList.Add((currentPosition.y, currentPosition.x));

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
                if (nx < 0 || nx >= tileMatrix.column || ny < 0 || ny >= tileMatrix.row)
                    continue;

                // �̹� �湮 �ߴ� ���, ����
                if (check[ny, nx])
                    continue;

                // ��ġ ���� �ʴ� ���, ����
                if (tileMatrix.GetTile(nx, ny).tileEnum != tileMatrix.GetTile(curX, curY).tileEnum)
                    continue;

                queue.Enqueue((ny,nx));
                check[ny, nx] = true;
                matchList.Add((ny, nx));
            }
        } 

        return matchList;
    }   

    public void Match_Action((int x, int y) currentPosition)
    {
        var matchPositionList = MatchSearchTileList(currentPosition);

        // 3�� �̸��̸�, ����
        if (!IsMatchOver3(matchPositionList))
            return;

        // ��ġ �� ���� ��Ʈ����, ����
        foreach (var matchPosition in matchPositionList)
        {
            var matchTile = tileMatrix.GetTile(matchPosition.x, matchPosition.y);
            matchTile.SetRandomTile(tileMatrix.GetTile(matchPosition.x, matchPosition.y).tileEnum);
        }
    }

    private bool IsMatchOver3(List<(int x, int y)> tileList)
    {
        Debug.Log("count : " + tileList.Count);
        
        if (tileList.Count >= 3)
            return true;
        else
            return false;
    }

}
