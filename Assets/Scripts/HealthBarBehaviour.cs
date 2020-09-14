using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    [SerializeField]
    Vector3 offset = new Vector3(0, 1, 0);
    public Slider hpSlider;
    public Image sliderImage;
    [System.NonSerialized]
    public Transform owner;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = owner.position + offset;
    }
}
