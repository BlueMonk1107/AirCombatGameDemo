using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineController
{
	private static int _id;
	public int ID { get; private set; }
	private CoroutineItem _item;
	private MonoBehaviour _mono;
	private IEnumerator _routine;
	private Coroutine _coroutine;

	public CoroutineController(MonoBehaviour mono,IEnumerator routine)
	{
		_item = new CoroutineItem();
		_mono = mono;
		_routine = routine;
		ResetData();
	}

	public void Start()
	{
		_item.State = CoroutineItem.CoroutineState.RUNNING;
		_coroutine = _mono.StartCoroutine(_item.Body(_routine));
	}

	public void Pause()
	{
		_item.State = CoroutineItem.CoroutineState.PASUED;
	}

	public void Stop()
	{
		_item.State = CoroutineItem.CoroutineState.STOP;
	}

	public void Continue()
	{
		_item.State = CoroutineItem.CoroutineState.RUNNING;
	}

	public void ReStart()
	{
		if(_coroutine != null)
			_mono.StopCoroutine(_coroutine);

		Start();
	}

	private void ResetData()
	{
		ID = _id++;
	}
}
