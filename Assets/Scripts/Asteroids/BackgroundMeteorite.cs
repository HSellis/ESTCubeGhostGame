using UnityEngine;
using DG.Tweening;
public class BackgroundMeteorite : MonoBehaviour
{
    public float RotationSpeed = 1f;
    public float MoveSpeed = 5f;
    public float scale = 20f;
    public Vector3 RotationAxis = new Vector3(0f, 0f, 1f);
    
    private float StartScale=0f;
    private float LifeTime = 7f;

    private bool IsScalingDown = false;
  

    void Start()
    {
        scale = Random.Range(200f, 400f);
        MoveSpeed = Random.Range(10f, 14f);
        RotationSpeed = Random.Range(20f, 300f);
        RotationAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
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
        transform.position += new Vector3(-1f, 0f, 1f) * MoveSpeed * Time.deltaTime;
        


        LifeTime -= Time.deltaTime * 1f;
        if ((LifeTime <= 0) & !IsScalingDown)
        {   
            IsScalingDown = true;
            transform.DOScale(new Vector3(0f, 0f, 0f), 2f).OnComplete(() => Destroy(gameObject));
        }

    }
}
