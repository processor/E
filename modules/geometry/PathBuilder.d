Path `Builder protocol {
  arc   (to: Vector3)
  curve (to: Vector3)     // quadratic
  close ()         
  
  move  (to: Vector3)
  line  (to: Vector3)

  fill  (color: Texture | Color)
}