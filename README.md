# Decorator Design Pattern

The Decorator design pattern attaches additional responsibilities to an object dynamically. This pattern provide a flexible alternative to subclassing for extending functionality.

## UML class diagram

A visualization of the classes and objects participating in this pattern.
[<img src="https://github.com/felipefabiani/csharp-design-patterns/blob/structural/decorator/images/decorator.png" align="center" width="40%">](Test)

## Participants
The classes and objects participating in this pattern include:

- Component   (LibraryItem)
  > defines the interface for objects that can have responsibilities added to them dynamically.
- ConcreteComponent   (Book, Video)
  > defines an object to which additional responsibilities can be attached.
- Decorator   (Decorator)
  > maintains a reference to a Component object and defines an interface that conforms to Component's interface.
- ConcreteDecorator   (Borrowable)
  > adds responsibilities to the component.
