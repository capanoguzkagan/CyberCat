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
		GameManager.Instance.OnPressEvent += PressEvent;
		background.gameObject.SetActive(false);
	}
	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);
	}


	public override void OnPointerUp(PointerEventData eventData)
    {
		background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}