using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	public float InteractionRadius = 1;

	private Transform playerTransform;

	protected virtual void Start()
	{
		playerTransform = GameObject.Find("Player").transform;
	}

	protected virtual void FixedUpdate()
    {
		var distance = Vector2.Distance(transform.position, playerTransform.position);
		if (distance < InteractionRadius) {
			Interact();
		}
	}

	public virtual void Interact()
	{
		Debug.Log($"Interacted with {gameObject.name}");
	}
}
