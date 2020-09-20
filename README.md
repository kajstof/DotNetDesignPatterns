# DotNetDesignPatterns
Design patterns with C#

## SOLID Design Principles

* Single-responsibility principle
* Open–closed principle
* Liskov substitution principle
* Interface segregation principle
* Dependency inversion principle

## Creational

* Builder - When construction gets a little bit to complicated
  * Motivation
    * Some objects are simple and can be created in a single construct call
    * Other objects require a lot of ceremony to create
    * Having an object with 10 constructor arguments is not productive
    * Instead, opt for piecewise construction
    * Builder provides an API for constructing an object step-by-step
  * When piecewise object construction is complicated, provide an API for doing it succinctly
  * Summary
    * A builder is a separate component for building an object
    * Can either give builder a constructor or return it via a static function
    * To make builder fluent, return this
    * Different facets of an object can be build with different builders working in tandem via a base class
  * My notes
    * Fluent Builder
    * Fluent Builder with Recursive Generics
    * Faceted Builder
* Factories - Factory Method and Abstract Factory
  * Motivation
    * Object creation logic becomes too convoluted
    * Constructor is not descriptive
      * Name mandated by name of containing type
      * Cannot overload wit same sets of arguments with different names
      * Can turn into 'optional parameter hell'
    * Object creation (non-piecewise, unlike Builder) can be outsourced to
      * A separate function (Factory Method)
      * That may exist in a separate class (Factory)
      * Can create hierarchy of factories with Abstract Factory
    * A component responsible solely for the wholesale (not piecewise) creation of objects
  * Summary
    * A factory method is a static method that creates objects
    * A factory can take care of object creation
    * A factory can be external or reside inside the object as an inner class
    * Hierarchies of factories can be used to create related objects
  * My notes
    * Factory Method
    * Factory
    * Abstract Factory
* Prototype - When it's easier to copy an existing object to full initialize a new one
  * Motivation
    * Complicated objects (e.g., cars) aren't designed from scratch
      * They reiterate existing designs
    * An existing (partially or fully constructed) design is a Prototype
    * We make a copy (clone) the prototype and customize it
      * Requires 'deep copy' support
    * We make the cloning convenient (e.g., via a Factory)
  * A partially or fully initialized object that you copy (clone) and make use of
  * Summary
    * To implement a prototype, partially construct an object and store it somewhere
    * Clone the prototype
      * Implement your own deep copy functionality; or
      * Serialize and deserialize
    * Customize the resulting instance
  * My notes
    * ICloneable is bad
    * Copy constructor (c++ term)
    * Explicit Deep Copy Interface
    * Copy through Serialization
* Singleton - A design pattern everyone loves to hate...
  * Motivation
    * For some components it only makes sense to have one in the system
      * Database repository
      * Object factory
    * E.g., the constructor call is expensive
      * We only do it once
      * We provide everyone with the same instance
    * Want to prevent anyone creating additional copies
    * Need to take care of lazy instantiation and thread safety
  * A component which is instantiated only once
  * Summary
    * Making a 'safe' singleton is easy: construct a static Lazy<T> and return its Value
    * Singletons are difficult to test
    * Instead of directly using a singleton, consider depending on an abstraction (e.g., an interface)
    * Consider defining singleton lifetime in DI container
  * My notes
    * Testability issues
    * Use with dependency injection
    * Monostate (static fields and properties in non static class)

## Structural

* Adapter - Getting the interface you want from the interface you have
  * A construct which adapts an existing interface X to conform to the required interface Y
  * Summary
    * Implementing an Adapter is easy
    * Determine the API you have and the API you need
    * Create a component which aggregates (has a reference to, ...) the adaptee
    * Intermediate representations can pile up use caching and other optimizations
  * My notes
    * Converts from eg. interface X to interface Y
    * Caching
* Bridge - Connecting components together through abstractions
  * Motivation
    * Bridge prevents a 'Cartesian product' complexity explosion
    * Example
      * Base class ThreadScheduler
      * Can be preemptive or cooperative
      * Can run on Windows or Unix
      * End up with a 2x2 scenario: WindowsPTS, UnixPTS, WindowsCTS, UnixCTS
    * Bridge pattern avoids the entity explosion
  * A mechanism that decouples an interface (hierarchy) from an implementation (hierarchy)
  * Summary
    * Decouple abstraction from implementation
    * Both can exists as hierarchies
    * A stronger form of encapsulation
  * My notes
    * Injects eg. interface and class use this interface to generating results
* Composite - Treating individual and aggregate objects uniformly
  * Motivation
    * Objects use other objects' fields/properties/members through inheritance and composition
    * Composition lets us make compound objects
      * E.g., a mathematical expression composed of simple expressions; or
      * A grouping of shapes that consists of several shapes
    * Composite design pattern is used to treat both single (scalar) and composite objects uniformly
      * I.e., Foo and Collection<Foo> have common APIs
    * A mechanism for treating individual (scalar) objects and compositions of objects in a uniform manner
  * Summary
    * Objects can use other objects via inheritance/composition
    * Some composed and singular objects need similar/identical behaviors
    * Composite design pattern lets us treat both types of objects uniformly
    * C# has special support for the *enumeration* concept
    * A single object can masquerade as a collection with *yield return this;*
  * My notes
    * Composition of the self type
    * Treats single objects and collections in the same manner (API)
    * GetEnumerator with yield return this; + Extension Method
* Decorator - Adding behavior without altering the class itself
  * Motivation
    * Want to augment an object with additional functionality
    * Do not want to rewrite or alter existing code (OCP)
    * Want to keep new functionality separate (SRP)
    * Need to be able to interact with existing structures
    * Two options
      * Inherit from required object if possible; some objects are sealed
      * Build a Decorator, which simply references the decorated object(s)
    * Facilitates the addition of behaviors to individual objects without inheriting from them
  * Summary
    * A decorator keeps the reference to the decorated object(s)
    * May or may not proxy over calls
      * Use R# Generate Delegated Members
    * Exists in a *static* variation
      * X<Y<Foo>>
      * Very limited due to inability to inherit from type parameters
  * My notes
    * Add more methods to existing API (when inheritance is not allowed)
    * Adapter/Decorator
    * Multiple Inheritance
    * Dynamic decorator composition & static decorator composition X<Y<Foo>>
* Facade - Exposing several components through a single interface
  * Motivation (in house example)
    * Balancing complexity and presentation/usability
    * Typical home
      * Many subsystems (electrical, sanitation)
      * Complex internal structure (e.g., floor layers)
      * End user is not exposed to internals
    * Same with software!
      * Many systems working to provide flexibility, but...
      * API consumers want it to 'just work'
  * Provide a simple, easy to understand/user interface over a large and sophisticated body of code
  * Summary
    * Build a Facade to provide a simplified API over a set of classes
    * May wish to (optionally) expose internals through the facade
    * May allow users to 'escalate' to use more complex APIs if they need to
  * My notes
    * Provide a simplified API over a set of classes or indeed if you want to take a bigger set of subsystem
* Flyweight - Space optimization
  * Motivation
    * Avoid redundancy when storing data
    * E.g., MMORPG
      * Plenty of users with identical first/last names
      * No sense in storing same first/last name over and over again
      * Store a list of names and pointers to them
    * .NET performs string interning, so an identical string is stored only once
    * E.g., bold or italic text in the console
      * Don't want each character to have a formatting character
      * Operate on *ranges* (e.g., line number, start/end positions)
  * A space optimization technique that lets us use less memory by storing externally the data associated with similar objects
  * Summary
    * Store common data externally
    * Define the idea of 'ranges' on homogeneous collections and store data related to those ranges
    * .NET string interning *is* the Flyweight pattern
  * My notes
    * Avoid redundancy when storing data
    * Minimize data stored - extend data (eg. define "ranges" on homogeneous collections and store data related to those ranges
    * .Net string interning is the Flyweight pattern
* Proxy - An interface for accessing a particular resource
  * Motivation
    * You are calling *foo.Bar()*
    * This assumes that *foo* is in the same process as *Bar()*
    * What if, later on, you want to put all Foo-related operations into a separate process
      * Can you avoid changing your code?
    * Proxy to the rescue!
      * Same interface, entirely different behavior
    * This is called a communication proxy
      * Other types: logging, virtual, guarding, ...
  * A class that functions as an interface to a particular resource. That resource may be remote, expensive to construct, or may require logging or some other added functionality
  * Summary
    * A proxy has the same interface as the underlying object
    * To create a proxy, simply replicate the existing interface of an object
    * Add relevant functionality redefined member functions
    * Different proxies (communication, logging, caching, etc.) have completely different behaviors
  * My notes
    * Providing an interface for accessing and particular resource by essentially replicating that interface
    * A class that functions as an interface to a particular resource. That resource may be remote, expensive to construct, or may require logging or some other added functionality
    * Scenarios:
      * Protection Proxy - checking for permissions to object
      * Property Proxy - checking for assignment (if yet the same value)
      * Dynamic Proxy - Eg. Logging additional information of calling/etc.
    * Proxy vs Decorator
      * Identical interface vs enhanced interface
      * Decorator typically aggregates (or has reference to) what it is decorating; proxy doesn't have to)
      * Proxy might not even be working with a materialized object
    * Different proxies (communication, logging, caching, etc.) have completely different behaviors

## Behavioral

* Chain of Responsibility - Sequence of handlers processing an event one after another
  * Motivation
    * Unethical behavior by an employee; who takes the blame?
      * Employee
      * Manager
      * CEO
    * You click a graphical element on a form
      * Button handles it, stops further processing
      * Underlying group box
      * Underlying window
    * CCG computer game
      * Creature has attack and defense values
      * Those can be boosted by other cards
  * A chain of components who all get a chance to process a command or a query, optionally having default processing implementation and an ability to terminate the processing chain
  * Summary
    * Chain of Responsibility can be implemented as a chain of references or a centralized construct
    * Enlist objects in the chain possibly controlling their order
    * Object removal from chain (e.g., in *Dispose()*)
  * My notes
    * A chain of components who all get a chance to process a command or a query, optionally having default processing implementation and an ability to terminate the processing chain
    * CQS - Command Query Separation
    * Mediator + Event broker
    * Can be implemented as a chain of references or a centralized construct (list/linked list)
    * Enlist objects in the chain, possibly controlling their order
    * Object removal from chain (e.g. in Dispose())
* Command - You shall not pass
  * Motivation
    * Ordinary C# statements are perishable
      * Cannot undo a field/property assignment
      * Cannot directly serialize a sequence of actions (calls)
    * Want an object that represents an operation
      * X should change its property Y to Z
      * X should do W()
    * Uses: GUI commands, multi-level undo/redo, macro recording and more!
  * An object which represents an instruction to perform a particular action. Contains all the information necessary for the action to be taken
  * Summary
    * Encapsulate all details of an operation in a separate object
    * Define instruction for applying the command (either in the command itself, or elsewhere)
    * Optionally define instructions for undoing the command
    * Can create composite commands (a.k.a. macros)
  * My notes
    * Ordinary C# statements are perishable
      * Cannot undo a field/property assignment
      * Cannot directly serialize a sequence of actions (calls)
    * Want an object that represents an operation
      * X should change its property Y to Z
      * X should do W()
    * Encapsulate all details of an operation in a separate object
    * Define instruction for applying the command (either in the command itself, or elsewhere)
    * Optionally define instructions for undoing the command
    * Can create composite commands (a.k.a. macros)
* Interpreter - Interpreters are all around us. Even now, in this very room.
  * Motivation
    * Textual input need to be processed
      * E.g., turned into OOP structure
    * Some examples
      * Programming language compilers, interpreters and IDEs
      * HTML, XML and similar
      * Numeric expressions (3+4/5)
      * Regular expressions
    * Turning strings into OOP based structures in a complicated process
  * A component that processes structured text data. Does so by turning it into separate lexical tokens (*lexing) and then interpreting sequences of said tokens (*parsing*)
  * Summary
    * Barring simple cases an interpreter acts in two stages
    * Lexing turns text into a set of tokens, e.g.
      `3*(4+5) -> Lit[3] Star Lparen Lit[4] Plus Lit[5] Rparen`
    * Parsing tokens into meaningful constructs
      `-> MultiplicationExpression[Integer[3], AdditionExpression[Integer[4], Integer[5]]]`
    * Parsed data can then be traversed
  * My notes
    * Textual input needs to be processed
      * E.g., turned into OOP structures
    * Some examples
      * Programming language compilers, interpreters and IDEs
      * HTML, XML and similar
      * Numeric expressions (3+4/5)
      * Regular expressions
    * Turning strings into OOP based structures in a complicated process
    * A component that processes structured text data. Does so by turning it into separate lexical tokens (lexing) and then interpreting sequences of said tokens (parsing)
    * ANTLR - Another Tool for Language Recognition http://antlr.org
* Iterator - How traversal of data structures happens and who makes it happen
  * Motivation
    * Iteration (traversal) is a core functionality of various data structures
    * An *iterator* is a class that facilitates the traversal
      * Keeps a reference to the current element
      * Knows how to move to a different element
    * Iterator is an implicit construct
      * .Net builds a state machine around your *yield return* statements
  * An object (or, in .NET, a method) that facilitates the traversal of a data structure
  * Summary
    * An iterator specified how you can traverse an object
    * An iterator object, unlike a method, cannot be recursive
    * Generally, an IEnumerable<T> returning method is enough
    * Iteration works through *duck typing* - you need a GetEnumerator() that yields a type that has Current and MoveNext()
* Mediator - Facilitates communication between components
  * Motivation
    * Components may go in and out of a system at any time
      * Chat room participants
      * Players in an MMORPG
    * It makes no sense for them to have direct references to on another
      * Those references may go dead
    * Solution: have then all refer to some central component that facilitates communication
  * A component that facilitates communication between other components without them necessarily being aware of each other or having direct (reference) access to each other
  * Summary
    * Create the mediator and have each object in the system refer to it
      * E.g., in a field
    * Mediator engages in bidirectional communication with its connected components
    * Mediator has functions the components can call
    * Components have functions the mediator can call
    * Event processing (e.g., Rx) libraries make communication easier to implement
* Memento - Keep a memento of an object's state to return to that state
  * Motivation
    * An object or system goes through changes
      * E.g. a bank account gets deposits and withdrawals
    * There are different ways of navigating those changes
    * One way is to record every change (Command) and teach a command to 'undo' itself
    * Another is to simply save snapshot of the system
  * A token/handle representing the system state. Lets us roll back to the state when the token was generated. May or may not directly expose state information.Me
  * Summary
    * Mementos are used to roll back states arbitrarily
    * A memento is simply a token/handle class with (typically) no functions of its own
    * A memento is not required to expose directly the state(s) to which i reverts the system
    * Can be used to implement undo/redo
* Null Object - A behavioral design pattern with no behaviors
  * Motivation
    * When component A uses component B, it typically assumes that B is non-null
      * You inject B, not B? or some Option<B>
      * You do not check for null (?.) on every call
    * There is no option of telling A *not* to use an instance of B
      * Its use is hard-coded
    * Thus, we build a no-op, non-functioning inheritor of B and pass it into A
  * A no-op object that conforms to the required interface, satisfying a dependency requirement of some other object
  * Summary
    * Implement the required interface
    * Rewrite the methods with empty bodies
      * If method is non-void, return default(T)
      * If these values are ever used, you are in trouble
    * Supply an instance of Null Object in place of actual object
    * Dynamic construction possible
      * With associated performance implications
* Observer - Build-in right into C#/.NET, right?
  * Motivation
    * We need to be informed when certain things happen
      * Object's property changes
      * Object does something
      * Some external even occurs
    * We want to listen to events and notified when they occur
    * Build into C# with the *event* keyword
      * But then what is this IObservable<T> / IObserver<T> for?
      * What about INotifyPropertyChanging/Changed?
      * And what are BindingList<T>/ObservableCollection<T>?
    * An *observer* is an object that wishes to be informed about events happening in the system. The entity generating the events is an *observable*
    * Summary
      * Observer is an intrusive approach: an observable must provide an event to subscribe to
      * Special care must be taken to prevent issues in multithreaded scenerios
      * .NET comes with observable collections
      * IObserver<T>/IObservable<T> are used in stream processing (Reactive Extensions)
