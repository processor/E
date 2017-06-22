Table type : Block { }    // <table />  

Row type : Block { }      // <row />      

Column type : Flex { }    // <column />

Table protocol { 
  rows -> [ ] Row
}

Row protocol { 
  mutable columns = [ ] columns
}

Column protocol { 

}