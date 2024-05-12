using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class NextSceneScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public string selectedCharacterName;
    public Sprite groundSprite;
    public Sprite grassSprite; // ������ �����
    public int worldWidth = 100;
    public int worldHeight = 100;
    public float perlinScale = 0.1f;
    public float threshold = 0.5f;
    public Tilemap worldTilemap;
    public GameObject menuCanvas; // ��������� �� Canvas � ����

    void Start()
    {
        worldTilemap = FindObjectOfType<Tilemap>();

        if (worldTilemap == null)
        {
            UnityEngine.Debug.LogError("Tilemap not found in scene!");
            return;
        }

        selectedCharacterName = PlayerPrefs.GetString("SelectedCharacter");

        if (!string.IsNullOrEmpty(selectedCharacterName))
        {
            if (PlayerPrefs.HasKey(selectedCharacterName + "_WorldWidth") &&
                PlayerPrefs.HasKey(selectedCharacterName + "_WorldHeight"))
            {
                GenerateWorld();
                LoadPlayer();
            }
            else
            {
                GenerateWorld();
                SaveWorld();
            }
        }
        else
        {
            UnityEngine.Debug.LogError("No character selected!");
        }
    }

    private void GenerateWorld()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            float perlinValue = Mathf.PerlinNoise((float)x * perlinScale, 0f);
            int surfaceHeight = Mathf.RoundToInt(perlinValue * worldHeight);

            for (int y = 0; y < surfaceHeight; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = groundSprite;
                worldTilemap.SetTile(tilePosition, tile);
            }

            // ����� ������ ������ ����� �� ����� �����
            Vector3Int grassTilePosition = new Vector3Int(x, surfaceHeight - 1, 0);
            Tile grassTile = ScriptableObject.CreateInstance<Tile>();
            grassTile.sprite = grassSprite;
            worldTilemap.SetTile(grassTilePosition, grassTile);
        }

        // �������� ��� ���� ��� ���������
        PlayerPrefs.SetInt(selectedCharacterName + "_WorldWidth", worldWidth);
        PlayerPrefs.SetInt(selectedCharacterName + "_WorldHeight", worldHeight);
    }

    private void LoadPlayer()
    {
        // ��������� ������� ������ � PlayerPrefs
        float playerX = PlayerPrefs.GetFloat(selectedCharacterName + "_PlayerX", worldWidth / 2f);
        float playerY = PlayerPrefs.GetFloat(selectedCharacterName + "_PlayerY", worldHeight / 2f);
        Vector3 playerPosition = new Vector3(playerX, playerY, 0f);

        // ���� ������� ���������� ������ �� ����, ���� ��� ������ ���������
        // Instantiate(playerPrefab, playerPosition, Quaternion.identity);

        UnityEngine.Debug.Log("Player loaded with selected character: " + selectedCharacterName);
    }

    private void SaveWorld()
    {
        // ���������� ������ ���� ��� �������� ���������
        // �������� ������ ����
        PlayerPrefs.SetInt(selectedCharacterName + "_WorldWidth", worldWidth);
        PlayerPrefs.SetInt(selectedCharacterName + "_WorldHeight", worldHeight);

        // �������� ��������� ����� ����
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = worldTilemap.GetTile(tilePosition);
                if (tile != null)
                {
                    // ���� �� ���� (x, y) � ����, �� �������� ���� ���
                    PlayerPrefs.SetInt(selectedCharacterName + "_Tile_" + x + "_" + y, 1);
                }
                else
                {
                    // ���� �� ���� (x, y) ���� �����, �� �������� 0
                    PlayerPrefs.SetInt(selectedCharacterName + "_Tile_" + x + "_" + y, 0);
                }
            }
        }
    }

    private void SavePlayerPosition(Vector3 playerPosition)
    {
        // ���������� ������� ������ � PlayerPrefs
        PlayerPrefs.SetFloat(selectedCharacterName + "_PlayerX", playerPosition.x);
        PlayerPrefs.SetFloat(selectedCharacterName + "_PlayerY", playerPosition.y);
    }

    public void NextScene()
    {
        // ������� �� �������� �����
        SavePlayerPosition(GameObject.FindGameObjectWithTag("Player").transform.position);
        SceneManager.LoadScene("NextSceneName");
    }

    public void OpenMenu()
    {
        // ³���������� Canvas � ����
        menuCanvas.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        // ���������� �� ��������� ����
        SceneManager.LoadScene("MainMenuScene");
    }

    void Update()
    {
        // �������� ���������� ������ "ESC"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ³������� ����
            OpenMenu();
        }
    }
}
