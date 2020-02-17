Table protocol {
  rows -> [] Row
}

// <table />  
Table class : Block {

  // <row />
  Row class : Block {
    columns : [] Column
  }           

  // <column />
  Column class : Flex { }


}



// Table::Row
// Table::Column
