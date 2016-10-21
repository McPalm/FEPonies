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
        if (i != null)
        {
            items.Add(i);
        }
        return true;
	}

	/// <summary>
	/// Remove an item from the supply train.
	/// </summary>
	/// <param name="i"></param>
	/// <param name="quantity">number of items remove from a stack, default is 1</param>
	/// <returns></returns>
	public bool Retrieve(Item i, int quantity = 1)
	{
		for (int n=0; n<quantity; n++)
        {
            if(items.Contains(i))
            {
                items.Remove(i);
            }
            else
            {
                Debug.Log("Not enough items here, something went wrong");
                return false;
            }
        }
        return true;
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
