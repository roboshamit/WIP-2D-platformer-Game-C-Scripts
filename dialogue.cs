﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class dialogue
{
	public string name;

	[TextArea(0,10)]
	public string[] sentences;
}
