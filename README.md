LogElastic.NET
===================

A logging tool that uses Logstash based entries and ElasticSearch via PlainElastic.NET. Purpose is to be optimized for support by Kibana, and provide an array of useful functionality for logging without text files and lots of setup.

Usage
-------------------------
		
You should call Log.Initialise() or Log.Initialise("ip") when your ElasticSearch installation is not hosted locally. If you have an AppSetting "LogElastic.Enabled" set as a true for your application, Log.Initialise() will attach the Log Storage mechanism and LogElastic.NET will begin to export your Log messages to ElasticSearch.

You can call Log.Trace(), Log.Info() and Log.Error() always with little performance overhead when Logging is Enabled or Disabled. These methods raise events that the Storage actions - only after Initialised with the AppSetting turned on - in a seperate Thread every 60 seconds.

Installation
-------------------------

Please install Elastic Search then follow the Usage instructions.
		
History
-------------------------

* 0.0.1 - Project Begins
* 0.1 - Initial version with limited functionality
* 0.2 - Ability to turn logging on and off