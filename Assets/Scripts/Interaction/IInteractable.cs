using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LGDP.Interaction
{
	public enum InteractionType
	{
		Manual,
		Trigger,
	}

	public interface IInteractable
	{
		InteractionType InteractionType { get; set; }
		void Activate();
	}
}