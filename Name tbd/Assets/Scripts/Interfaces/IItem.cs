using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem: MonoBehaviour
{

	void Ability (IPlayer player);

	int Duration();

}

