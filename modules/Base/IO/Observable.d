Observerable<T> protocol {
  subscriptions -> [] Subscription<T>

  subscribe(Function<Message|∎>) -> Subscription<T>
}
