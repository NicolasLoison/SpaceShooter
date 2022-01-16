using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBossHpUI : MonoBehaviour
{
    public Slider hpSlider;

    public Vector3 offset;

    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<Camera>();
        hpSlider.gameObject.SetActive(true);
        hpSlider.maxValue = BossHealth.Instance.hp;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.transform.position = _camera.WorldToScreenPoint(transform.parent.position);
        hpSlider.value = BossHealth.Instance.hp;
    }
}
