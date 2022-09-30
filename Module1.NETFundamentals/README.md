# .NET Fundamentals

## Task 1:
Create 3 basic applications for the following .NET platforms:

- .NET Core – console
- .NET Framework – WinForms/WPF

Application requirements:

Input: `{username}` (for console app – as a command line parameter)

Output: `“Hello, {username}”` (via form or separate window)

## Task 2:
Create a .NET standard library which will handle “hello world” concatenation logic. Add this library for both projects. Change the output to following: `“{current_time} Hello, {username}!”`

### NB! Scoreboard:
1-4 stars – Both tasks are implemented

5 stars – Mentee can lead discussion on the Self-check questions.

## Questions for the self-check:
1. What does the .NET ecosystem provide?

> The .NET ecosystem encompasses different runtimes, such as:
> - .NET Framework (WPF, Windows Forms, ASP.NET) - Windows centric.
> - .NET Core (ASP.NET Core, Universal Windows Platform - UWP) - Cross-platform, works side-by-side with other versions.
> - .NET 5 (ASP.NET Core, WPF, Windows Forms, Blazor) - A unifying platform for desktop, web, cloud, mobile, gaming, IoT, and AI applications.
> - Mono for Xamarin (iOS, OS X, Android) - Cross-platform.
>
> All of the above runtimes implement .NET Standard, which is a specification of .NET APIs that have implementations for each runtime. This was done so that code created for one runtime could be executed with other runtimes.
>
> All the runtimes use tools and infrastructure to compile and run code. This includes languages (C#, Visual Basic), compilers (Roslyn), garbage collection, as well as build tools like MS Build or (Core) CLR.

2. What are .NET implementations? Which ones are used nowadays?

> A .NET app is developed for one or more implementations of .NET. Implementations of .NET include .NET Framework, .NET 5+ (and .NET Core), Universal Windows Platform (UWP) and Mono.
>
> Each implementation of .NET includes the following components:
>
> - One or more runtimes (CLR) — for example, .NET Framework CLR and .NET 5 CLR.
> - A class library (BCL) — for example, .NET Framework Base Class Library and .NET 5 Base Class Library.
> - Optionally, one or more application frameworks — for example, ASP.NET, Windows Forms, and Windows Presentation Foundation (WPF) are included in .NET Framework and .NET 5+.
> - Optionally, development tools. Some development tools are shared among multiple implementations.
>
> .NET 6 is currently the primary implementation, and the one that's the focus of ongoing development. .NET 6 is built on a single code base that supports multiple platforms and many workloads, such as Windows desktop apps and cross-platform console apps, cloud services, and websites. Some workloads, such as .NET WebAssembly build tools, are available as optional installations.

3. What is CLR?

> The Common Language Runtime (CLR) is the execution environment for a managed program. A CLR is also a virtual machine that not only executes apps but also generates and compiles code on-the-fly using a JIT compiler (IL code -> machine code).
> The CLR is an implementation of the CLI (Common Language Infrastructure) specification. Code that runs under the CLR is called managed code. CLR is responsible for:
>
> - assembly loading;
> - type safety;
> - strong typing;
> - exception handling;
> - garbage collection;
> - memory management;
> - thread management;
> - calling methods;

4. Why is .NET 5 a bit of a special version?

> It was released in November 2020, with the goal of unifying development for desktop, web, cloud, mobile, gaming, IoT, and AI. The earlier setup's goal was to produce a single, cross-platform .NET runtime and framework that integrated the best features of .NET Core, .NET Framework, Xamarin, and Mono.
>
> .NET 5 significantly improved single-file applications. Single-file applications are published and deployed as a single file, which includes the app and all of its dependencies. They can also be self-contained, which means they carry their own .NET runtime. Even though single file applications were already a thing in .NET Core, their behavior is completely different. In .NET Core 3.1, in fact, it packages' binaries are held in a single file for deployment and then it unpacks those files to a temporary directory to load and execute them. With .NET 5, instead, when the app is run its dependencies are loaded directly from that file into memory. .NET 5 also brings smaller single-file applications with more efficient memory usage and can be used in microservices-based and containerized applications.
>
> In addition, one of the important new features is support for ARM64 that will allow .NET 5 applications to run natively on Windows and ARM hardware (such as new Apple M1 ARM-based processors). This means that is has the widest OS support of any .NET version before.

5. Which technologies are supported by the .NET framework?
2. Does cross-platform development is possible in .NET? What about cross-platform UI?
3. What does the multitargeting term mean?