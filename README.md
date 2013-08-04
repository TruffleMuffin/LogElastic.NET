LogElastic.NET
===================

A logging tool that uses Logstash based entries and [ElasticSearch](http://www.elasticsearch.org/) via [PlainElastic.NET](https://github.com/Yegoroff/PlainElastic.Net). Purpose is to be optimized for support by [Kibana](http://three.kibana.org/), and provide an array of useful functionality for logging without text files and lots of setup.

Installation
-------------------------

Please install Elastic Search then follow the Usage instructions.

Usage
-------------------------

Use the NuGet Package LogElastic.NET where you want to call Log (as described below). In your executing application, i.e. Web Site, or .exe etc, Use NuGet Package LogElastic.Net.Manager. This will install a dependency for storage that is not required by LogElastic.NET. I hope this helps keep your non-application Projects a bit cleaner.

You can call Log.Trace(), Log.Info() and Log.Error() always with little performance overhead when Logging is Enabled or Disabled. These methods raise events that the Storage actions - only after Initialised with the AppSetting turned on - in a seperate Thread every 60 seconds.

You can use Log.GetLogger() to mock the ILog interface if you wish to test your code is logging what you expect.

You can use Log.Performance(optional name) to track the time a code block takes to execute. When not provided, the name will be the calling codes Namespace and Method name.

Examples
-------------------------

### Simple Logging

```c#
Log.Trace("Happy Logging!");
Log.Info("I started application {0}.", Variable);
Log.Error("I started application {0} and it did this execption {1}.", variable, exception.Message);
```

### Testable Logging

```c#

readonly ILog log;

public MyClass() : this(Log.GetLogger()) { }

public MyClass(ILog log) {
	this.log = log
}

void MyMethod(){
	log.Trace("Happy Logging!");
}
```

### Performance

```c#
using (Log.Performance())
{
    // My Code here
}
```
		
History
-------------------------

* 0.0.1 - Project Begins
* 0.1 - Initial version with limited functionality
* 0.2 - Ability to turn logging on and off
* 0.3 - Ability to turn use an instance logger that can be tested
* 0.4 - Ability to turn logging on and off via ApiController and refactoring of components for new NuGet packages
* 0.5 - Performance tracking