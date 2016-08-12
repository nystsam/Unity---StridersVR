using UnityEngine;
using System;

namespace StridersVR.Domain
{
	public interface ITouchBoard
	{
		void startAction();

		void cancelAction();

		bool actionComplete();
	}
}

