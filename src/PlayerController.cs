using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string selectedCharacterName;
    public UnityEngine.UI.Text characterNameText;
    public GameObject inventoryCanvas;
    public Sprite[] walkSprites; // ����� ������� ��� ������� ������
    public float moveSpeed = 5f; // �������� ���� ������
    public float jumpForce = 10f; // ���� ������� ������

    private Rigidbody2D rb; // Rigidbody ������
    private bool isGrounded = false; // �������, �� ������� ����������� �� ����
    private SpriteRenderer spriteRenderer; // ��������� ��� ���� �������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ������������ ���� ���������
        selectedCharacterName = PlayerPrefs.GetString("SelectedCharacter");
        characterNameText.text = selectedCharacterName;

        // ������������ ���������� ��� ��������� ������
        LoadPlayerPosition();
    }

    void Update()
    {
        // ³���������� ��� ���������� ��������� ��� ��������� Esc ��� "E"
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }

        // ��� ������
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * moveSpeed * Time.deltaTime);

        // ������� ������ � ������� ����
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false; // ������� ������ ������
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true; // ������� ������ ����
        }

        // ������� ������
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            isGrounded = false; // ������������ ��������� "isGrounded" � false, ������� ������� ����� � �����
        }

        // ³��������� ������� ������
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            PlayWalkAnimation();
        }
    }

    void PlayWalkAnimation()
    {
        // ������� ������ ������ ��� ������� ������
        int spriteIndex = Mathf.FloorToInt(Time.time * 10) % walkSprites.Length;
        spriteRenderer.sprite = walkSprites[spriteIndex];
    }

    private void LoadPlayerPosition()
    {
        // ��������� ������� ������ � PlayerPrefs
        float playerX = PlayerPrefs.GetFloat(selectedCharacterName + "_PlayerX", 0f);
        float playerY = PlayerPrefs.GetFloat(selectedCharacterName + "_PlayerY", 0f);
        Vector3 playerPosition = new Vector3(playerX, playerY, 0f);

        // ���������� ������ �� ���� �������
        transform.position = playerPosition;
    }

    private void ToggleInventory()
    {
        // ³���������� ��� ���������� ���������
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }

    public void SavePlayerPosition()
    {
        // ���������� ������� ������ � PlayerPrefs
        PlayerPrefs.SetFloat(selectedCharacterName + "_PlayerX", transform.position.x);
        PlayerPrefs.SetFloat(selectedCharacterName + "_PlayerY", transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��������, �� ������� ���������� �� ���������
        isGrounded = true; // ������������ ��������� "isGrounded" � true, ������� ������� ��������� ����-��� ���糿
    }
}
