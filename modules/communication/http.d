HTTP protocal {
  * HttpHeader : head
  * byte       : body
  ∎
}


HttpConnection protocal : Connection { 
  url     : URI,
  protocol: HTTP1 | HTTP1.1 | H2 | Websocket?
}