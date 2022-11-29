using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed;
    private int damage;
    private bool isFly;
    private Vector3 startPosition;
    private Vector2 direction;
    private LayerMask allyLayer;
    
    private void Update()
   {
       if (isFly)
       {
           transform.Translate(direction * (speed * Time.deltaTime));
           if(Vector3.Distance(startPosition, transform.position) > 100) Hit();
       }
   }

   public void Shot(bool condition, int damage, LayerMask layer)
   {
       allyLayer = layer;
       startPosition = transform.position;
       direction = condition ? Vector2.left : Vector2.right;
       spriteRenderer.flipX = !condition;
       isFly = true;
       this.damage = damage;
       gameObject.SetActive(true);
   }
   
   private void OnTriggerEnter2D(Collider2D col)
   {
       if (!isFly || col.gameObject.layer == allyLayer) return;
       if (HealthManager.Instance.ContainsKey(col.gameObject))
       {
           var health = HealthManager.Instance.TakeInstance(col.gameObject);
           health.TakeDamage(damage);
       }
       Hit();
   }

   private void Hit()
   {
       isFly = false;
       gameObject.SetActive(false);
   }
}
