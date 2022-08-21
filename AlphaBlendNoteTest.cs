using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaBlendNoteTest : MonoBehaviour
{
    public SpriteRenderer[] rendererre;
    public float a;



    public void Fade()
    {
        GameObject obj = new GameObject("Fade");
        obj.transform.SetParent(rendererre[0].transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0);
        spriteRenderer.sprite = rendererre[1].sprite;
        spriteRenderer.sortingOrder -= 1;

        LeanTween.value(-0.5f, 0.5f, 1).setOnUpdate((float f) =>
        {
            rendererre[0].color = new Color(1, 1, 1, -f + 0.5f);
            spriteRenderer.color = new Color(1, 1, 1, f + 0.5f);
        }).setOnComplete(() =>
        {
            Destroy(obj);
            rendererre[0].color = new Color(1, 1, 1, 1);
        }).setEaseInOutExpo();
    }
}
