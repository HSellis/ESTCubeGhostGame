using UnityEngine;
using DG.Tweening;
public class Meteorite_On_Spawn : MonoBehaviour
{
    public Rigidbody rb;
    public float RotationSpeed = 1f;
    public float MoveSpeed = 5f;
    public float scale = 20f;
    public Vector3 RotationAxis = new Vector3(0f, 0f, 1f);
    
    private float StartScale=0f;
    public float LifeTime = 2f;

    private bool IsScalingDown = false;
  

    void Start()
    {
        scale = Random.Range(15f, 30f);
        MoveSpeed = Random.Range(2f, 7f);
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
        if (LifeTime <= 0 and !IsScalingDown)
        {   
            IsScalingDown = true;
            transform.DOLocalScale(new Vector3(0f, 0f, 0f), 1f).OnComplete(() => Destroy(gameObject));
        }

    }
}
