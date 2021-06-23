using System;

namespace RatingAdjustment.Services
{

    public class RatingAdjustmentService
    {
        const double MAX_STARS = 5.0;
        const double Z = 1.96;

        double _q;
        double _percent_positive;

        void SetPercentPositive(double stars)
        {
            _percent_positive = (stars * 20.0) / 100.0;
        }

        void SetQ(double number_of_ratings)
        {
            double a = _percent_positive;
            double b = number_of_ratings;
            _q = Z * Math.Sqrt(((a * (1.0 - a)) + ((Z * Z) / (40 * b))) / b);
        }

        public double Adjust(double stars, double number_of_ratings)
        {
            if (stars <= MAX_STARS)
            {
                SetPercentPositive(stars);
                SetQ(number_of_ratings);
                double a = _percent_positive;
                double b = number_of_ratings;
                double c = _q;
                double lb = ((a + ((Z * Z) / (2.0 * b)) - c) / (1.0 + ((Z * Z) / b)));
                double lbnd = (lb / 20.0) * 100.0;
                if (lbnd <= MAX_STARS)
                {
                    return lbnd;
                }
                else
                {
                    return stars;
                }
            }
            return 0.0;
        }
    }
}
