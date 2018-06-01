Coordinates<T: ℝ & Blittable = Float64> struct {  
  latitude	 : T
  longitude	 : T
  altitude	 : Altitude<T>?
}