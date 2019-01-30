using System;

namespace CsharpOopAssignment
{
    public class SimplifiedRational : RationalBase
    {
        public int Numerator { get; }
        public int Denominator { get; }

        /**
         * Determines the greatest common denominator for the given values
         *
         * @param a the first value to consider
         * @param b the second value to consider
         * @return the greatest common denominator, or shared factor, of `a` and `b`
         * @throws InvalidOperationException if a <= 0 or b < 0
         */
        public static int Gcd(int a, int b)
        {
            int gcd=0;
            if (a <= 0 || b < 0)
                throw new InvalidOperationException();
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a == 0 ? b : a;    
        }

        /**
         * Simplifies the numerator and denominator of a rational value.
         * <p>
         * For example:
         * `simplify(10, 100) = [1, 10]`
         * or:
         * `simplify(0, 10) = [0, 1]`
         *
         * @param numerator   the numerator of the rational value to simplify
         * @param denominator the denominator of the rational value to simplify
         * @return a two element array representation of the simplified numerator and denominator
         * @throws InvalidOperationException if the given denominator is 0
         */
        public static int[] Simplify(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new InvalidOperationException();
            int[] simp = new int[2];
            int gcd = 0;
            try
            {
                gcd = Gcd(numerator, denominator);
            }
            catch
            {
                simp[0] = numerator;
                simp[1] = denominator;
                return simp;
            }
            
            if (numerator == 0)
            {
                simp[0] = 0;
                simp[1] = 1;
            }
            else
            {
                simp[0] = numerator / gcd;
                simp[1] = denominator / gcd;
            }
            return simp;

        }

        /**
         * Constructor for rational values of the type:
         * <p>
         * `numerator / denominator`
         * <p>
         * Simplification of numerator/denominator pair should occur in this method.
         * If the numerator is zero, no further simplification can be performed
         *
         * @param numerator   the numerator of the rational value
         * @param denominator the denominator of the rational value
         * @throws ArgumentException if the given denominator is 0
         */
        public SimplifiedRational(int numerator, int denominator) : base(numerator, denominator)
        {
            if(denominator ==0)
	            throw new ArgumentException();

            int[] s = Simplify(numerator, denominator);
            Numerator = s[0];
            Denominator = s[1];

        }

        /**
		 * Specialized constructor to take advantage of shared code between
		 * Rational and SimplifiedRational
		 * <p>
		 * Essentially, this method allows us to implement most of RationalBase methods
		 * directly in the interface while preserving the underlying type
		 *
		 * @param numerator
		 *            the numerator of the rational value to construct
		 * @param denominator
		 *            the denominator of the rational value to construct
		 * @return the constructed rational value
		 * @throws ArgumentException
		 *             if the given denominator is 0
		 */
        public override RationalBase Construct(int numerator, int denominator)
        {
           if(denominator ==0)
	        throw new ArgumentException("Give me a different error");
           return new SimplifiedRational(numerator, denominator);
  
        }

        /**
         * @param obj the object to check this against for equality
         * @return true if the given obj is a SimplifiedRational value and its
         * numerator and denominator are equal to this rational value's numerator and denominator,
         * false otherwise
         */
        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            if (obj == null) return false;

            SimplifiedRational check = null;

            if (obj is SimplifiedRational)
                check = (SimplifiedRational)obj;
    

            return (check.Numerator == Numerator && check.Denominator == Denominator);
                
        }

        /**
         * If this is positive, the string should be of the form `numerator/denominator`
         * <p>
         * If this is negative, the string should be of the form `-numerator/denominator`
         *
         * @return a string representation of this rational value
         */
        public override string ToString()
        {
            if(numerator<0 || Denominator<0)
                return ($"-{numerator}/{Denominator}");
            if (numerator < 0 && Denominator <0)
                return ($"{numerator}/{Denominator}");

            return ($"{numerator}/{Denominator}");
        }
    }
}