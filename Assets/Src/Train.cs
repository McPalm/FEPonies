using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class Train : MonoBehaviour, IEnumerable<Item> {


	private List<Item> items;
	private int cash;

	public IEnumerator<Item> GetEnumerator()
	{
		return items.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return items.GetEnumerator();
	}

	/// <summary>
	/// Add this item ot the Supply Train
	/// </summary>
	/// <param name="i"></param>
	/// <returns></returns>
	public bool Add(Item i)
	{
		throw new System.NotImplementedException();
	}

	/// <summary>
	/// Remove an item from the supply train.
	/// </summary>
	/// <param name="i"></param>
	/// <param name="quantity">number of items remove from a stack, default is 1</param>
	/// <returns></returns>
	public bool Retrieve(Item i, int quantity = 1)
	{
		throw new System.NotImplementedException();
	}

	/// <summary>
	/// Monetary resrouces
	/// </summary>
	public int Cash
	{
		get { return cash;}
		set { cash = value;}
	}
}
