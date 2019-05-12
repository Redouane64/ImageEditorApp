using System;
using Xunit;

namespace ImageEditorApp.ImageEditorTests
{
    public class RotateImageTests
    {

        [Theory(DisplayName = "Rotate Matrix CW")]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 })]
        public void RotateMatrixCWTest(int[] input)
        {
            var expected_3x2 = new int[] { 5, 3, 1, 6, 4, 2 };
            var result_3x2 = ImageEditor.Utilities.PixelsArrayHelpers.RotateCW(input, 3, 2);
            Assert.Equal(expected_3x2, result_3x2);

            var expected_2x3 = new int[] { 4, 1, 5, 2, 6, 3 };
            var result_2x3 = ImageEditor.Utilities.PixelsArrayHelpers.RotateCW(input, 2, 3);
            Assert.Equal(expected_2x3, result_2x3);
        }

        [Theory(DisplayName = "Rotate Matrix CCW")]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6 })]
        public void RotateMatrixCCW(int[] input)
        {
            var expected = new int[] { 3, 6, 2, 5, 1, 4 };
            var result = ImageEditor.Utilities.PixelsArrayHelpers.RotateCCW(input, 2, 3);

            Assert.Equal(expected, result);
        }
    }
}
