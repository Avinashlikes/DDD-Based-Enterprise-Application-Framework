# Domain Driven Design Based Enterprise Application Framework 

An opinionated Enterprise Application Framework based on different Patterns, Principles and Practices of Domain Driven Design. Although not all tactical and strategic Patterns, Principles and Practices of DDD are in place but the most important ones (the ones that are used almost in any Enterprise app) are in place.

This framework is helpful in scenarios wherein one needs to interact with different Integration technologies using different .NET based access technologies (can be DBs, SOAP or RESTFUL Web Services or MQs or File System or an LDAP or any other imaginable data source).Another possible scenario can be in a CQRS (Command Query Responsibility Segregation) environment wherein the commands are processed in an RDBMS like SQL Server whereas the queries are executed to fetch data from NOSQL Dbs. Also, once this framework is completed and if someone uses this framework, at least for the development of a SPA based Web Application or Website or a Mobile Web application, ideally he/or she should need to work only on Domain Modelling(i.e. mainly Domain, Domain Services and Application Services) and UI stuffs(there might be some requirement for extending some extensibility points which are already provided out of the box or creating new extensibility points all-together or some other configuration stuff changes required like DI container specific stuffs or ORM configurations if RDBMS is used etc.).That doesn't mean that all these can be applied only for Web apps or Websites or Mobile Web Application only but in-fact, parts of this framework can be applied to other types of applications as well e.g. Business Process Management and Integration projects as well.

DDD is more about domain modelling for complex domains using concepts of Entities, Value Objects, and Aggregates etc. and separating out your Business functionalities from your technical functionalities. Although this framework provides(or will provide) most of the technical functionalities(out of the box, including the source code) used in an Enterprise app and some base level classes for dealing with Entities, Value Objects, Aggregates etc. but it's not necessary that one is going to need every bit of it. So use this framework (or may be just parts of the framework) diligently after analyzing the requirements for your app meticulously.

Implementation Overview-> 
Here the CommandRepository(for persisting data) and QueryableRepository(for querying data) are in-memory representation of some external source - mainly DBs(but can be extended to Web Services or MQ interactions as well or any other imaginable data source for that matter).
The CommandRepository class needs instances of concrete implementation of BaseUnitOfWork and ICommand which can be injected using some DI Framework like Unity.

UnitOfWork (implements IUnitOfWork members) - De-couples the logic to do atomic transactions across Dbs(can be extended to use Web Services/MQs or some other data source as well to be part of the Transaction) using different DB access technologies(again can be extended to use Web Service/MQs or any other data source). UnitOfWork based transactions can be applied at the API or Domain Services or Repository layer.

ICommand & IQuery- Provides contracts to deal with different DB technologies viz. ADO.NET, Enterprise Library or ORMs like Entity FrameWork Code First etc. and different DBs (current implementation supports mainly SQL Server - but as mentioned earlier also, can be extended to support other DB types as well).

Pending Tasks ->

• Incorporation of some tactical DDD stuffs (mainly the common framework elements).

• Trying exploring and incorporating Dapper(a Micro-ORM - Micro ORMs may not provide you some functionalities like UnitOfWork out of the box like that of an ORM but performance wise they are way better compared to ORMs), Event Stores and Grid Based Storage.

• Incorporation of some Restful stuffs which are commonly used in most Enterprise Apps.

• Whatever done till now is all Orchestrations rather than Event Driven Choreographies. Even the async await based request reply mechanisms are also actually Orchestrations. True Fire and Forget Event Driven Choreographies (may be with some nominal acknowledgement   sent to the requester) following Eventually Consistent approach WILL ALSO BE TRIED, at the Web API Layer using "Event Driven Rest"     and at the Business Layer using [Zero MQ](http://zeromq.org/).[Zero  MQ](http://zeromq.org/) was designed from the ground up, keeping   in mind stock trading apps wherein very high throughput and very low latency are required, as discussed 
  [here](http://aosabook.org/en/zeromq.html).         
 
• Testing BulkOperations using SQL Express Edition.

• Fixing WCF related Unit Test Case(s).

• Redesign Caching stuffs to support in-memory caching or some scalable option like Windows AppFabric or Redis(a scalable NOSQL  option). Ideally, should be designed in a pluggable way to support any cool Caching mechanism coming in future as well. Also should use   some AOP or attribute (annotation) based approach to apply Caching or invalidating the Cache else it becomes very hectic to apply these   cross cutting concerns everywhere within a large application.
  
• Exploring Single Page Applications and building a Fluent UI Framework using which the UI layout (HTML + CSS) and UI Behaviors (using JavaScript) can be coded in a fluent way using JavaScript. IF POSSIBLE, will try to have plugin features wherein SPA Frameworks like Angular or React can be plugged in as and when required. Will also try to incorporate Offline-First capabilities. All     these probably will have a GitHub Project of its own and that will be used in this project.This is going to take quite some time since   lots of exploration needs to be done in this area.

• Ultimately building a SAAS Framework based on all the above stuffs.

• Fixing or suppressing the Warnings generated by MS Code Analysis Tool (currently, Code Analysis Settings is set to "Microsoft
  Basic Design Guidelines").

• Also need to run the Code Metrics to check everything is as per standards.

**If at all this codebase is migrated to .NET Core then hopefully [ASP.NET Core Documentation](https://docs.asp.net/en/latest/) and [Porting to .NET Core](https://blogs.msdn.microsoft.com/dotnet/2016/02/10/porting-to-net-core/) will be pretty much helpfull.But currently lots of .NET Framework stuffs are not supported by the current .NET Core version as verified by [.NET Portability Analyzer Tool](https://visualstudiogallery.msdn.microsoft.com/1177943e-cfb7-4822-a8a6-e56c7905292b).Also don't have any plans to have a mix and match of .NET Framework and .NET Core environment working together since that would have its own challenges(didn't do any thorough analysis though) and the whole point of using .NET(wholly) for LINUX/MAC is missing in such an approach(although one might suggest to deploy .NET Framework code as a Web Service deployed in a Windows machine while the .NET Core code deployed in some LINUX/MAC machine consuming the Web Service but for sure, that would have its own challenges).Anyways, that's secondary and so MIGHT be taken care at some later point of time.
