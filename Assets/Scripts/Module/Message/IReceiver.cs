using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceiver
{
	void ReceiveMessage(params object[] args);
}
