using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    [SerializeField] public float LifeTime;
    [SerializeField] public float MovingDistance;

    private void Start()
    {
        transform.DOMoveY(transform.position.y + MovingDistance, LifeTime * 0.8f);
    }

    private void Update()
    {
        if(LifeTime > 0)
        {
            LifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
