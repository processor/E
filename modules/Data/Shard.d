Shard record {
  ranges    : [ Range<i64> ],
  capacity  :   i64 > 0,
  flags     :   Shard `Flags
}

Shard `Flags enum {
  Master  = 1
  Replica = 2 
}