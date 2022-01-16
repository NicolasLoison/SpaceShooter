using UnityEngine;

public class MoveShoot : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 _botRightCorner;

    public int speed;
    private Vector3 _size;

    private Vector3 _velocity = Vector3.zero;
    public float tauxDropHp;

    // Start is called before the first frame update
    private void Start()
    {
        if (Camera.main is { }) _botRightCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
    }

    // Update is called once per frame
    private void Update()
    {
        _size.x = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        Vector3 pos = transform.position;
        if (pos.x > _botRightCorner.x)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(speed,0);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _velocity, 0.05f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Asteriod"))
        {
            Destroy(gameObject);
            GameManager.Instance.AddScorePlayer(1);
            SoundManager.Instance.AsteriodExplosion();
            collision.gameObject.AddComponent<FadeOut>();
            MoveAsteriod.CreateAsteriod();
            Destroy(collision);
        }
        if (collision.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.Instance.AddScorePlayer(5);
            SoundManager.Instance.AsteriodExplosion();
            Instantiate(Resources.Load("Impact03"), transform.position, Quaternion.identity);
            BonusSpawner.Instance.SpawnHealthBonus(tauxDropHp, gameObject);
        }
        if (collision.transform.CompareTag("Boss"))
        {
            Destroy(gameObject);
            BossHealth.Instance.TakeDamage(10);
            SoundManager.Instance.AsteriodExplosion();
            Instantiate(Resources.Load("Impact03"), transform.position, Quaternion.identity);
        }
    }
}
