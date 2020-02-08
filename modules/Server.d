let container          = Storage::Container(145234)

let template_processor = Templating::Processor(container)
let image_processor    = Imaging::Processor()

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
      | "gif" => image_processor.process(object)
    }

    $"Unsupported format: {path.format}" |> context.response.write 

    return;
  }
  
  match path {
    | "/" 		      => template_processor.process("/home/index")
    | $"/{section}" => template_processor.process($"/{section}/index")
    | _             => template_processor.process("/errors/404")
  }
}

HTTP::Server(handler: handler).start()

