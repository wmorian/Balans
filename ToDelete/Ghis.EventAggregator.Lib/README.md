# DTM12EventAggregator library
## Intro
Using IEventAggregator in Prism looks strange and complex
### Motivation
* Simple way to manage Event in WPF- Application
* Using the library system.reactive.linq
* Any class should be an TEvent ( no restriction like in PRISM)
* IObservable<> interface is a interesting point, because it is a part of Namespace System

## Getting start
1. Simulate Client/Server App by creating respectly 2 Projects
2. Add a package Ghis to  projects
3. Create a simple subscription job
4. That's all!!!


## Description
### IDM12EventAggreator interface
* Definie only 2 Methods
* No dependency
* GetEvent< >(): Method is used to get an IObservable of TEvent
* Publish() :Method is for publishing a TEvent

### USE CASE
* Subscription: 
* Unsubscription:
* Selective Subscription : by using simple linq.
* Subscription on UI thread: .ObserveOnDispatcher()
 
