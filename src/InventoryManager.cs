using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class InventoryManager : MonoBehaviour
{
    public Tile[] blockTiles; // ����� ����� �����, �� ������ ���� � ��������
    public UnityEngine.UI.Image[] inventorySlots; // ����� ��������� ��� ����������� �����
    public GameObject highlightPrefab; // ������ ��� ����������� �������� �����
    private GameObject currentHighlight; // ������� ������� �������� �����
    public float placeRadius = 1.5f; // ����� ������������ �����
    private int selectedBlockIndex = 0; // ������ �������� �����

    private Transform playerTransform; // ������� ������

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        UpdateInventoryUI(); // ��������� ��������� ��� �������
    }

    void Update()
    {
        // ���� �������� ����� � ��������
        if (Input.GetKeyDown(KeyCode.Alpha1) && blockTiles.Length > 0) // ��� ��������� "1"
        {
            selectedBlockIndex = 0;
            UpdateInventoryUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && blockTiles.Length > 1) // ��� ��������� "2"
        {
            selectedBlockIndex = 1;
            UpdateInventoryUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && blockTiles.Length > 2) // ��� ��������� "3"
        {
            selectedBlockIndex = 2;
            UpdateInventoryUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && blockTiles.Length > 3) // ��� ��������� "4"
        {
            selectedBlockIndex = 3;
            UpdateInventoryUI();
        }

        // ������������ �������� ����� � ��� ��� ��������� ����� ������ ����
        if (Input.GetMouseButton(1)) // ��� ��������� ����� ���
        {
            SetSelectedBlockInWorld();
        }

        // ��������� ������� ��� ��������� ������ "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            ClearHighlight();
        }
    }

    void SetSelectedBlockInWorld()
    {
        // ��������� ������� ������� ���� � ���
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // �������� ������ �� �������� ���� �� �������� ������
        if (Vector3.Distance(mouseWorldPosition, playerTransform.position) > placeRadius)
        {
            // ������� ���� ������� ������ �� ������, ���� �� ������������ ����
            return;
        }

        // ��������� ����� � �������� ������� � ��������
        Tile selectedBlockTile = blockTiles[selectedBlockIndex];

        // ��������� Tilemap, �� ����� ��������� ����
        Tilemap tilemap = FindObjectOfType<Tilemap>();

        // ������������ ������� ���� � ���������� Tilemap
        Vector3Int tilePosition = tilemap.WorldToCell(mouseWorldPosition);

        // ������������ �������� ����� � ��� �� �������� ����
        tilemap.SetTile(tilePosition, selectedBlockTile);
    }

    void UpdateInventoryUI()
    {
        // ��������� ����������� ���������
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            UnityEngine.UI.Image slotImage = inventorySlots[i];
            if (i < blockTiles.Length)
            {
                // ³���������� ����� � ������
                slotImage.sprite = Sprite.Create(blockTiles[i].sprite.texture, new Rect(0, 0, blockTiles[i].sprite.texture.width, blockTiles[i].sprite.texture.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                // �������� ����� �����������
                slotImage.sprite = null;
            }
        }

        // ��������� ���������� �������, ���� ���� ����
        if (currentHighlight != null)
        {
            Destroy(currentHighlight);
        }

        // ��������� ���� ������� ��� �������� �����
        currentHighlight = Instantiate(highlightPrefab, inventorySlots[selectedBlockIndex].transform.position, Quaternion.identity);
        currentHighlight.transform.SetParent(inventorySlots[selectedBlockIndex].transform); // ������������ ������������ ��'���� �������
    }

    void ClearHighlight()
    {
        // ��������� ������� �������� �����
        if (currentHighlight != null)
        {
            Destroy(currentHighlight);
        }
    }
}
