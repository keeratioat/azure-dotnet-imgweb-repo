namespace Web
{
    public class Calculator
    {
        // Add two integers
        public int Add(int a, int b) => a + b;
       
        // Subtract two integers
        public int Subtract(int a, int b) => a - b;
        // Multiply two integers
        public int Multiply(int a, int b) => a * b;
        // Divide two integers (returns double to preserve fractional results and avoid integer division)
        public double Divide(int a, int b) => b == 0 ? 0.0 : (double)a / b;


    }
}
