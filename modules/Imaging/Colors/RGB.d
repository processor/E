 RGB<T: ℝ & Blittable> struct: Color {
  r, g, b : T

  to YCbCr<T> {
    let r_f: f32 = r;
    let g_f: f32 = g;
    let b_f: f32 = b;

    let y  =  0.299f32  * r_f + 0.587f32  * g_f + 0.114f32  * b_f
    let cb = -0.1687f32 * r_f - 0.3313f32 * g_f + 0.5f32    * b_f + 128f32
    let cr =  0.5f32    * r_f - 0.4187f32 * g_f - 0.0813f32 * b_f + 128f32

    return YCbCr(y, cb, cr)
  }
}

RGB`24 alias = RGB<Byte>
RGB`48 alias = RGB<Float16>