using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private TileMatrix tileMap;

    [SerializeField]
    private GameObject tilePrefab;

    [SerializeField]
    private GameObject tileParentPrefab;


    public void GridInstantiate()
    {
        // Ÿ�ϸ� ���� �� ������ �ҷ�����
        for (int i = 0; i < tileMap.row; i++)
        {
            var parent = Instantiate(tileParentPrefab, transform);

            for (int j = 0; j < tileMap.column; j++)
            {
                var tile = Instantiate(tilePrefab, parent.transform).GetComponent<Tile>();

                tile.SetRandomTile();
                tileMap.InitializeTile(j, i, tile);
            }
        }
    }

}
