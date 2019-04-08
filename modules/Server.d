let container          = Storage::Container(145234)

let template`Processor = Templating::Processor(container)
let image`Processor    = Imaging::Processor()

let handler = ƒ(context: HTTP::Context) {
  let path = Path(context.request.path)

  if (path.format != null) {
    let object = container.get(path)

    match path.format {
      | "css" => CSS::render(object)
      | "js"  => JavaScript::render(object)
      | "md"  => Markdown::render(object)

      | "png" 
      | "jpeg" 
      | "gif" => image`Processor.process(object)
    }

    $"Unsupported format: {path.format}" |> context.response.write 

    return;
  }
  
  match path {
    | "/" 		      => template`Processor.process("/home/index")
    | $"/{section}" => template`Processor.process($"/{section}/index")
    | _             => template`Processor.process("/errors/404")
  }
}

HTTP::Server(handler: handler).start()

