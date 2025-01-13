using System.Numerics;

public interface IDamageable {
	
	public float Health{set; get; }
	public bool Tragetable{set; get; }
	public void OnHit(float damage, UnityEngine.Vector2 knockback);
	public void OnHit(float damage);
	public void OnObjectDestroyed();
	
}