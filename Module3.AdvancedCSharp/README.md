# Advanced C#

## Home task:

### Task 1:

Create a class FileSystemVisitor, which allows you to traverse a tree of folders on the filesystem from the pre-defined folder.

Note: Please discuss with the mentor which presentation layer you will use before implementation (console or desktop application).

FileSystemVisitor class should implement the following functionality:

1. Return all found files and folders in the form of a linear sequence, for which implement your own iterator (using the yield operator if possible).
1. Provide the ability to set an algorithm for filtering found files and folders at the time of creating an instance of FileSystemVisitor (via a special overloaded constructor). The algorithm must be specified as a delegate/lambda.

---

### Task 2:

For solution from Task 1 add the following functionality:

1. Generate notifications (via the ‘event’ mechanism) about the stages of their work. In particular, the following events must be implemented (event names can be anything, it is important to follow the [naming convention](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-type-members#names-of-events)):

1. Start and Finish (for the beginning and end of the search).
1. FileFound / DirectoryFound for all found files and folders before filtering, and FilteredFileFound / FilteredDirectoryFound for filtered files and folders. These events should allow (by setting special flags in the parameter):

   - Abort search
   - Exclude files/folders from the final list

---

### Task 3 (optional):

Write unit tests that will show different modes of work of FileSystemVisitor from Task 1.

Discuss with your mentor whether you need to use Mock.

All unit tests should pass successfully.

NB! Scoreboard:

1-4 stars – 1st and 2nd tasks implemented
5 stars – Optional task is covered, and all tests are GREEN.

---

## Questions for the self-check:
1. **What is the difference between reference and value types?**

> A variable of a value type contains *an instance of the type*. This differs from a variable of a reference type, which contains a *reference to an instance of the type*. By default, on assignment, passing an argument to a method, and returning a method result, variable values are copied. In the case of value-type variables, the corresponding type instances are copied.

2. **What is boxing and unboxing?**

> When a variable of a value type is converted to `object`, it's said to be *boxed*. When a variable of type object is converted to a value type, it's said to be *unboxed*.
>
> *Boxing* is the process of converting a value type to the type `object` or to any `interface` type implemented by this value type. When the common language runtime (CLR) boxes a value type, it wraps the value inside a `System.Object` instance and stores it on the managed heap. Unboxing extracts the value type from the object. Boxing is implicit; unboxing is explicit.

3. **What is a class? What is an interface?**

> A `class` is a way of describing an entity that defines state and behavior, as well as rules for interacting with that entity. The class is a reference data type.
>
> An `interface` defines a contract. Any `class` or `struct` that implements that contract must provide an implementation of the members defined in the interface. An interface may define a default implementation for members. It may also define static members in order to provide a single implementation for common functionality. Beginning with C# 11, an interface may define `static abstract` or `static virtual` members to declare that an implementing type must provide the declared members. Typically, `static virtual` methods declare that an implementation must define a set of overloaded operators.

4. **What is the difference between class and structure?**

> Class is a reference type, struct is a value type. Structure types have value semantics.
>
> `Reference` types are allocated on the `heap` and garbage-collected, whereas `value` types are allocated either on the `stack` or inline in containing types and deallocated when the stack unwinds or when their containing type gets deallocated. Therefore, allocations and deallocations of value types are in general cheaper than allocations and deallocations of reference types.
>
>  Arrays of `reference` types are allocated out-of-line, meaning the array elements are just references to instances of the `reference` type residing on the heap. `Value` type arrays are allocated inline, meaning that the array elements are the actual instances of the `value` type. Therefore, allocations and deallocations of `value` type arrays are much cheaper than allocations and deallocations of reference type arrays. In addition, in a majority of cases `value` type arrays exhibit much better locality of reference.
>
> `Value` types get boxed when cast to a reference type or one of the interfaces they implement. They get unboxed when cast back to the `value` type. Because boxes are objects that are allocated on the heap and are garbage-collected, too much boxing and unboxing can have a negative impact on the heap, the garbage collector, and ultimately the performance of the application.
>
> `Reference` type assignments copy the reference, whereas `value` type assignments copy the entire value. Therefore, assignments of large reference types are cheaper than assignments of large `value` types
>
> `Reference` types are passed by reference, whereas `value` types are passed by value. Changes to an instance of a reference type affect all references pointing to the instance. `Value` type instances are copied when they are passed by value. When an instance of a `value` type is changed, it of course does not affect any of its copies. Because the copies are not created explicitly by the user but are implicitly created when arguments are passed or return values are returned, `value` types that can be changed can be confusing to many users. Therefore, `value` types should be immutable

5. **What is a generic type? What is Covariance and Contravariance?**

> Generics let you tailor a method, class, structure, or interface to the precise data type it acts upon. For example, instead of using the `Hashtable` class, which allows keys and values to be of any type, you can use the `Dictionary<TKey,TValue>` generic class and specify the types allowed for the key and the value. Among the benefits of generics are increased code reusability and type safety.
>
> `Covariance` and `contravariance` are terms that refer to the ability to use a more derived type (more specific) or a less derived type (less specific) than originally specified. Generic type parameters support covariance and contravariance to provide greater flexibility in assigning and using generic types.
>
> - Covariance (out)
>
>   Enables you to use a `more derived` type than originally specified.
>
>   You can assign an instance of IEnumerable<Derived> to a variable of type IEnumerable<Base>.
>
>       IEnumerable<Derived> d = new();
>       IEnumerable<Base> b = d;
>
> - Contravariance (in)
>
>   Enables you to use a `more generic (less derived)` type than originally specified.
>
>   You can assign an instance of Action<Base> to a variable of type Action<Derived>.
>
>       Action<Base> b = (target) => { Console.WriteLine(target.GetType().Name); };
>       Action<Derived> d = b;
>       d(new Derived());
>
> - Invariance
>
>   Means that you can use only the type originally specified. An invariant generic type parameter is neither covariant nor contravariant.
>
>   You cannot assign an instance of List<Base> to a variable of type List<Derived> or vice versa.

6. **What is delegate?**

> A delegate is a type that holds a reference to a method. A delegate is declared with a signature that shows the return type and parameters for the methods it references, and it can hold references only to methods that match its signature. A delegate is thus equivalent to a type-safe function pointer or a callback. A delegate declaration is sufficient to define a delegate class.
>
>       public delegate string Reverse(string s);
>
>       static string ReverseString(string s)
>       {
>           return new string(s.Reverse().ToArray());
>       }
>
>       Reverse rev = ReverseString;

7. **What is event?**

> An event is a member that enables an object or class to provide notifications. Clients can attach executable code for events by supplying event handlers.
> The type of an event declaration shall be a `delegate type`, and that `delegate type` shall be at least as accessible as the event itself.

8. **What is the difference between delegate and event?**

> Delegate is a type-safe function pointer. Event is an implementation of publisher-subscriber design pattern using delegate.
>
> An Event declaration adds a layer of abstraction and protection on the delegate instance. This protection prevents clients of the delegate from resetting the delegate and its invocation list and only allows adding or removing targets from the invocation list.
>
>       public class Animal
>       {
>           public Action Run {get; set;}
>
>           public void RaiseEvent()
>           {
>               if (Run != null)
>               {
>                   Run();
>               }
>           }
>       }
>
>       // delegate, all is fine
>       Animal animal= new Animal();
>       animal.Run += () => Console.WriteLine("I'm running");
>       animal.Run += () => Console.WriteLine("I'm still running") ;
>       animal.RaiseEvent();
>
>       //but
>       animal.Run += () => Console.WriteLine("I'm running");
>       animal.Run += () => Console.WriteLine("I'm still running");
>       animal.Run = () => Console.WriteLine("I'm sleeping") ;