Pseudo Fixture
==============

Automatic dummy object / fixture generation using random values, all the way down.

Purpose
-------

Sometimes your tests can get unwieldy because you need to pass concrete objects with real values to the subject.

Most of the time, though, you don't care about the actual values - you just want some values that you can test against in your assertions, or, worse yet, those values are required by the concrete class but aren't used in the test at all. The usual solution is to implement a dummy object which implements the concrete class without those dependencies.

It would be nice if, instead of implementing a class just for our tests, we could just populate a concrete class with some random values. This would have the added benefit of making some of your tests slightly more robust, since implementations with hard-coded values wouldn't pass, and you'd never get false positives from empty strings, zeroes, and other default values.

This project aims to provide an easy and semantically satisfying way to do that.

Usage
-----

For a class called Dude, which has a reference to a class called Like:

	public class Dude { 
		public virtual string Uhhhh { get; set; } 
		public virtual Like What { get; set; }
		public virtual bool Nope { get; set; }
	}
	
	public class Like {
		public virtual int Yeah { get; set; }
		public virtual Dude What { get; set; } // An infinite loop? No!
	}

We create a Pseudo like this:

	Dude erino = new Pseudo<Dude>().Create();

The variable will now return random values from all its value-type and string poperties:

	var gibberish = erino.Uhhhh; //a random string (but really just a random int.ToString())

Note that it doesn't generate a new random value each time you call the property, so:

	erino.Uhhhh == erino.Uhhhh; //true

And its other reference-type properties will themselves return new Pseudos:

	var nonsense = erino.What.Yeah; //a random int
	
Note that bools always return false:

	erino.Nope == false;

Everything is a dynamic proxy, and lazy, so having circular references is fine:

	var turtles = erino.What.What.What.What.What.What.What; //keep it up, jerk.

Current Limitations	/ Roadmap
-----------------------------

There's lots it doesn't do yet...

It will only do virtual properties so far.

It doesn't do methods yet.

It doesn't do interface or abstract return values yet.

It doesn't do arrays or other collections yet.

It doesn't let you supply non-random values or delegates to properties where you actually care yet.

So I have a ways to go before it's really useful, but I'll get there, I hope...