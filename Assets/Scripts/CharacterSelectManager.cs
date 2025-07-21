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
        infoText.text = "Player 1 choosing";

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

            infoText.text = "Player 2 choosing ";
            btn.interactable = false; // không cho chọn lại
            currentPlayer = 2;
        }
        else if (currentPlayer == 2 && charName != selectedChar1)
        {
            selectedChar2 = charName;
            GameData.Player2Character = selectedChar2;

            infoText.text = "CLICK NEXT TO SELECT MAP";
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
        // Reset trạng thái
        currentPlayer = 1;
        selectedChar1 = "";
        selectedChar2 = "";
        GameData.Player1Character = "";
        GameData.Player2Character = "";

        infoText.text = "Player1 choosing";
        okButton.interactable = false;

        // Reset lại các nút chọn nhân vật
        foreach (Button b in characterButtons)
        {
            b.interactable = true;

            // Reset alpha ảnh về bình thường
            Image img = b.GetComponent<Image>();
            Color normalColor = img.color;
            normalColor.a = 1f;
            img.color = normalColor;
        }
    }

}


