using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockDestroyer : MonoBehaviour
{
    public Tilemap tilemap; // ��������� �� Tilemap, �� ����������� ����� ��� ����������
    public float destroyRadius = 1.5f; // �����, � ����� ������� ���� ��������� �����
    private Transform playerTransform; // ��������� �� ��������� ������
    private GameObject currentHighlightedTile; // �������� ��������� ����
    public GameObject highlightPrefab; // ������ ��� ����������� �����

    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 playerPosition = playerTransform.position;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int mouseTilePosition = tilemap.WorldToCell(mouseWorldPosition);

        if (currentHighlightedTile == null || !mouseTilePosition.Equals(currentHighlightedTile.transform.position))
        {
            Destroy(currentHighlightedTile);

            float distanceToPlayer = Vector3.Distance(playerPosition, mouseWorldPosition);
            if (distanceToPlayer <= destroyRadius)
            {
                currentHighlightedTile = tilemap.GetTile(mouseTilePosition) ? Instantiate(highlightPrefab, tilemap.GetCellCenterWorld(mouseTilePosition), Quaternion.identity) : null;
            }
        }

        if (Input.GetMouseButton(0)) // ���� ��������� ��� ������ ����
        {
            DestroyBlockAtPosition(mouseTilePosition);
        }
    }

    void DestroyBlockAtPosition(Vector3Int position)
    {
        TileBase tile = tilemap.GetTile(position);
        if (tile != null)
        {
            tilemap.SetTile(position, null);
        }
    }
}
