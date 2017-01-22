// create Blob(a, stream); // identity assigned

on create(A).commited => {
  diagnostics log("commited")
}

match insert create(A) {
  | Commited => diagnostics log("commited")
  | _       => 
}

await insert(a)

// Accepted
// Commited
// Propagated


Blob.locate 100 |> open



a Blob has blocks via Blob'Blocks(blob) 
≡ Blob.blocks -> Blob'Blocks($0)
≡ sql(select block from Blob'Blocks where blob = $0)
//   hashes     ƒ (this Blob) ≡ Blob'Hashes($0)      // select hash      from hash Blob'Hashes   where blob = $0

/// block.blocks.sql ≡ select block from Blob'Blocks where blob = $0 // true



/*
for block in blob blocks {
  
}
*/


/*
        Accepted
   then Commited
   then Propagated
*/




// accessable everywhere

until delete “animating.gif” // a Deleted or Error event sometime in the future



create [ Dog("Blue",  age: 7)
         Dog("Bozer", age: 4)
         Dog("Bozer", age: 4)
       ] and wait until Propagated

let bob    = create Human("Bob")
let linda  = create Human("Linda")
let louise = create Human("Louise")
let gene   = create Human("Gene")
let tina   = create Human("Tina")

Sibling(a, b) ≡ Sibling(b, a)

married ƒ(a, b) => Marriages where a == a && b == b || a == b && b == a |> any

create Marriage(bob, linda, authority: Californa)

create [ 
  Sibling(louise, gene)  // infers Sibling(gene, louise) 
  Sibling(louise, tina)  // infers Sibling(tina, louise) 
  Sibling(gene, tina)    // infers Sibling(tina, gene) 
] and wait until Commited

married(linda, bob)       // = true

get Sibling(louise, gene) // = Sibling(louise, gene)

get Dog("Blue").age 

let dogYears = from Dogs 
               where name like "B*"
               select age |> sum


