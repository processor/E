Table protocol { 
  rows -> [ Row ]
}

Table class : Block { }    // <table />  

 // <row />
Row class : Block {
  columns : [ Column ]
}           

 // <column />
Column class : Flex { }