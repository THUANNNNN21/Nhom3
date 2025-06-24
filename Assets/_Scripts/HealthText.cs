using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    public float timetoFade = 1f;
    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;
    private float timeEslaped;
    private Color startColor;
    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }
    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;
        timeEslaped += Time.deltaTime;
        if (timeEslaped < timetoFade)
        {
            float fadeAlpha = startColor.a * (1 - (timeEslaped / timetoFade));
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
