//------------------------------------------------------------------------------
// <auto-generated>
//     Denna kod har genererats av ett verktyg.
//     Körtidsversion:4.0.30319.34014
//
//     Ändringar i denna fil kan orsaka fel och kommer att förloras om
//     koden återgenereras.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

public interface Observable
{
	//protected HashSet<Observer> observerCollection; //will need one of these
	void registerObserver(Observer obs);
	void unregisterObserver(Observer obs);
	// Make damned sure to wrap every notification sent with a try{...}, or you might end up with the most random ass bugs.
	void notifyObservers();
}
