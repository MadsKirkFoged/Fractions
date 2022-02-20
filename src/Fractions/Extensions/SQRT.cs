﻿using System;
using System.Numerics;

namespace Fractions.Extensions {
    public static partial class MathExt {

        /// <summary>
        /// Returns the square root of <paramref name="x"/>.
        /// Use <paramref name="accuracy"/> to set the accuracy by specifying the number of digits after the decimal point of accuracy.
        /// Higher value of <paramref name="accuracy"/> means better accuracy but longer calculations time.
        /// </summary>
        /// <param name="x">Source value</param>
        /// <param name="accuracy">Number of digits after the decimal point of accuracy</param>
        public static Fraction Sqrt(this Fraction x, int accuracy = 30) {
            //Babylonian Method of computing square roots

            if (x < Fraction.Zero) {
                throw new OverflowException("Cannot calculate square root from a negative number");
            }

            if (accuracy <= 0) {
                throw new ArgumentOutOfRangeException(nameof(accuracy), accuracy, $"Accuracy of {accuracy} is not allowed! Have to be above 0.");
            }


            Fraction oldGuess;
            var newGuess = Fraction.Zero;
            var tolerance = new Fraction(BigInteger.One, BigInteger.Pow(new BigInteger(10), accuracy));


            //Using Math.Sqrt to get a good starting guess
            var guessDouble = Math.Sqrt((double)x);
            if (double.IsInfinity(guessDouble)) {
                oldGuess = x / Fraction.Two;
            } else {
                oldGuess = (Fraction)guessDouble;
            }


            while ((oldGuess - newGuess).Abs() > tolerance) {
                //Babylonian Method
                newGuess = (oldGuess + (x / oldGuess)) / Fraction.Two;
                oldGuess = newGuess;
            }

            return newGuess;
        }
    }
}