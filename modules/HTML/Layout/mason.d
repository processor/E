Masonary class {
  gap     :   Number
  columns : [ Box ]

  init (columnCount: i32, columnWidth: f32, gap: f32 = 10) {
    var left = 0
    
    gap     = gap
    columns = [ Box ];

    for i in 0..<columnCount {
      columns.add(Box(
        width  : columnWidth,
        height : 0,
        top    : 0,
        left   : left
      ))

      left += columnWidth + gap
    }
  }

  shortestColumn ƒ() => columns.orderByDescending(c => c.height) |> first
}

Layout impl for Masonary {
  layout (elements: [ Node ]) { 
    for el in elements {
      let column = shortestColumn()
                
      // Add bottom gutter
      if column.height > 0 {
        column.height += columnGap
      }

      el.left = column.left
      el.top  = column.height

      // Add the item height to the column
      column.height += el.height
    }
    
    return Size {
      width  : (columnGap * (columns.count - 1)) + (columnWidth * columnCount),
      height : columns |> map(c => c.height) |> max
    }
  }
}

Box struct {
  var width  = 0.0, 
      height = 0.0,
      top    = 0.0, 
      left   = 0.0
}