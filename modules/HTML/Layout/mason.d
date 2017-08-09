Masonary `Layout class {
  gap     :   f32
  columns : [ Box ]

  from (columnCount: i32, columnWidth: f32, gap: f32 = 10) {
    let mutable left = 0
    
    let mutable columns = [ Column ];

    for i in 0..<columnCount {
      columns.add {
        width  : columnWidth,
        height : 0,
        top    : 0,
        left   : left
      }

      left += columnWidth + gap
    }

    return Masonary `Layout { gap, columns }
  }

  shortestColumn ƒ() => 
    from columns 
    orderby $0.height descending |> first
}

Layout impl for Masonary `Layout {
  doLayout (elements) { 
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

Box type { 
  width          : f32  
  mutable height : f32  
  top            : f32 
  left           : f32
}