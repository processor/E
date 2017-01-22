using System;

namespace D.Mathematics
{
    public class MathConstants : Module
    {
        public MathConstants()
        {
            // Imaginary

            Add("π", new Float(Math.PI));                                      // pi
            Add("γ", new Float(0.5772156649015328606065120900824024310421d));  // Euler-Mascheroni  constant
        }
    }

    // φ: Golden Ration
    // https://en.wikipedia.org/wiki/Euler%E2%80%93Mascheroni_constant
}
