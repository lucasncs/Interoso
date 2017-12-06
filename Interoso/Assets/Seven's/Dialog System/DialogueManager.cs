using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
	//public Text nameText;
	public Text dialogueText;
	public GameObject continueTxt;

	public Animator animator;

	private Queue<string> sentences;
	private string actualSentence;

	private UnityEvent OnDialogEnd;

	private bool isOpen;
	private Coroutine typing;
	private Coroutine closeByTime;

	void Start()
	{
		sentences = new Queue<string>();
		continueTxt.SetActive(false);
	}

	public void StartDialogue(Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);
		//Time.timeScale = 0;

		isOpen = true;

		//nameText.text = dialogue.name;
		OnDialogEnd = dialogue.OnDialogEnd;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		actualSentence = sentences.Dequeue();
		StopAllCoroutines();

		typing = StartCoroutine(TypeSentence(actualSentence));
		continueTxt.SetActive(false);
		closeByTime = this.Invoke(DisplayNextSentence, 10);
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
		continueTxt.SetActive(true);
	}

	private void Update()
	{
		if (isOpen)
		{
			if (Input.GetKeyDown(/*PC2D.Input.JUMP*/ KeyCode.Return))
			{
				if (dialogueText.text != actualSentence)
				{
					StopCoroutine(typing);
					dialogueText.text = actualSentence;
					continueTxt.SetActive(true);
				}
				else
					DisplayNextSentence();
			}
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		//Time.timeScale = 1;
		isOpen = false;

		if (OnDialogEnd.GetPersistentEventCount() > 0)
		{
			OnDialogEnd.Invoke();
		}
	}

}