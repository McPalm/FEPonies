public interface Buff{
	// dont forget to remove from the buff manager when the buff is destroyed. presumably when a new level is loaded.

	bool Affects(Unit u);

	Stats Stats{
		get;
	}
}
