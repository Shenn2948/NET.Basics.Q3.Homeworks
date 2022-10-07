# Debugging Fundamentals

## Home task:
In provided project fix Utilities.Sort and Utilities.IndexOf methods so that the tests run successfully. Use debugging for investigations.

## Questions for the self-check:
1. **What is break mode? What are the options to step through code?**

> In *break mode*, app execution is suspended while functions, variables, and objects remain in memory. When the debugger is in *break mode*, you can navigate through your code. There are two common ways to quickly enter break mode:
>
> - Begin code stepping by selecting F10 or F11. Doing so allows you to quickly find the entry point of your app. You can then continue to press step commands to navigate through the code.
>
> - Run to a specific location or function, for example, by setting a breakpoint and starting your app.
>
> For example, in the code editor in Visual Studio, you can use the **Run To Cursor** command to start the app, debugger attached, and enter break mode, and then select **F11** to navigate through the code:
>
> When you're in break mode, you can use various commands to navigate through your code. You can examine the values of variables to look for violations or bugs. For some project types, you can also make adjustments to the app when you're in break mode.
>
> Most debugger windows, like the **Modules** and **Watch** windows, are available only when the debugger is attached to your app. Some debugger features, like viewing variable values in the **Locals** window or evaluating expressions in the **Watch** window, are available only when the debugger is paused (that is, in *break mode*).

2. **How to ignore some exceptions during debugging?**

> You can add and delete exceptions. To delete an exception type from a category, select the exception, and choose the **Delete the selected exception from the list** button (the minus sign) on the **Exception Settings** toolbar. Or you may right-click the exception and select **Delete** from the shortcut menu.
>
> Deleting an exception has the same effect as having the exception unchecked, which is that the debugger won't break when it's thrown.

3. **How to set up conditional breakpoint?**

> You can control when and where a breakpoint executes by setting conditions. The condition can be any valid expression that the debugger recognizes. For more information about valid expressions, see Expressions in the debugger.
>
> **To set a breakpoint condition:**
>
> 1. Right-click the breakpoint symbol and select Conditions (or press Alt + F9, C). Or hover over the breakpoint symbol, select the Settings icon, and then select Conditions in the Breakpoint Settings window.
>
>    You can also right-click in the far left margin next to a line of code and select Insert Conditional Breakpoint from the context menu to set a new conditional breakpoint.
>
>    You can also set conditions in the Breakpoints window by right-clicking a breakpoint and selecting Settings, and then selecting Conditions
>
> 2. In the dropdown, select Conditional Expression, Hit Count, or Filter, and set the value accordingly.
>
> 3. Select Close or press Ctrl+Enter to close the Breakpoint Settings window. Or, from the Breakpoints window, select OK to close the dialog.

4. **What is data breakpoint?**

> Data breakpoints break execution when a specific object's property changes. (.NET Core 3.x or .NET 5+)
>
> **To set a data breakpoint**
>
> 1. In a .NET Core project, start debugging, and wait until a breakpoint is reached.
> 2. In the Autos, Watch, or Locals window, right-click a property and select Break when value changes in the context menu.

5. **What is trace point and how to use it?**

> Tracepoints allow you to log information to the Output window under configurable conditions without modifying or stopping your code. This feature is supported for both managed languages (C#, Visual Basic, F#) and native code as well as languages such as JavaScript and Python.
>
> You can set tracepoints by specifying an output string under the Action checkbox in the Breakpoint Settings window.

6. **What are pdb files, when are they created and how to use them?**

> Program database (.pdb) files, also called symbol files, map identifiers and statements in your project's source code to corresponding identifiers and instructions in compiled apps. These mapping files link the debugger to your source code, which enables debugging.
>
> When you build a project from the Visual Studio IDE with the standard Debug build configuration, the compiler creates the appropriate symbol files.
---
> The .pdb file holds debugging and project state information that allows incremental linking of a Debug configuration of your app. The Visual Studio debugger uses .pdb files to determine two key pieces of information while debugging:
>
> - The source file name and line number to display in the Visual Studio IDE.
> - Where in the app to stop for a breakpoint.
---
> The debugger only loads .pdb files that exactly match the .pdb files created when an app was built (that is, the original .pdb files or copies).
>
> The debugger searches for symbol files in the following locations:
>
> - The project folder.
> - The location that is specified inside the DLL or the executable (.exe) file.
>
>   By default, if you have built a DLL or an .exe file on your computer, the linker places the full path and filename of the associated .pdb file in the DLL or .exe file. The debugger checks to see if the symbol file exists in that location.
> - The same folder as the DLL or .exe file.
> - Any locations specified in the debugger options for symbol files

1. **What is symbol server?**

> Visual Studio can download debugging symbol files from symbol servers that implement the *symsrv* protocol. Visual Studio Team Foundation Server and the Debugging Tools for Windows are two tools that can use symbol servers.
>
> Symbol servers you might use include:
>
> **Public Microsoft Symbol Servers**: To debug a crash that occurs during a call to a system DLL or to a third-party library, you often need system .pdb files. System .pdb files contain symbols for Windows DLLs, .exe files, and device drivers. You can get symbols for Windows operating systems, MDAC, IIS, ISA, and .NET from the public Microsoft Symbol Servers.
>
> **Symbol servers on an internal network or on your local machine**: Your team or company can create symbol servers for your own products, and as a cache for symbols from external sources. You might have a symbol server on your own machine.
>
> **Third-party symbol servers**: Third-party providers of Windows applications and libraries can provide access to symbol server on the internet.

8. **What are debug and release build configurations?**

> Visual Studio projects have separate release and debug configurations for your program. You build the debug version for debugging and the release version for the final release distribution.
>
> In debug configuration, your program compiles with full symbolic debug information and no optimization. Optimization complicates debugging, because the relationship between source code and generated instructions is more complex.
>
> The release configuration of your program has no symbolic debug information and is fully optimized. For managed code and C++ code, debug information can be generated in .pdb files, depending on the compiler options that are used. Creating .pdb files can be useful if you later have to debug your release version.