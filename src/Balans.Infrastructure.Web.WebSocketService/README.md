# Intro

# Development process

* Realizing WebSocketConnectionManager class 
  *  Accessing from interface
  *  Thread safe collection 
  *  Add/Remove a socket 
  
* Realizing WebSocketHandler class
  * Is abstract
  * Handle connections
  * Disconnect Events
  * Manage sending and receiving Message/data  through socket protocoll

* Realizing a Middleware
  * Creating a class for this purpose calls WebSocketManagerMiddleware
  * Receive RequestDelegate
  * Adding it after routing midllware why?
  * Extending method for adding the middlware
    * Start with Use
    * IApplicationBuilder as this parameter
 
# References
* [Spec for websocket protocol](https://tools.ietf.org/html/rfc6455#section-7.1.6)
* [API Websocket](https://html.spec.whatwg.org/multipage/web-sockets.html#network)
