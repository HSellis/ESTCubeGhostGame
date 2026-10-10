using UnityEngine;
using DG.Tweening;
public class Meteorite : MonoBehaviour
{
    public Rigidbody rb;
    public float RotationSpeed = 1f;
    private float MoveSpeed = 5f;
    public float MoveSpeedLower = 2f;
    public float MoveSpeedUpper = 7f;
    private float scale = 20f;
    public float scaleLower = 15f;
    public float scaleUpper = 30f;
    public Vector3 RotationAxis = new Vector3(0f, 0f, 1f);
    
    private float StartScale=0f;
    private float LifeTime = 12f;

    private bool IsScalingDown = false;
  

    void Start()
    {
        scale = Random.Range(scaleLower, scaleUpper);
        MoveSpeed = Random.Range(MoveSpeedLower, MoveSpeedUpper);
        RotationSpeed = Random.Range(20f, 300f);
        RotationAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.linearVelocity = new Vector3(-1f, 0f, 0f) * MoveSpeed;
        transform.localScale = new Vector3(0f,0f,0f);
        transform.Rotate(RotationAxis * RotationSpeed * Time.deltaTime);
    }
    void Update()
    {
        if (StartScale < scale)
        {
            StartScale += Time.deltaTime * 10f;
            transform.localScale = new Vector3(Mathf.Clamp(StartScale, 0f, scale), Mathf.Clamp(StartScale, 0f, scale), Mathf.Clamp(StartScale, 0f, scale));
        }
        //transform.position += new Vector3(1f, 0f, 0f) * MoveSpeed * Time.deltaTime;
        


        LifeTime -= Time.deltaTime * 1f;
        if ((LifeTime <= 0) & !IsScalingDown)
        {   
            IsScalingDown = true;
            transform.DOScale(new Vector3(0f, 0f, 0f), 2f).OnComplete(() => Destroy(gameObject));
        }

    }
}
