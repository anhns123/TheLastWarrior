using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    void Start()
    {
        SpawnCharacter(GameData.Player1Character, spawnPoint1, "Player1");
        SpawnCharacter(GameData.Player2Character, spawnPoint2, "Player2");
    }

    void SpawnCharacter(string characterName, Transform spawnPoint, string playerTag)
    {
        if (string.IsNullOrEmpty(characterName))
        {
            Debug.LogError($"❌ Character name for {playerTag} is null or empty.");
            return;
        }

        // Load từ Resources/Prefabs/CharacterName.prefab
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + characterName);
        if (prefab == null)
        {
            Debug.LogError($"❌ Could not find prefab for {characterName} in Resources/Prefabs/");
            return;
        }

        GameObject character = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        character.name = characterName + "_" + playerTag; // Đặt tên rõ ràng hơn cho nhân vật trong scene
    }
}
