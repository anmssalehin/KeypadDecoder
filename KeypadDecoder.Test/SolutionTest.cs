using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeypadDecoder.Test
{
    [TestClass]
    public class SolutionTest
    {
        [TestMethod]
        public void Decode33()
        {
            var result = Solution.OldPhonePad("33#");
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "e");
        }

        [TestMethod]
        public void Decode227b()
        {
            var result = Solution.OldPhonePad("227*#");
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "b");
        }


        [TestMethod]
        public void Decode4433555_555666()
        {
            var result = Solution.OldPhonePad("4433555 555666#");
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "hello");
        }

        [TestMethod]
        public void Decode8_88777444666b664()
        {
            var result = Solution.OldPhonePad("8 88777444666*664#");
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "turing");
        }

        [TestMethod]
        public void DecodeUnsupportedEncoding()
        {
            var result = Solution.OldPhonePad("1#");
            Assert.AreEqual(result, "");
        }
    }
}
