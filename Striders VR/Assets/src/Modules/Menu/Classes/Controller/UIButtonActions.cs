using System;
using UnityEngine;

public interface UIButtonActions
{
	void buttonHover(bool isHitting);

	void buttonAction(GameObject menuOptions);

	bool buttonPressed();
}


