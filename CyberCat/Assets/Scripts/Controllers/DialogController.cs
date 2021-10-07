using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogController : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _dialogText;
	[SerializeField] InputActionReference _buttonConfirm;
	[SerializeField] InputActionReference _buttonConfirmKeyboard;
	[SerializeField] GameObject _skipText;
	[SerializeField] string[] _sentences;
	[SerializeField] float _speedDialog;
	bool isPressed;

	private int _index = 0;
	[SerializeField] UnityEvent _finishEvent;
	private void OnEnable()
	{
		_buttonConfirm.action.Enable();
		_buttonConfirmKeyboard.action.Enable();
	}
	private void OnDisable()
	{
		_buttonConfirm.action.Disable();
		_buttonConfirmKeyboard.action.Disable();
	}
	private void Start()
	{
		NextSentence();
	}
	void Update()
    {
		if (_buttonConfirm.action.triggered || _buttonConfirmKeyboard.action.triggered)
		{
			NextSentence();
		}
        
    }
	private void NextSentence()
	{
		if (!isPressed)
		{
			if (_index <_sentences.Length)
			{
				_skipText.SetActive(false);
				_dialogText.text = "";
				StartCoroutine(WriteSentence());
				isPressed = true;
			}
			else if (_index == _sentences.Length)
			{
				_finishEvent?.Invoke();
			}
		}
		else
		{
			_speedDialog = 0;
		}
	}
	IEnumerator WriteSentence()
	{
		foreach (char Character in _sentences[_index].ToCharArray())
		{
			_dialogText.text += Character;
			yield return new WaitForSeconds(_speedDialog);
		}
		_index++;
		_skipText.SetActive(true);
		_speedDialog = 0.05f;
		isPressed = false;
	}
}
