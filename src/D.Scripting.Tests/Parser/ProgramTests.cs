using System;
using System.Linq;

using Xunit;

namespace D.Parsing.Tests
{
    public class ProgramTests : TestBase
    {
        [Fact]
        public void Geometry()
        {
            foreach (var doc in ReadDocuments("geometry"))
            {
                try
                {
                    var program = new Parser(doc.OpenText().ReadToEnd());

                    var statements = program.Enumerate().ToArray();
                }
                catch (Exception ex)
                {
                    throw new Exception("error in " + doc.Name, ex);
                }
            }
        }

        [Fact]
        public void Math()
        {
            var text = ReadDocument("math/Functions.d");

            var program = new Parser(text);

            var statements = program.Enumerate().ToArray();

            Assert.Equal(52, statements.Length);
        }

        [Fact]
        public void Vector3()
        {
            var text = ReadDocument("numerics/Vector3.d");

            var program = new Parser(text);

            var statements = program.Enumerate().ToArray();

            Assert.Equal(2, statements.Length);
        }

        [Fact]
        public void Masonary()
        {
            // implementation

            var program = new Parser(@"
using layout

Masonary`Layout type {
  columnWidth : Number
  columnGap   : Number
  columns     : [ Column ]
}

Masonary`Layout implementation {
  from (columnCount: Number, columnWidth: Number, columnGap = 0) {
    var left = 0

    var columns = [ ] Columns;

    for i in 0..<columnCount {
      let column = {
        width  : columnWidth,
        height : 0,
        top    : 0,
        left   : left
      }

      left += columnWidth + columnGap
      
      columns.append(column)
    }

    return Masonary`Layout { columnWidth, columnGap, columns }
  }

  getSmallestColumn() {
    var bestColumn = this.columns[0]

    // |> skip 1 
    for column in columns {
      if column.height < bestColumn.height {
        bestColumn = column
      }
    }

    return bestColumn
  }

  layout(items) {
    for item in items {
      let column = getSmallestColumn
                
      // Add bottom gutter
      if column.height > 0 {
        column.height += columnGap
      }

      item.left = column.left
      item.top = column.height

      // Add the item height to the column
      column.height += item.height
    }

    var height = 0
    
    for column in columns { 
      if column.height > height {
        height = column.height
      }
    }

    return {
      width  : (columnGap * (columnCount - 1)) + (columnWidth * columnCount),
      height : height
    }
  }
}");

            var statements = program.Enumerate().ToArray();

            Assert.Equal(3, statements.Length);
        }
        [Fact]
        public void JpegDecoder()
        {
            var program = new Parser(@"
import imaging

Image type { 
  width      :  i32
  height     :  i32
  pixels     : [ Color ]
  colorspace :  Colorspace
}

let unzig = [
  0, 1, 8, 16, 9, 2, 3, 10, 17, 24, 32, 25, 18, 11, 4, 5, 12, 19, 26,
  33, 40, 48, 41, 34, 27, 20, 13, 6, 7, 14, 21, 28, 35, 42, 49, 56, 57,
  50, 43, 36, 29, 22, 15, 23, 30, 37, 44, 51, 58, 59, 52, 45, 38, 31,
  39, 46, 53, 60, 61, 54, 47, 55, 62, 63 
]

let blockSize: i32 = 4096

resize ƒ (
  image     : Image, 
  size      : Size, 
  resampler : Resampler = Lanzos
) -> Image { 

}

readBlock ƒ (
  data: Binary,
  size: i32 > 0 = blockSize) -> Block {
}

decode ƒ (data: JPEG) -> Image {
  var n = data.length
  let quant = Color[255]

  match x >> 4 {
    0 when n < blockSize => {
      n -= blockSize

      readBlock(data)

      for i in 0...blockSize { 
        quant[i] = data[i]
      }
    }

    1 when n < 2 * blockSize => {
      n -= 2 * blockSize

      // readBlock(data, 2 * blockSize)
    }

    _ => BadValue
  }

  return Image(x)
}
");

            var statements = program.Enumerate().ToArray();
        }
    }


        
}
