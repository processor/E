namespace E.Mathematics;

public class MathConstants : Module
{
    public MathConstants()
    {
        // Imaginary

        AddExport("π", new Number(Math.PI));                                      // pi
        AddExport("γ", new Number(0.5772156649015328606065120900824024310421d));  // Euler-Mascheroni  constant
    }
}

// φ: Golden Ration
// https://en.wikipedia.org/wiki/Euler%E2%80%93Mascheroni_constant