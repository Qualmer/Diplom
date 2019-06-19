using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	public float InteractionRadius = 1;
	private Transform playerTransform;

	protected virtual void Start()
	{
		playerTransform = FindObjectOfType<Player>().transform;
	}

	protected virtual void FixedUpdate()
    {
		var distance = Vector2.Distance(transform.position, playerTransform.position);
		if (distance < InteractionRadius && Input.GetButtonDown("Interaction")) {
			Interact();
		}
	}

	public virtual void Interact()
	{
		Debug.Log($"Взаимодействие с {gameObject.name}");
	}
}
