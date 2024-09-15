namespace CalculatorTests
{
    public class UnitTest1
    {
        //[Fact]
        //public void Add_ShouldReturnSum_ForTwoNumbers()
        //{
        //    var calculator = new StringCalculator();
        //    int result = calculator.Calculate("1,5000");
        //    Assert.Equal(5001, result);
        //}

        //[Fact]
        //public void Add_ShouldReturn0_ForEmptyInput()
        //{
        //    var calculator = new StringCalculator();
        //    int result = calculator.Calculate("");
        //    Assert.Equal(0, result);
        //}

        //[Fact]
        //public void Add_ShouldHandleInvalidNumbers()
        //{
        //    var calculator = new StringCalculator();
        //    int result = calculator.Calculate("5,tytyt");
        //    Assert.Equal(5, result); // Invalid number treated as 0
        //}

        //[Fact]
        //public void Add_ShouldReturnSum_ForMultipleNumbers()
        //{
        //    var calculator = new StringCalculator();
        //    int result = calculator.Calculate("1,2,3,4,5,6,7,8,9,10");
        //    Assert.Equal(55, result); // Sum of 1 to 10
        //}

        //[Fact]
        //public void Add_ShouldSupportNewlineDelimiter()
        //{
        //    var calculator = new StringCalculator();
        //    int result = calculator.Calculate("1\n2,3");
        //    Assert.Equal(6, result);
        //}

        //[Fact]
        //public void Add_ShouldThrowException_ForNegativeNumbers()
        //{
        //    var calculator = new StringCalculator();
        //    var ex = Assert.Throws<ArgumentException>(() => calculator.Calculate("1,-2,-3"));
        //    Assert.Equal("Negatives not allowed: -2, -3", ex.Message);
        //}

        //[Fact]
        //public void Add_ShouldIgnoreNumbersGreaterThan1000()
        //{
        //    var calculator = new StringCalculator();
        //    int result = calculator.Calculate("2,1001,6");
        //    Assert.Equal(8, result); // 1001 is ignored
        //}

        //[Fact]
        //public void Add_ShouldSupportSingleCharacterCustomDelimiter()
        //{
        //    var calculator = new StringCalculator();
        //    int result = calculator.Calculate("//#\n2#5");
        //    Assert.Equal(7, result);
        //}

        //[Fact]
        //public void Add_ShouldSupportMultiCharacterCustomDelimiter()
        //{
        //    var calculator = new StringCalculator();
        //    int result = calculator.Calculate("//[***]\n11***22***33");
        //    Assert.Equal(66, result);
        //}

        //[Fact]
        //public void Add_ShouldSupportMultipleCustomDelimiters()
        //{
        //    var calculator = new StringCalculator();
        //    int result = calculator.Calculate("//[*][!!][r9r]\n11r9r22*33!!44");
        //    Assert.Equal(110, result);
        //}

        //[Fact]
        //public void Calculate_ShouldReturnFormulaAndResult()
        //{
        //    var calculator = new StringCalculator();
        //    (string formula, int result) = calculator.Calculate("2,,4,rrrr,1001,6");
        //    Assert.Equal("2+0+4+0+0+6", formula);
        //    Assert.Equal(12, result);
        //}

        //[Fact]
        //public void Calculate_ShouldSupportCustomDelimiterAndAllowNegatives()
        //{
        //    var calculator = new StringCalculator();
        //    int result = calculator.Calculate("//#\n2#-5", "#");
        //    Assert.Equal(-3, result); // Negatives allowed
        //}

        //[Fact]
        //public void Calculate_ShouldPerformSubtraction()
        //{
        //    var calculator = new StringCalculator();
        //    int result = calculator.Calculate("10,3,2", "-");
        //    Assert.Equal(5, result); // 10 - 3 - 2
        //}

        //[Fact]
        //public void Calculate_ShouldPerformMultiplication()
        //{
        //    var calculator = new StringCalculator();
        //    int result = calculator.Calculate("2,3,4", "*");
        //    Assert.Equal(24, result); // 2 * 3 * 4
        //}

        //[Fact]
        //public void Calculate_ShouldPerformDivision()
        //{
        //    var calculator = new StringCalculator();
        //    int result = calculator.Calculate("100,5,2", "/");
        //    Assert.Equal(10, result); // 100 / 5 / 2
        //}

        [Fact]
        public void Calculate_ShouldThrowExceptionOnDivideByZero()
        {
            var calculator = new StringCalculator();
            var ex = Assert.Throws<DivideByZeroException>(() => calculator.Calculate("100,0", "/"));
            Assert.Equal("Division by zero is not allowed", ex.Message);
        }


    }
}