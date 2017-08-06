Shard<T> record {
  ranges    : [ Range<i64> ],
  capacity  : i64 > 0,
  flags     : Master | Replica
}