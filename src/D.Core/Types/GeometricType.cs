namespace E;

public enum GeometricType
{
    Arc        = 147_572,
    Curve      = 161_973,
    Line       = 37_105,
    Path       = 1_415_372, // Point *
    Plane      = 17_285,
    Point      = 44_946, // Vector | (x,y,z?)
    Polygon    = 37_555, // Path Point * 
    Quaternion = 173_853,
    Ray        = 1_147_832, // (position: Vector3, direction: Vector3)

    // 2D
    Circle     = 17_278,
    Ellipse    = 40_112,
    Triangle   = 19_821,
    Rectangle  = 209, 

    // 3D
    Cone       = 42_344,
    Cylinder   = 34_132,
    Ellipsoid  = 190_046,
    Prism      = 180_544,
    Pyramid    = 3_358_290,
    Sphere     = 12_507,
    Torus      = 12_510,
}