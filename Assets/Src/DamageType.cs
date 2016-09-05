
/// <summary>
/// Damage type.
/// Contains a number of flags with information on damagetypes
/// Get a flag using the properties such as ArmourPiercing to si if the damagetype is armourpiercing.
/// contains a number of constants that can be used in conjuction with the constructor.
/// </summary>
public struct DamageType{

	private int flags;

	public const int ANTI_AIR = 1;
	public const int ARMOUR_PIERCING = 2;
	public const int MAGE_SLAYER = 4;
	public const int MAGIC = 8;
	public const int CRITICAL = 16;
	public const int TRUE = 32;
	public const int POISON = 64;
	public const int HALVED = 128;

	/// <summary>
	/// If this DamageType has no special type (all the other properties are false) then this will return true.
	/// </summary>
	/// <value><c>true</c> if normal; otherwise, <c>false</c>.</value>
	public bool Normal{
		get{return flags == 0;}
	}

	public bool AntiAir{
		set{
			if(value && flags%2 == 0) flags++;
			else if(!value && flags%2 == 1) flags--;
		}
		get{return flags%2 == 1;}
	}

	public bool ArmourPiercing{
		set{
			if(value && (flags >> 1) %2 == 0) flags+= 2;
			else if(!value && (flags >> 1) %2 == 1) flags-= 2;
		}
		get{return (flags >> 1) %2 == 1;}
	}

	public bool MageSlayer{
		set{
			if(value && (flags >> 2) %2 == 0) flags+= 4;
			else if(!value && (flags >> 2) %2 == 1) flags-= 4;
		}
		get{return (flags >> 2) %2 == 1;}
	}

	public bool Magic{
		set{
			if(value && (flags >> 3) %2 == 0) flags+= 8;
			else if(!value && (flags >> 3) %2 == 1) flags-= 8;
		}
		get{return (flags >> 3) %2 == 1;}

	}

	public bool Critical{
		set{
			if(value && (flags >> 4) %2 == 0) flags+= 16;
			else if(!value && (flags >> 4) %2 == 1) flags-= 16;
		}
		get{return (flags >> 4) %2 == 1;}
	}

	public bool True{
		set{
			if(value && (flags >> 5) %2 == 0) flags+= 32;
			else if(!value && (flags >> 5) %2 == 1) flags-= 32;
		}
		get{return (flags >> 5) %2 == 1;}
	}

	public bool Poison{
		set{
			if(value && (flags >> 6) %2 == 0) flags+= 64;
			else if(!value && (flags >> 6) %2 == 1) flags-= 64;
		}
		get{return (flags >> 6) %2 == 1;}
	}

	public bool Halved{
		set{
			if(value && (flags >> 7) %2 == 0) flags+= HALVED;
			else if(!value && (flags >> 7) %2 == 1) flags-= HALVED;
		}
		get{return (flags >> 7) %2 == 1;}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="DamageType"/> struct.
	/// Make use of the constants in DamageType. To create a poison damage, create a new as such.
	/// new DamageType(DamageType.POISON)
	/// Can take multiple types, to make an armour piercing anti air attack do the following
	/// New DamageType(DamageType.ANTI_AIR, DamageType.ARMOUR_PIERCING)
	/// </summary>
	/// <param name="types">Types.</param>
	public DamageType (params int[] types)
	{
		flags = 0;
		foreach(int i in types){
			flags += i;
		}
	}

	
}
