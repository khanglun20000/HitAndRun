using UnityEngine;
using TMPro;
public class FloatingText : MonoBehaviour
{
    private TextMeshPro textMeshPr;
    private Color textColor;

    public Vector3 RandonIntensity = new Vector3(10.5f, 10.5f, 10.5f);
    private float disappearTimer;
    public float DISAPPEAR_TIMER_MAX = 0.5f;
    // Start is called before the first frame update
    private void Awake()
    {
        textMeshPr = transform.GetComponent<TextMeshPro>();
        disappearTimer = DISAPPEAR_TIMER_MAX;
        transform.localPosition += new Vector3(Random.Range(-RandonIntensity.x, RandonIntensity.x), Random.Range(-RandonIntensity.y, RandonIntensity.y), 0);
    }
    private void Update()
    {
        float moveSpeed = 3f;
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            //First half of popup lifetime
            float increaseScaleAmount = 0.5f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {//second half
            float decreaseScaleAmount = 0.5f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            //start disappearing
            float disapearSpeed = 0.25f;
            textColor.a -= disapearSpeed * Time.deltaTime;
            textMeshPr.color = textColor;
            textMeshPr.outlineColor = new Color(textMeshPr.color.r, textMeshPr.color.g, textMeshPr.color.b, textMeshPr.color.a - (Time.deltaTime * disapearSpeed));
            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
