Gallery type : Block {
  current : Slide
  slides  : [ Slide ]
}

Slide type {
  index            : Int32
  mutable previous : Gallary_Slide
  mutable next     : Gallary_Slide
}

Gallery impl {
  from (medias: [ Media ]) {
    let slides = [ Slide ]
    
    let mutable last : Slide
    let mutable index = 0

    for medias {
      // Create a viewer for the media
      
      let element = match $ {
        | Image _ => Image `Element $
        | Video _ => Video `Element $
        | Audio _ => Audio `Element $
      }

      let slide = Slide { element, index: index, previous: last }

      // Create a linked list
      if (last) last.Next = slide

      slide.previous = last

      slides.add(slide)

      last = slide
      index += 1
    }

    return Gallery { slides, current: slides[0] }
  }
  
  view (index: Int64) {

  }

  previous () {
    if previous {

    }
    else {
      view
    }
  }

  next {

  }

  on Pointer `pressed press {
    observe gallary Pointer_move {
      // Drag the slide

    } until gallary.Root Pointer `released
  }

  on Attached => log "attached"

  on Detached => log "detached"
}

  // Attach the event listeners on construction


<Document (
  <Header>

  <Table>
	<Row (
	  <Column "a" />
	  <Column "b" />
	  <Column "c" />
	) />
  </Table>

  <Paragraph ("The quick brown fox")>
)>

<Document (
  <Header>
	  <Heading (site.title) />
  </Header>

  {#project}
    {#children}
      {#children}
        {$0}
      {/children}
    {/children}
  {/project}
</Document>


<Table (
	<Row (
	  <Column "a" />
	  <Column "b" />
	  <Column "c" />
	)/> 
)/>








  <Image>
  <Video>
  <Audio>

  <Piece>
