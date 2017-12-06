using UnityEngine;
using UnityEngine.UI;

public class AlphaPingPong : MonoBehaviour
{
	public Text component;
	public float speed = .5f;
	
	private Color c;
	private float timer = 0;

	void Start ()
	{
		if (component.IsNullOrEmpty())
		{
			this.enabled = false;
			return;
		}

		c = component.color;
		c.a = 0f;
	}

	void OnDisable ()
	{
		c.a = 0f;
		timer = 0f;
		component.color = c;
	}

	void FixedUpdate ()
	{
		c.a = Mathf.PingPong(timer * speed, 1);
		timer += Time.deltaTime;
		component.color = c;
	}
}
