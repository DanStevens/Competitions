using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Snowflakes.Test
{
    public class IdentifyDuplicates
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, false)]
        [InlineData(new[] { 1, 2, 3, 4, 4 }, true)]
        [InlineData(new[] { 1, 2, 1, 4, 4 }, true)]
        [InlineData(new[] { 1, 3 }, false)]
        public void Test(int[] values, bool expectedResult)
        {
            Assert.Equal(expectedResult, ContainsDuplicates(values));
        }

        public bool ContainsDuplicates(int[] values)
        {
            for (int i = 0; i < values.Length; i++)
            for (int j = i + 1; j < values.Length; j++)
            {
                if (values[i] == values[j])
                    return true;
            }

            return false;
        }
    }
}
