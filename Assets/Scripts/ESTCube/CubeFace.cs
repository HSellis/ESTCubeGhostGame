using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeFace : MonoBehaviour
{
    private Rigidbody rb;
    private Transform originalParent;
    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;
    private Vector3 originalLocalScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalParent = transform.parent;
        originalLocalPosition = transform.localPosition;
        originalLocalRotation = transform.localRotation;
        originalLocalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void Detach()
    {
        rb.isKinematic = false;
        transform.parent = null;
    }

    public void Attach()
    {
        rb.isKinematic = true;
        transform.parent = originalParent;
        transform.localPosition = originalLocalPosition;
        transform.localRotation = originalLocalRotation;

        transform.localScale = Vector3.one * 0.01f;
        transform.DOScale(originalLocalScale, 0.5f);
    }
}
