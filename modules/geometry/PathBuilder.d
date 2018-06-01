Path `Builder<T: ℝ & Blittable: Float64> protocol {
  arc   (to: Vector3<T>)
  curve (to: Vector3<T>)     // quadratic
  close ()         
  
  move  (to: Vector3<T>)
  line  (to: Vector3<T>)

  fill  (color: Texture | Color)
}