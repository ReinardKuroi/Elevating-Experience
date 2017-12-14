using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ObjectController : MonoBehaviour{

	protected Subject subject = new Subject ();

	public abstract void OnClick ();

}