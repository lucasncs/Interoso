using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(CircleCollider2D))]
public class InteractionAction : MonoBehaviour
{
	public bool waitForInteraction;
	public bool useFade = false;
	public Color fadeColor = Color.white;
	public float fadeDamp = 3;
	public UnityEvent DoWhenInteract;

	private bool collide = false;

	//private Animator indicatorAnim;


	void Start()
	{
		//indicatorAnim = GetComponentInChildren<Animator>();
		//if (!waitForInteraction) indicatorAnim.gameObject.SetActive(false);
	}

	void Update()
	{
		if (collide)
		{
			//indicatorAnim.transform.position = FindObjectOfType<PlayerControl>().transform.position + new Vector3(0, 5); //faz o balao seguir a Dree

			if (waitForInteraction)
			{
				//indicatorAnim.SetBool("appear", true);
				if (Input.GetKeyUp(KeyCode.E)) //key de interacao
				{
					Execute();
					//indicatorAnim.SetBool("appear", false);
					//indicatorAnim.gameObject.SetActive(false);
					return;
				}
			}
			else
			{
				Execute();
			}
		}
	}

	void Execute()
	{
		if (useFade)
			Fade.Initialize(fadeColor, fadeDamp, delegate { /*if (DoWhenInteract.GetPersistentEventCount() > 0)*/ DoWhenInteract.Invoke(); });
		else
			/*if (DoWhenInteract.GetPersistentEventCount() > 0)*/ DoWhenInteract.Invoke();

		GetComponent<Collider2D>().enabled = collide = false;
	}


	public void OnTriggerEnter2D(Collider2D hit)
	{
		if (hit.gameObject.tag == "Player")
		{
			collide = true;
		}
	}

	public void OnTriggerExit2D(Collider2D hit)
	{
		if (hit.gameObject.tag == "Player")
		{
			collide = false;
			//indicatorAnim.SetBool("appear", false);
		}
	}

	void Reset()
	{
		Collider2D coll = GetComponent<Collider2D>();
		if (!coll) coll = gameObject.AddComponent<CircleCollider2D>();
		coll.isTrigger = true;

#if UNITY_EDITOR
		UnityEditorInternal.ComponentUtility.MoveComponentDown(this);
#endif
	}
}