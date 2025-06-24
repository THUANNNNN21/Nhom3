using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UiManger : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;
    public void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }
    private void OnEnable()
    {
        CharacterEvent.characterDamaged += CharacterTookDamage;
        CharacterEvent.characterHealed += CharacterHealed;
    }
    private void OnDisable()
    {
        CharacterEvent.characterDamaged -= CharacterTookDamage;
        CharacterEvent.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        Vector3 spawmPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawmPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }
    public void CharacterHealed(GameObject character, int healthRestored)
    {
        Vector3 spawmPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawmPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = healthRestored.ToString();
    }
}
