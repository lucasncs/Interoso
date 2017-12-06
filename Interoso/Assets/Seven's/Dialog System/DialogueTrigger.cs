using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public Dialogue dialogue;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			TriggerDialogue();
	}

	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
		Destroy(gameObject);
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