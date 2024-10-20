using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager instance;

    [SerializeField] GameObject Prefab;
    [SerializeField] List<TextVariant> TextVariants;
    [SerializeField] float OffsetMultiplier;
    [SerializeField] Transform ParentCanvas;
    ObjectPool<DamageText> texts;
    Color TextColor = Color.black;
    string TextString;
    Vector2 TextPosition;
    private void Awake()
    {
        instance = this;
        texts = new(textCreate, getText, releaseText, DestroyText, true, 10, 20);
    }
    public DamageText textCreate()
    {
        GameObject instance = Instantiate(Prefab, Vector2.zero, Quaternion.identity);
        instance.transform.SetParent(ParentCanvas);
        DamageText text = instance.GetComponent<DamageText>();
        text.OnRelease = texts.Release;
        return text;
    }
    public void getText(DamageText DamageText)
    {
        var TMP_text = DamageText.GetComponent<TMP_Text>();
        TMP_text.color = TextColor;
        TMP_text.text = TextString;
        DamageText.transform.position = TextPosition;
        DamageText.gameObject.SetActive(true);
    }
    public void releaseText(DamageText DamageText)
    {
        Debug.Log("Released");
        DamageText.gameObject.SetActive(false);
    }
    public void DestroyText(DamageText DamageText)
    {
        Destroy(DamageText.gameObject);
    }
    public void ShowDamageText(float value, DMG_Text color, Transform parent)
    {
        TextPosition = Camera.main.WorldToScreenPoint((Vector2)parent.position + Random.insideUnitCircle * OffsetMultiplier);
        TextString = ((int)value).ToString();
        Debug.Log(TextVariants[0].type.Equals(color));
        TextColor = TextVariants.Find(x => x.type.Equals(color)).color;
        texts.Get();
    }
}
