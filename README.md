# Specification

## Intent

Specification pattern separates the statement of how to match a candidate, from the candidate object that it is matched against. As well as its usefulness in selection, it is also valuable for validation and for building to order.
In short, the main benefit of using “specifications” is a possibility to have all the rules for filtering domain model objects in one place, instead of a thousand of lambda expressions spread across an application.

## Problem

Separate the search criteria from the object that performs the search.

## Structure

![enter image description here](https://github.com/felipefabiani/csharp-design-patterns/blob/behavioral/specification/Images/Specification.png)
