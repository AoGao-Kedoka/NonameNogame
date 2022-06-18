using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private RectTransform fill;
    [SerializeField] private float timeToOverride = 0.0005f;
    [SerializeField] private Animator animator;

    private bool _notFilled = true;
    private float _origX;
    
    // Start is called before the first frame update
    void Start()
    {
        _origX = fill.offsetMax.x;
        StartCoroutine(Override());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Override()
    {
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Override");
        yield return new WaitForSeconds(0.1f);
        while (_notFilled)
        {
            Debug.Log(fill.offsetMax.x);
            fill.offsetMax = new Vector2(fill.offsetMax.x + 1f, fill.offsetMax.y);
            yield return new WaitForSeconds(timeToOverride);
            if (fill.offsetMax.x >= -1f)
                _notFilled = false;
        }
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("GoOut");
        yield return new WaitForSeconds(0.15f);
        fill.offsetMax = new Vector2(_origX, fill.offsetMax.y);
    }
}
