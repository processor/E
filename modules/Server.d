from HTTP       import Context, Request, Response, Server
from Templating import Template


handle ƒ (context: Context) {
  match context.request.path {
    | "/" 		      => Template("/home/index")       |> renderTo(context.response)
    | $"/{section}" => Template($"/{section}/index") |> renderTo(context.response)
    | _             => Template("/errors/404")       |> renderTo(context.response)
  }
}

let server = Server(
  handler: handle
)

server.handle("/", ƒ (context) { 
   Template("/home/index") |> renderTo(context.response)
});

server.start()
