using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBox : MonoBehaviour
{
    private bool isActivated = true;

    BoxCollider2D _collider2D;
    SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //_collider2D = GameObject.FindGameObjectWithTag("MagnetTransform").GetComponent<BoxCollider2D>();
        _collider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isActivated = !isActivated;

            _collider2D.enabled = isActivated;
            _spriteRenderer.enabled = isActivated;
        }
    }
}
