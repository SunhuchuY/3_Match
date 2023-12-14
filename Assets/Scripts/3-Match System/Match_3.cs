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
    private TileMatrix tileMatrix;

    public List<TilePosition> MatchSearchTileList(TilePosition currentPosition)
    {
        var matchList = new List<TilePosition>();
        var queue = new Queue<TilePosition>();
        var check = new bool[tileMatrix.row, tileMatrix.column];

        // 현재 위치로부터 시작
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

                // 맵 밖으로 나간 경우, 종료
                if (nx < 0 || nx >= tileMatrix.column || ny < 0 || ny >= tileMatrix.row)
                    continue;

                // 이미 방문 했던 경우, 종료
                if (check[ny, nx])
                    continue;

                // 매치 하지 않는 경우, 종료
                if (tileMatrix.GetTile(nx, ny).tileEnum != tileMatrix.GetTile(curX, curY).tileEnum)
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
        // 매치 된 퍼즐 터트리기, 실행
        foreach (var matchPosition in matchPositionList)
        {
            Debug.Log("x: " + matchPosition.x + "," + "y: " + matchPosition.y);

            var matchTile = tileMatrix.GetTile(matchPosition.x, matchPosition.y);
            matchTile.SetRandomTile(tileMatrix.GetTile(matchPosition.x, matchPosition.y).tileEnum);
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
