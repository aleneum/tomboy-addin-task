TomboyTask
==========

Collects your tasks from all your Tomboy notes which comes in handy when you use Tomboy for Getting Things Done (GTD).
Supports contexts and ignores done tasks.

* status: 0.1.1 (alpha)
* tested with: Ubuntu 12.04 12.10 13.04
* requires: tomboy libgtk2.0-cil-dev libmono-posix4.0-cil

Mark and Collect Tasks
----------------------

* Create a bullet point within a Note and add "@Task".  
![create Note and @Task](img/ttask01.png)  
* Create your task list called "GTD". This one gets an additional tool called "Update Tasks".  
![create GTD](img/ttask02.png)  
* Update the list and have a look at your tasks, ordered by notebook.  
![update tasks](img/ttask03.png)

One Task per Note
-----------------
* TomboyTask ignores tasks you striked out.  
![list of tasks](img/ttask04.png)  
* if your note contains more than one task only the first not finished task is shown.  
![gtd list](img/ttask05.png)

Use Contexts
------------
* add a context to specify tasks.  
![advanced task](img/ttask06.png)  
* now you can create a specific task list called "GTD.advanced" to show only tasks for this context.  
![advanced task](img/ttask07.png)  
* The generic "GTD" note will collect all tasks, disregarding the context  
![advanced task](img/ttask08.png)

Installation Instructions
-------------------------
### Linux 
Change into the source directory and execute:  

	dmcs -platform:anycpu -out:TomboyTask.dll -target:library TomboyTaskNoteAddin.cs -res:TomboyTask.addin.xml -pkg:gtk-sharp-2.0,tomboy-addins -r:/usr/lib/mono/4.0/Mono.Posix.dll

The dll has to be copied into the addin directory of tomboy.  

	cp TomboyTask.dll ~/.config/tomboy/addins/

More info: https://live.gnome.org/Tomboy/PluginList
