using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
    public Button[] characterButtons;
    public Button okButton;
    public TextMeshProUGUI infoText;
    public Button reButton;
    private int currentPlayer = 1;
    private string selectedChar1 = "";
    private string selectedChar2 = "";

    void Start()
    {
        okButton.interactable = false;
        infoText.text = "Người chơi 1";

        foreach (Button btn in characterButtons)
        {
            string charName = btn.name; // tên Button trùng tên nhân vật
            btn.onClick.AddListener(() => OnCharacterSelected(btn, charName));
        }
    }

    void OnCharacterSelected(Button btn, string charName)
    {
        if (currentPlayer == 1)
        {
            selectedChar1 = charName;
            GameData.Player1Character = selectedChar1;

            infoText.text = "Người chơi 2 ";
            btn.interactable = false; // không cho chọn lại
            currentPlayer = 2;
        }
        else if (currentPlayer == 2 && charName != selectedChar1)
        {
            selectedChar2 = charName;
            GameData.Player2Character = selectedChar2;

            infoText.text = "Nhấn OK để tiếp tục.";
            okButton.interactable = true;

            // 🔽 Sau khi chọn xong → xử lý toàn bộ nút
            foreach (Button b in characterButtons)
            {
                Image img = b.GetComponent<Image>();

                if (b.name == selectedChar1 || b.name == selectedChar2)
                {
                    // Làm mờ 2 nhân vật được chọn
                    Color fadedColor = img.color;
                    fadedColor.a = 0.4f;
                    img.color = fadedColor;
                }

                // Vô hiệu hoá tất cả các nút
                b.interactable = false;
            }
        }
    }

    public void OnOkClicked()
    {
        SceneManager.LoadScene("MapSelect");
    }
    public void OnResetClicked()
    {
        selectedChar1 = "";
        selectedChar2 = "";
        currentPlayer = 1;

        GameData.Player1Character = "";
        GameData.Player2Character = "";

        infoText.text = "Người chơi 1";
        okButton.interactable = false;

        foreach (Button btn in characterButtons)
        {
            btn.interactable = true;

            Image img = btn.GetComponent<Image>();
            Color color = img.color;
            color.a = 1f; // làm rõ lại
            img.color = color;
        }
    }
}


