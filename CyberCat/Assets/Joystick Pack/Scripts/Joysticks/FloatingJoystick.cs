using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
	private void PressEvent()
	{
			background.anchoredPosition = ScreenPointToAnchoredPosition(GameManager.Instance.pressPosition);
			background.gameObject.SetActive(true);
	}

	protected override void Start()
    {
        base.Start();

		background.gameObject.SetActive(false);
		GameManager.Instance.OnPressEvent += PressEvent;
		//GameManager.Instance.ReleaseEvent += ReleseEvent;
	}

    public override void OnPointerUp(PointerEventData eventData)
    {
		background.gameObject.transform.GetChild(0).gameObject.SetActive(false);
		background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}